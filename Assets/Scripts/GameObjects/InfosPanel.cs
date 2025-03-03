using UnityEngine;
using DG.Tweening;

public struct State
{
    public bool nextButton;
    public bool infosButton;
    public bool settingsButton;
    public bool trophyButton;
    public bool soundButton;
}

public class InfosPanel : MonoBehaviour
{
    public GameObject animationBtnGrid;
    private Vector3 animationBtnGridInitialPosition;
    public Transform infosPanelContentParent;
    public static InfosPanel Instance;
    [SerializeField] private GameObject settingsBtn;
    private Vector3 settingsBtnInitialPosition;
    [SerializeField] private GameObject trophyBtn;
    private Vector3 trophyBtnInitialPosition;
    public State state;
    public bool isOpen = false;

    void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        animationBtnGridInitialPosition = animationBtnGrid.transform.position;
        settingsBtnInitialPosition = settingsBtn.transform.position;
        trophyBtnInitialPosition = trophyBtn.transform.position;
        transform.DOMoveY(-Screen.height, 0f);
        gameObject.SetActive(false);
    }

    public void Close()
    {
        if (!gameObject.activeSelf) return;

        isOpen = false;

        transform
            .DOMoveY(-Screen.height, 0.5f)
            .SetEase(Ease.InOutExpo)
            .OnComplete(() => gameObject.SetActive(false));

        animationBtnGrid.transform
            .DOMove(animationBtnGridInitialPosition, 0.5f)
            .SetEase(Ease.InOutExpo);

        AnimationManager.Instance.ToggleInfosButton(state.infosButton, false);
        AnimationManager.Instance.ToggleNextButton(state.nextButton, false);
        AnimationManager.Instance.ToggleSoundButton(state.soundButton, false);

        if (state.settingsButton)
        {
            settingsBtn.SetActive(true);
            settingsBtn.transform.position = settingsBtnInitialPosition + Vector3.right * 100;
            settingsBtn.transform
                .DOMove(settingsBtnInitialPosition, 0.5f)
                .SetEase(Ease.OutBack);
        }

        if (state.trophyButton)
        {
            trophyBtn.SetActive(true);
            trophyBtn.transform.position = trophyBtnInitialPosition + Vector3.right * 100;
            trophyBtn.transform
                .DOMove(trophyBtnInitialPosition, 0.5f)
                .SetEase(Ease.OutBack);
        }
    }

    public void SetContent(GameObject content)
    {
        if (gameObject.activeSelf) return;

        foreach (Transform child in infosPanelContentParent)
            child.gameObject.SetActive(false);
        content.SetActive(true);
    }

    public void Open()
    {
        if (gameObject.activeSelf) return;
        if (OnBoarding.Instance.gameObject.activeSelf) return;

        isOpen = true;

        gameObject.SetActive(true);
        transform
            .DOMoveY(0, 0.5f)
            .SetEase(Ease.InOutExpo);

        animationBtnGrid.transform
            .DOMoveY(Screen.height / 2 + 20, 0.5f)
            .SetEase(Ease.InOutExpo);

        // get current state
        state.nextButton = AnimationManager.Instance.NextButtonActive();
        state.infosButton = AnimationManager.Instance.InfosButtonActive();
        state.soundButton = AnimationManager.Instance.SoundButtonActive();
        state.settingsButton = settingsBtn.activeSelf;
        state.trophyButton = trophyBtn.activeSelf;

        if (state.infosButton) AnimationManager.Instance.ToggleInfosButton(false, false);
        if (state.nextButton) AnimationManager.Instance.ToggleNextButton(false, false);
        if (state.soundButton) AnimationManager.Instance.ToggleSoundButton(false, false);

        if (settingsBtn.activeSelf)
            settingsBtn.transform
                .DOMove(settingsBtnInitialPosition + Vector3.right * 100, 0.5f)
                .SetEase(Ease.Linear)
                .OnComplete(() => settingsBtn.SetActive(false));

        if (trophyBtn.activeSelf)
            trophyBtn.transform
                .DOMove(trophyBtnInitialPosition + Vector3.right * 100, 0.5f)
                .SetEase(Ease.Linear)
                .OnComplete(() => trophyBtn.SetActive(false));
    }

    public void SetTrophyBtnInitialPosition(Vector3 position)
    {
        trophyBtnInitialPosition = position;
    }
}