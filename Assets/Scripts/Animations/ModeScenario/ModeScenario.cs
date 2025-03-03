using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ModeScenario : IGameMode
{
    [System.Serializable]
    struct StepData
    {
        public AudioClip audioClip;
        public AnimationClip animationClip;
        public WorldSpaceTag worldSpaceTag;
        public GameObject infosPanel;
    }

    const float DEFAULT_DURATION = 1.5f;
    private const float ANIMATION_SPEED_DEFAULT = 1f;
    private const float ANIMATION_SPEED_x02 = 0.2f;
    private const float ANIMATION_SPEED_x025 = 0.25f;
    private const float ANIMATION_SPEED_x03 = 0.3f;
    private const float ANIMATION_SPEED_x05 = 0.5f;

    [Header("Astres")]
    [SerializeField] private Aster sun;
    [SerializeField] private Aster moon;
    [SerializeField] private Aster earth;
    [SerializeField] private Aster bigMoon;
    [SerializeField] private Aster bigEarth;
    [SerializeField] private Aster middleMoon;

    [Header("GameObjects généraux")]
    [SerializeField] private GameObject sunIndicator;
    [SerializeField] private GameObject animationGO;
    [SerializeField] private GameObject space;
    [SerializeField] private GameObject orbiteTerre;
    [SerializeField] private GameObject orbiteLune;

    [Header("Rotations")]
    [SerializeField] private Rotate rotateLight;

    [Header("Animations")]
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip idle;

    [Header("Steps Data")]
    [SerializeField] private Credits credits;
    [SerializeField] private List<StepData> stepsData = new();

    // Privates
    private Rotate rotateSun;
    private Rotate rotateMoon;
    private Rotate rotateEarth;
    private RotateAround rotateAroundMoon;
    private RotateAround rotateAroundEarth;
    private Rotate rorateBigEarth;
    private Rotate rorateMiddleMoon;
    private Vector3 spaceInitialScale;
    private Vector3 orbiteTerreInitialScale;
    private Vector3 orbiteLuneInitialScale;
    private readonly SubtitlesData subtitlesData = new();

    private void Awake()
    {
        steps.Add(Step1);
        steps.Add(Step2);
        steps.Add(Step3);
        steps.Add(Step4);
        steps.Add(Step5);
        steps.Add(Step6);
        steps.Add(Close);

        rotateSun = sun.GetComponent<Rotate>();
        rotateMoon = moon.GetComponent<Rotate>();
        rotateEarth = earth.GetComponent<Rotate>();
        rotateAroundMoon = moon.GetComponent<RotateAround>();
        rotateAroundEarth = earth.GetComponent<RotateAround>();
        rorateBigEarth = bigEarth.GetComponent<Rotate>();
        rorateMiddleMoon = middleMoon.GetComponent<Rotate>();

        spaceInitialScale = space.transform.localScale;
        orbiteTerreInitialScale = orbiteTerre.transform.localScale;
        orbiteLuneInitialScale = orbiteLune.transform.localScale;
        sun.SetupInitialScale();
        moon.SetupInitialScale();
        earth.SetupInitialScale();
        bigMoon.SetupInitialScale();
        bigEarth.SetupInitialScale();
        middleMoon.SetupInitialScale();
    }

    public override void Reset()
    {
        base.Reset();

        SubtitlesManager.Instance.HideSubtitlesNow();

        rotateSun.enabled = false;
        rotateMoon.enabled = false;
        rotateEarth.enabled = false;
        rotateAroundMoon.enabled = false;
        rotateLight.enabled = false;
        rotateAroundEarth.enabled = false;
        rorateBigEarth.enabled = false;
        rorateMiddleMoon.enabled = false;

        animator.enabled = true;

        sun.SetActive(false);
        moon.SetActive(false);
        earth.SetActive(false);
        space.SetActive(false);
        sunIndicator.SetActive(true);
        animationGO.SetActive(false);
        bigMoon.SetActive(false);
        orbiteTerre.SetActive(false);
        orbiteLune.SetActive(false);
        bigEarth.SetActive(false);
        middleMoon.SetActive(false);
        stepsData.ForEach(stepData => { if (stepData.worldSpaceTag != null) stepData.worldSpaceTag.HideImediately(); });
        InfosPanel.Instance.Close();
        credits.gameObject.SetActive(false);

        animator.Play(idle.name, 0, 0);
        animator.speed = ANIMATION_SPEED_DEFAULT;

        AudioManager.Instance.Stop();
        AnimationManager.Instance.isAnimationPlaying = false;
        AnimationManager.Instance.currentAnimation = null;

        AnimationManager.Instance.ToggleNextButton(false);
        AnimationManager.Instance.ToggleInfosButton(false);
        OnBoarding.Instance.Close();
    }

    private IEnumerator Step1()
    {
        AnimationManager.Instance.isAnimationPlaying = true;
        AnimationManager.Instance.currentAnimation = this;

        AnimationManager.Instance.ToggleBackButton(true);
        AnimationManager.Instance.HideButtons();
        sunIndicator.SetActive(false);
        animationGO.SetActive(true);

        AudioManager.Instance.Play(stepsData[0].audioClip);
        SubtitlesManager.Instance.ShowSubtitles(subtitlesData.STEP1);

        yield return ActionAndWait(() => { bigMoon.FadeIn(); }, 8f);

        FadeIn(space.gameObject, spaceInitialScale);
        AudioManager.Instance.Play(AudioManager.Instance.A1SpaceAudioSource, AudioManager.Instance.smallSweep, true);

        yield return ActionAndWait(() => { bigMoon.FadeOut(); }, 1f);

        animator.Play(stepsData[0].animationClip.name, 0, 0);
        animator.speed = ANIMATION_SPEED_x025;

        yield return ActionAndWait(() => { earth.FadeIn(); }, 0.5f);
        yield return ActionAndWait(() => { moon.FadeIn(); }, stepsData[0].animationClip.length);

        sun.FadeIn();
        rotateSun.enabled = true;

        yield return new WaitUntil(() => SubtitlesManager.Instance.isFinished);

        AnimationManager.Instance.SetNextButtonAction(NextStep);
        ToggleBottomButtons(nextStatus: true, infoStatus: false);
    }

    private IEnumerator Step2()
    {
        ToggleBottomButtons(nextStatus: false, infoStatus: false);

        AudioManager.Instance.Play(stepsData[1].audioClip);

        SubtitlesManager.Instance.ShowSubtitles(subtitlesData.STEP2);

        yield return new WaitForSeconds(1f);

        animator.Play(stepsData[1].animationClip.name, 0, 0);
        animator.speed = ANIMATION_SPEED_x05;

        yield return new WaitForSeconds(stepsData[1].animationClip.length / ANIMATION_SPEED_x05);

        stepsData[1].worldSpaceTag.Toggle(true);

        yield return new WaitUntil(() => SubtitlesManager.Instance.isFinished);

        AnimationManager.Instance.SetInfosBtnAction(stepsData[1].infosPanel);
        ToggleBottomButtons(nextStatus: true, infoStatus: true);
    }

    private IEnumerator Step3()
    {
        ToggleBottomButtons(nextStatus: false, infoStatus: false);
        stepsData[1].worldSpaceTag.Toggle(false);

        AudioManager.Instance.Play(stepsData[2].audioClip);

        SubtitlesManager.Instance.ShowSubtitles(subtitlesData.STEP3);

        animator.Play(stepsData[2].animationClip.name, 0, 0);
        animator.speed = ANIMATION_SPEED_x05;

        yield return new WaitForSeconds(stepsData[2].animationClip.length / ANIMATION_SPEED_x05);

        stepsData[2].worldSpaceTag.Toggle(true);

        yield return new WaitUntil(() => SubtitlesManager.Instance.isFinished);

        AnimationManager.Instance.SetInfosBtnAction(stepsData[2].infosPanel);
        ToggleBottomButtons(nextStatus: true, infoStatus: true);
    }

    private IEnumerator Step4()
    {
        ToggleBottomButtons(nextStatus: false, infoStatus: false);
        stepsData[2].worldSpaceTag.Toggle(false);

        AudioManager.Instance.Play(stepsData[3].audioClip);

        SubtitlesManager.Instance.ShowSubtitles(subtitlesData.STEP4);

        animator.Play(stepsData[3].animationClip.name, 0, 0);
        animator.speed = ANIMATION_SPEED_x03;

        yield return new WaitForSeconds(stepsData[3].animationClip.length / ANIMATION_SPEED_x03);

        stepsData[3].worldSpaceTag.Toggle(true);

        yield return new WaitUntil(() => SubtitlesManager.Instance.isFinished);

        AnimationManager.Instance.SetInfosBtnAction(stepsData[3].infosPanel);
        ToggleBottomButtons(nextStatus: true, infoStatus: true);
    }

    private IEnumerator Step5()
    {
        ToggleBottomButtons(nextStatus: false, infoStatus: false);
        stepsData[3].worldSpaceTag.Toggle(false);

        AudioManager.Instance.Play(stepsData[4].audioClip);

        SubtitlesManager.Instance.ShowSubtitles(subtitlesData.STEP5);

        animator.enabled = false;
        rotateAroundEarth.enabled = true;
        rotateAroundMoon.enabled = true;
        rotateLight.enabled = true;
        rotateEarth.enabled = true;
        rotateMoon.enabled = true;

        yield return new WaitForSeconds(6f);

        yield return ActionAndWait(() => { moon.FadeOut(); }, 1f);
        yield return ActionAndWait(() => { earth.FadeOut(); }, 1f);
        yield return ActionAndWait(() => { sun.FadeOut(); }, 1f);

        AudioManager.Instance.Play(AudioManager.Instance.A1SpaceAudioSource, AudioManager.Instance.transitionBase, false);
        FadeOut(space, spaceInitialScale);

        yield return new WaitForSeconds(1f);

        yield return ActionAndWait(() => { bigEarth.FadeIn(); }, .5f);
        yield return ActionAndWait(() => { middleMoon.FadeIn(); }, 1f);

        FadeIn(orbiteLune, orbiteLuneInitialScale);
        AudioManager.Instance.Play(AudioManager.Instance.A1BigEarthAudioSource, AudioManager.Instance.smallSweep, false);

        yield return new WaitForSeconds(1.5f);

        FadeIn(orbiteTerre, orbiteTerreInitialScale);
        AudioManager.Instance.Play(AudioManager.Instance.A1BigEarthAudioSource, AudioManager.Instance.smallSweep, false);

        yield return new WaitForSeconds(1f);

        animator.enabled = true;
        animator.Play(stepsData[4].animationClip.name, 0, 0);
        animator.speed = ANIMATION_SPEED_x02;

        yield return new WaitForSeconds(stepsData[4].animationClip.length / ANIMATION_SPEED_x02);

        animator.enabled = false;
        rorateBigEarth.enabled = true;
        rorateMiddleMoon.enabled = true;

        stepsData[4].worldSpaceTag.Toggle(true);

        yield return new WaitUntil(() => SubtitlesManager.Instance.isFinished);

        AnimationManager.Instance.SetInfosBtnAction(stepsData[4].infosPanel);
        ToggleBottomButtons(nextStatus: true, infoStatus: true);
    }

    public IEnumerator Step6()
    {
        ToggleBottomButtons(nextStatus: false, infoStatus: false);
        stepsData[4].worldSpaceTag.Toggle(false);

        yield return ActionAndWait(() => { bigEarth.FadeOut(); }, 0.1f);
        yield return ActionAndWait(() => { middleMoon.FadeOut(); }, 0.1f);
        yield return ActionAndWait(() => { FadeOut(orbiteLune, orbiteLuneInitialScale); }, 0.1f);
        yield return ActionAndWait(() => { FadeOut(orbiteTerre, orbiteTerreInitialScale); }, 0.1f);

        AudioManager.Instance.Play(stepsData[5].audioClip, 0.2f);

        yield return new WaitForSeconds(0.8f);

        earth.FadeIn();
        moon.FadeIn();

        animator.enabled = false;
        rotateAroundEarth.enabled = true;
        rotateAroundMoon.enabled = true;
        rotateLight.enabled = true;
        rotateEarth.enabled = true;
        rotateMoon.enabled = true;

        yield return new WaitForSeconds(1f);

        credits.gameObject.SetActive(true);

        yield return new WaitForSeconds(4f);

        AnimationManager.Instance.ToggleNextButton(true);
    }

    public IEnumerator Close()
    {
        AnimationManager.Instance.CloseAllAnimations();
        yield return null;
    }

    private void FadeIn(GameObject GO, Vector3 initialScale, float duration = DEFAULT_DURATION)
    {
        MyVibration.SmallVibration();
        GO.SetActive(true);
        GO.transform.localScale = Vector3.zero;
        GO.transform.DOScale(initialScale, duration).SetEase(Ease.InOutBack);
    }

    private void FadeOut(GameObject GO, Vector3 initialScale, float duration = DEFAULT_DURATION)
    {
        GO.SetActive(true);
        GO.transform.localScale = initialScale;
        GO.transform.DOScale(Vector3.zero, duration).SetEase(Ease.InOutBack).OnComplete(() => GO.SetActive(false));
    }
}