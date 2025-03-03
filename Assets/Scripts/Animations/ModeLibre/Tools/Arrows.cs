using System;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    [SerializeField] private Material arrowMaterial;
    [SerializeField] private Material arrowMaterial2;
    [SerializeField] private Material arrowMaterial3;
    [SerializeField] private float lastTolerance = 0.01f;
    [SerializeField] private float moonTolerance = 0.02f;
    [SerializeField] private List<LineRenderer> arrows;
    [SerializeField] private Aster sun;
    [SerializeField] private Aster moon;
    [SerializeField] private Aster earth;
    private bool arrowsVisible = true;
    private const float LIGHT_OFFSET = 0.015f;

    public void UpdateSliders(Slider sliderLastTolerance, Slider sliderMoonTolerance)
    {
        sliderLastTolerance.UpdateSliderAndText(lastTolerance);
        sliderMoonTolerance.UpdateSliderAndText(moonTolerance);
    }

    public void ToggleArrows(bool value)
    {
        arrowsVisible = value;
        foreach (var arrow in arrows)
            arrow.gameObject.SetActive(value);
    }

    public void ArrowsUpdate(ResultsData resultsData)
    {
        if (!arrowsVisible) return;

        if (sun.HasMoved(lastTolerance)
            || moon.HasMoved(lastTolerance)
            || earth.HasMoved(lastTolerance))
        {
            UpdateLights(resultsData);
        }
    }

    private void UpdateLights(ResultsData resultsData)
    {
        void UpdateArrowData(LineRenderer arrow, Material material, Vector3 offset, float length)
        {
            arrow.SetPosition(0, sun.transform.position + offset);
            arrow.SetPosition(1, earth.transform.position + offset);
            material.SetFloat("_Length", length * 100);
        }

        float distanceEarthSun = Vector3.Distance(sun.transform.position, earth.transform.position);

        UpdateArrowData(arrows[0], arrowMaterial, Vector3.zero, distanceEarthSun);
        UpdateArrowData(arrows[1], arrowMaterial2, Vector3.up * LIGHT_OFFSET, distanceEarthSun);
        UpdateArrowData(arrows[2], arrowMaterial3, Vector3.down * LIGHT_OFFSET, distanceEarthSun);

        if (!(resultsData.isMoonEarthSunAlligned && resultsData.isMoonBetweenEarthSun)) return;

        float meanY1 = (arrows[0].GetPosition(0).y + arrows[0].GetPosition(1).y) / 2;
        float meanY2 = (arrows[1].GetPosition(0).y + arrows[1].GetPosition(1).y) / 2;
        float meanY3 = (arrows[2].GetPosition(0).y + arrows[2].GetPosition(1).y) / 2;
        float moonY = moon.transform.position.y;
        float distanceMoonSun = Vector3.Distance(sun.transform.position, moon.transform.position);

        void UpdateArrow(int index, Material material)
        {
            float distance = Vector3.Distance(arrows[index].GetPosition(0), moon.transform.position);
            Vector3 closestPoint = Vector3.Lerp(arrows[index].GetPosition(0), arrows[index].GetPosition(1), distance / distanceEarthSun);
            if (Vector3.Distance(closestPoint, moon.transform.position) > moonTolerance) return;
            arrows[index].SetPosition(1, closestPoint);
            material.SetFloat("_Length", distanceMoonSun * 100);

            TrophyManager.Instance.UnlockTrophy(TrophyName.MoonInLight);
        }

        bool isMoonInLight1 = Math.Abs(meanY1 - moonY) < Math.Abs(meanY2 - moonY) && Math.Abs(meanY1 - moonY) < Math.Abs(meanY3 - moonY);
        bool isMoonInLight2 = Math.Abs(meanY2 - moonY) < Math.Abs(meanY1 - moonY) && Math.Abs(meanY2 - moonY) < Math.Abs(meanY3 - moonY);
        bool isMoonInLight3 = Math.Abs(meanY3 - moonY) < Math.Abs(meanY1 - moonY) && Math.Abs(meanY3 - moonY) < Math.Abs(meanY2 - moonY);

        if (isMoonInLight1) UpdateArrow(0, arrowMaterial);
        if (isMoonInLight2) UpdateArrow(1, arrowMaterial2);
        if (isMoonInLight3) UpdateArrow(2, arrowMaterial3);
    }

    public void ToggleArrowsVisibility()
    {
        TrophyManager.Instance.UnlockTrophy(TrophyName.DisableLights);
        arrowsVisible = !arrowsVisible;
        ToggleArrows(arrowsVisible);
    }

    public void UpdateLastTolerance(float value)
    {
        lastTolerance = value;
    }

    public void UpdateMoonTolerance(float value)
    {
        moonTolerance = value;
    }
}