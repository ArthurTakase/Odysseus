using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[Serializable]
public enum FreeModeEventType
{
    BlueMoon,
    BloodMoon,
    Alligned,
    TotalEclipse,
    None
}

public struct ResultsData
{
    public bool isMoonEarthSunAlligned;
    public bool isEarthBetweenMoonSun;
    public bool isMoonBetweenEarthSun;
    public bool isMoonSameHeightAsEarth;
    public bool isMoonSameHeightAsEarthForBlueMoon;
    public bool isMoonOnTopOfEarth;
    public bool isMoonEarthSunOpposite;
}

public struct AnimationData
{
    public float angleMoonEarth;
    public float angleEarthSun;
    public float angleMoonSun;
    public float distanceMoonEarth;
    public float distanceEarthSun;
    public float distanceMoonSun;
    public Vector3 directionSunMoon;
    public Vector3 directionSunEarth;
    public Vector3 directionEarthMoon;
    public float angle;
}

public class ModeLibre : IGameMode
{
    [Header("SubElements")]
    [SerializeField] private BloodMoon bloodMoon;
    [SerializeField] private BlueMoon blueMoon;
    [SerializeField] private TotalEclipse totalEclipse;
    [SerializeField] private Alligned alligned;
    [SerializeField] private Arrows arrows;
    [SerializeField] private UI ui;


    [Header("GameObjects généraux")]
    [SerializeField] private Aster sun;
    [SerializeField] private Aster moon;
    [SerializeField] private Aster earth;
    [SerializeField] private GameObject sunIndicator;
    [SerializeField] private GameObject earthIndicator;
    [SerializeField] private GameObject moonIndicator;
    [SerializeField] private GameObject SunLight;
    [SerializeField] private GameObject SunLight2;
    [SerializeField] private HelpPanel helpPanel;

    [Header("Settings")]
    [SerializeField] private Slider sliderTolerance;
    [SerializeField] private Slider sliderLastTolerance;
    [SerializeField] private Slider sliderMoonTolerance;

    // Privates
    private float tolerance = 10f;
    private Rotate rotateMoon;
    private Rotate rotateEarth;
    private FreeModeEventType currentEvent = FreeModeEventType.None;
    private readonly List<FreeModeEventType> lastEvents = new();

    private ResultsData resultsData = new();

    private void Awake()
    {
        steps.Add(Step1);

        rotateMoon = moon.GetComponent<Rotate>();
        rotateEarth = earth.GetComponent<Rotate>();

        sun.SetupInitialScale();
        moon.SetupInitialScale();
        earth.SetupInitialScale();

        sliderTolerance.UpdateSliderAndText(tolerance);
        arrows.UpdateSliders(sliderLastTolerance, sliderMoonTolerance);
    }

    public override void Reset()
    {
        base.Reset();

        rotateMoon.enabled = false;
        rotateEarth.enabled = false;

        sun.SetActive(false);
        moon.SetActive(false);
        earth.SetActive(false);
        SunLight.SetActive(false);
        SunLight2.SetActive(false);
        ui.Reset();

        sunIndicator.SetActive(true);
        earthIndicator.SetActive(true);
        moonIndicator.SetActive(true);

        arrows.ToggleArrows(false);
        helpPanel.Close();

        earth.ResetLastPosition();
        moon.ResetLastPosition();
        sun.ResetLastPosition();

        InfosPanel.Instance.Close();

        currentEvent = FreeModeEventType.None;
        lastEvents.Clear();

        blueMoon.Stop();
        totalEclipse.Stop();
        bloodMoon.Stop();
        alligned.Stop();

        CancelInvoke(nameof(LaunchPositionMaths));

        AnimationManager.Instance.isAnimationPlaying = false;
        AnimationManager.Instance.currentAnimation = null;
    }

    public override void AnimationUpdate()
    {
        if (!sun.IsActive()) return;

        SunLight.transform.LookAt(earth.transform.position);
        SunLight2.transform.LookAt(moon.transform.position);

        arrows.ArrowsUpdate(resultsData);
    }

    private IEnumerator Step1()
    {
        AnimationManager.Instance.isAnimationPlaying = true;
        AnimationManager.Instance.currentAnimation = this;

        InvokeRepeating(nameof(LaunchPositionMaths), 0, 1);

        AnimationManager.Instance.ToggleBackButton(true);
        AnimationManager.Instance.HideButtons();
        OnBoarding.Instance.Close();

        sunIndicator.SetActive(false);
        earthIndicator.SetActive(false);
        moonIndicator.SetActive(false);

        arrows.ToggleArrows(true);

        helpPanel.Toggle(true);

        ui.Launch();

        SunLight.SetActive(true);
        SunLight2.SetActive(true);

        sun.FadeIn();
        moon.FadeIn();
        earth.FadeIn();

        AstersRaycast.Instance.action = () =>
        {
            MyVibration.SmallVibration();
            arrows.ToggleArrowsVisibility();
        };

        yield return null;
    }

