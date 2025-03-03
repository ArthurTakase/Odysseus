using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum AnimationType
{
    Animation1,
    Animation2,
    None
}

public enum TargetType
{
    Sun,
    Moon,
    Earth,
}

public class AnimationManager : MonoBehaviour
{
    // Visibles
    [Header("Détection des cibles (Auto Update)")]
    [SerializeField] private int nbTargetFound = 0;
    [SerializeField] private bool isSunFound = false;

    [Header("UI")]
    [SerializeField] private GameObject pleaseScan;

    [Header("Boutons")]
    [SerializeField] private GameObject launchAnimation1GO;
    [SerializeField] private GameObject launchAnimation2GO;
    [SerializeField] private GameObject backBtnGO;
    [SerializeField] private GameObject nextBtnGO;
    [SerializeField] private GameObject infosBtnGO;
    [SerializeField] private GameObject soundBtnGO;

    [Header("Animations")]
    public bool isAnimationPlaying = false;
    [SerializeField] private List<IGameMode> animations = new();

    // Hides
    [HideInInspector] public IGameMode currentAnimation;
    private Vector3 backBtnInitialPosition;
    public static AnimationManager Instance;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        launchAnimation1GO.SetActive(false);
        launchAnimation2GO.SetActive(false);
        backBtnGO.SetActive(false);
        nextBtnGO.SetActive(false);
        infosBtnGO.SetActive(false);

        backBtnInitialPosition = backBtnGO.transform.position;
    }

    private void FixedUpdate()
    {
        if (currentAnimation != null)
            currentAnimation.AnimationUpdate();
    }

    public void CloseAllAnimations()
    {
        InfosPanel.Instance.state.trophyButton = false;

        foreach (var animation in animations)
            animation.Reset();
        ToggleButtons();
        ToggleBackButton(false);
        nextBtnGO.SetActive(false);
        infosBtnGO.SetActive(false);
    }

    public void ToggleBackButton(bool active)
    {
        if (backBtnGO.activeSelf == active) return;

        if (active)
        {
            backBtnGO.SetActive(active);
            backBtnGO.transform.position = backBtnInitialPosition + Vector3.left * 100;
            backBtnGO.transform
                .DOMove(backBtnInitialPosition, 0.5f)
                .SetEase(Ease.OutBack);
        }
        else
        {
            backBtnGO.transform
                .DOMove(backBtnInitialPosition + Vector3.left * 100, 0.5f)
                .SetEase(Ease.InBack)
                .OnComplete(() => backBtnGO.SetActive(false));
        }
    }

    private void ToggleButtons()
    {
        if (isAnimationPlaying) return;

        if (nbTargetFound == 0)
        {
            pleaseScan.SetActive(true);
            ToggleButtonAnimate(launchAnimation1GO, false);
            ToggleButtonAnimate(launchAnimation2GO, false);
            return;
        }

        pleaseScan.SetActive(false);

        if (nbTargetFound >= 1) ToggleButtonAnimate(launchAnimation1GO, isSunFound);
        if (nbTargetFound == 3) ToggleButtonAnimate(launchAnimation2GO, true);
    }

    public void HideButtons()
    {
        ToggleButtonAnimate(launchAnimation1GO, false);
        ToggleButtonAnimate(launchAnimation2GO, false);
    }

    private void ToggleButtonAnimate(GameObject button, bool active)
    {
        if (button.activeSelf == active) return;

        if (active)
        {
            button.SetActive(true);
            button.transform.localScale = Vector3.zero;
            button.transform
                .DOScale(Vector3.one, 0.35f)
                .SetEase(Ease.OutBack);
        }
        else
        {
            button.transform
                .DOScale(Vector3.zero, 0.35f)
                .SetEase(Ease.OutBack)
                .OnComplete(() => button.SetActive(false));
        }

    }

    public void AddTarget(TargetType type)
    {
        MyVibration.MediumVibration();

        nbTargetFound++;
        switch (type)
        {
            case TargetType.Sun: isSunFound = true; break;
            case TargetType.Moon: break;
            case TargetType.Earth: break;
        }
        ToggleButtons();
    }

    public void RemoveTarget(TargetType type)
    {
        MyVibration.SmallVibration();

        nbTargetFound--;
        switch (type)
        {
            case TargetType.Sun: isSunFound = false; break;
            case TargetType.Moon: break;
            case TargetType.Earth: break;
        }
        ToggleButtons();
    }

    public void SetNextButtonAction(Action action)
    {
        Button btn = nextBtnGO.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() =>
        {
            MyVibration.SmallVibration();
            action();
        });
    }

    public void SetInfosBtnAction(GameObject content)
    {
        Button btn = infosBtnGO.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() =>
        {
            MyVibration.SmallVibration();
            InfosPanel.Instance.SetContent(content);
            InfosPanel.Instance.Open();
        });
    }

    public void SetSoundBtnAction(AudioClip audio, List<Subtitles> subtitles)
    {
        Button btn = soundBtnGO.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() =>
        {
            TrophyManager.Instance.UnlockTrophy(TrophyName.ListenAudio);
            MyVibration.SmallVibration();
            AudioManager.Instance.Play(audio);
            SubtitlesManager.Instance.ShowSubtitles(subtitles);
        });
    }

    public void ToggleNextButton(bool active, bool force = true)
    {
        ToggleButtonAnimate(nextBtnGO, active);
        if (force) InfosPanel.Instance.state.nextButton = active;
    }

    public void ToggleInfosButton(bool active, bool force = true)
    {
        ToggleButtonAnimate(infosBtnGO, active);
        if (force) InfosPanel.Instance.state.infosButton = active;
    }

    public void ToggleSoundButton(bool active, bool force = true)
    {
        ToggleButtonAnimate(soundBtnGO, active);
        if (force) InfosPanel.Instance.state.soundButton = active;

        if (!active)
        {
            AudioManager.Instance.Stop();
            SubtitlesManager.Instance.HideSubtitlesNow();
        }
    }

    public bool NextButtonActive()
    {
        return nextBtnGO.activeSelf;
    }

    public bool InfosButtonActive()
    {
        return infosBtnGO.activeSelf;
    }

    public bool SoundButtonActive()
    {
        return soundBtnGO.activeSelf;
    }
}