    private void LaunchPositionMaths()
    {
        AnimationData data = new()
        {
            angleMoonEarth = MyMaths.GetAngleBetweenPoints(earth.transform.position, moon.transform.position),
            angleEarthSun = MyMaths.GetAngleBetweenPoints(sun.transform.position, earth.transform.position),
            angleMoonSun = MyMaths.GetAngleBetweenPoints(sun.transform.position, moon.transform.position),

            distanceMoonEarth = MyMaths.GetDistanceBetweenPoints(earth.transform.position, moon.transform.position),
            distanceEarthSun = MyMaths.GetDistanceBetweenPoints(sun.transform.position, earth.transform.position),
            distanceMoonSun = MyMaths.GetDistanceBetweenPoints(sun.transform.position, moon.transform.position),

            angle = MyMaths.GetAngle(sun.transform.position, earth.transform.position, moon.transform.position),

            directionSunMoon = sun.transform.position - moon.transform.position,
            directionSunEarth = sun.transform.position - earth.transform.position,
            directionEarthMoon = earth.transform.position - moon.transform.position
        };

        ui.UpdateText(data);

        var tempEvent = GetEventType(data);
        tempEvent = UpdateEventTypeWithQueue(tempEvent);

        if (currentEvent != tempEvent)
        {
            currentEvent = tempEvent;
            ShowEvent();
        }
    }

    private FreeModeEventType GetEventType(AnimationData data)
    {
        float isNear_angleMoonEarth_angleEarthSun = MyMaths.IsNear(data.angleMoonEarth, data.angleEarthSun);
        float isNear_angleMoonEarth_angleMoonSun = MyMaths.IsNear(data.angleMoonEarth, data.angleMoonSun);

        resultsData.isMoonEarthSunAlligned = isNear_angleMoonEarth_angleEarthSun < tolerance && isNear_angleMoonEarth_angleMoonSun < tolerance;
        resultsData.isMoonEarthSunOpposite = Vector3.Dot(data.directionSunMoon, data.directionSunEarth) < 0;
        resultsData.isEarthBetweenMoonSun = data.distanceMoonSun > data.distanceEarthSun && !resultsData.isMoonEarthSunOpposite;
        resultsData.isMoonBetweenEarthSun = data.distanceMoonSun < data.distanceEarthSun && !resultsData.isMoonEarthSunOpposite;
        resultsData.isMoonSameHeightAsEarth = Math.Abs(moon.transform.position.y - earth.transform.position.y) < 0.03;
        resultsData.isMoonSameHeightAsEarthForBlueMoon = Math.Abs(moon.transform.position.y - earth.transform.position.y) < 0.06;
        resultsData.isMoonOnTopOfEarth = moon.transform.position.y > earth.transform.position.y;

        ui.UpdateUIPictures(resultsData);

        if (!alligned.AreConditionsMet(resultsData)) return FreeModeEventType.None;
        if (bloodMoon.AreConditionsMet(resultsData)) return FreeModeEventType.BloodMoon;
        if (blueMoon.AreConditionsMet(resultsData)) return FreeModeEventType.BlueMoon;
        if (totalEclipse.AreConditionsMet(resultsData)) return FreeModeEventType.TotalEclipse;
        return FreeModeEventType.Alligned;
    }



    private FreeModeEventType UpdateEventTypeWithQueue(FreeModeEventType newType)
    {
        const int maxQueueSize = 3;
        int nbEventTypes = FreeModeEventType.GetValues(typeof(FreeModeEventType)).Length;

        lastEvents.Add(newType);
        if (lastEvents.Count > maxQueueSize) lastEvents.RemoveAt(0);

        int[] eventTypesCount = new int[nbEventTypes];
        foreach (var eventType in lastEvents)
            eventTypesCount[(int)eventType]++;

        // get the index of the most common event
        int mostCommonEventIndex = 0;
        for (int i = 1; i < nbEventTypes; i++)
            if (eventTypesCount[i] > eventTypesCount[mostCommonEventIndex])
                mostCommonEventIndex = i;

        return (FreeModeEventType)mostCommonEventIndex;
    }

    private void ShowEvent()
    {
        switch (currentEvent)
        {
            case FreeModeEventType.Alligned:
                bloodMoon.Stop();
                blueMoon.Stop();
                totalEclipse.Stop();
                alligned.Play();
                MyVibration.SmallVibration();
                break;
            case FreeModeEventType.BlueMoon:
                bloodMoon.Stop(true);
                alligned.Stop(true);
                totalEclipse.Stop(true);
                blueMoon.Play();
                MyVibration.SmallVibration();
                break;
            case FreeModeEventType.BloodMoon:
                blueMoon.Stop(true);
                alligned.Stop(true);
                totalEclipse.Stop(true);
                bloodMoon.Play();
                MyVibration.SmallVibration();
                break;
            case FreeModeEventType.TotalEclipse:
                blueMoon.Stop(true);
                alligned.Stop(true);
                bloodMoon.Stop(true);
                totalEclipse.Play();
                MyVibration.SmallVibration();
                break;
            case FreeModeEventType.None:
                bloodMoon.Stop();
                blueMoon.Stop();
                alligned.Stop();
                totalEclipse.Stop();
                MyVibration.SmallVibration();
                break;
        }
    }

    public void UpdateTolerance(float value)
    {
        tolerance = value;
    }
}