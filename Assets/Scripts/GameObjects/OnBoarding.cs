using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class OnBoarding : MonoBehaviour
{
    public GameObject openBtn;
    public List<GameObject> onBoardingGO;
    public List<Image> onBoardingStepIndicator;
    public Sprite fullIndicator;
    public Sprite emptyIndicator;
    private int currentStep = 0;
    private Vector2 touchStart;
    private Vector3 openBtnInitialPosition;
    public GameObject animationBtnGrid;
    private Vector3 animationBtnGridInitialPosition;
    [SerializeField] private GameObject noTargetFoundIndicator;
    private bool lastStateNoTargetFoundIndicator;
    public static OnBoarding Instance;

    void Awake()
    {
        Instance = this;
        if (!this.gameObject.activeSelf) gameObject.SetActive(true);
    }

    public void Start()
    {
        openBtnInitialPosition = openBtn.transform.position;
        animationBtnGridInitialPosition = animationBtnGrid.transform.position;
        Close();
    }

    public void Close()
    {
        transform
            .DOMoveY(-Screen.height, 0.5f)
            .SetEase(Ease.InOutExpo)
            .OnComplete(() => gameObject.SetActive(false));

        openBtn.SetActive(true);
        openBtn.transform.position = openBtnInitialPosition + Vector3.right * 100;
        openBtn.transform
            .DOMove(openBtnInitialPosition, 0.5f)
            .SetEase(Ease.OutBack);

        animationBtnGrid.transform
            .DOMove(animationBtnGridInitialPosition, 0.5f)
            .SetEase(Ease.InOutExpo);

        if (lastStateNoTargetFoundIndicator) noTargetFoundIndicator.SetActive(true);
    }

    public void Open(bool force = false)
    {
        if (gameObject.activeSelf && !force) return;
        if (InfosPanel.Instance.gameObject.activeSelf && !force) return;

        lastStateNoTargetFoundIndicator = noTargetFoundIndicator.activeSelf;
        noTargetFoundIndicator.SetActive(false);

        GoToStep(0);

        gameObject.SetActive(true);
        transform
            .DOMoveY(0, 0.5f)
            .SetEase(Ease.InOutExpo);

        openBtn.transform
            .DOMove(openBtnInitialPosition + Vector3.right * 100, 0.5f)
            .SetEase(Ease.Linear)
            .OnComplete(() => openBtn.SetActive(false));

        animationBtnGrid.transform
            .DOMoveY(Screen.height / 2 + 20, 0.5f)
            .SetEase(Ease.InOutExpo);
    }

    void Update()
    {
        // detect swipe
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) touchStart = touch.position;

            if (touch.phase == TouchPhase.Ended)
            {
                Vector2 touchEnd = touch.position;

                float x = touchEnd.x - touchStart.x;
                float y = touchEnd.y - touchStart.y;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x > 0) PreviousStep();
                    else if (x < 0) NextStep();
                }
            }
        }
    }

    void GoToStep(int step)
    {
        if (step < 0 || step >= onBoardingGO.Count) return;

        onBoardingStepIndicator[currentStep].sprite = emptyIndicator;
        onBoardingGO[currentStep].SetActive(false);

        currentStep = step;

        onBoardingGO[currentStep].SetActive(true);
        onBoardingStepIndicator[currentStep].sprite = fullIndicator;
        onBoardingGO[currentStep].transform.position = new Vector3(Screen.width / 2, onBoardingGO[currentStep].transform.position.y, 0);
    }

    void NextStep()
    {
        if (currentStep >= onBoardingGO.Count - 1)
        {
            Close();
            return;
        }

        // using DOTween to animate the swap between the current and next onboarding step
        onBoardingStepIndicator[currentStep].sprite = emptyIndicator;
        onBoardingGO[currentStep].transform.DOMoveX(-Screen.width * 2, 0.3f).OnComplete(() =>
        {
            onBoardingGO[currentStep].SetActive(false);
            currentStep++;
        });

        onBoardingGO[currentStep + 1].SetActive(true);
        onBoardingStepIndicator[currentStep + 1].sprite = fullIndicator;
        float y = onBoardingGO[currentStep + 1].transform.position.y;
        onBoardingGO[currentStep + 1].transform.position = new Vector3(Screen.width, y, 0);
        onBoardingGO[currentStep + 1].transform.DOMoveX(Screen.width / 2, 0.3f);
    }

    void PreviousStep()
    {
        if (currentStep <= 0) return;

        // using DOTween to animate the swap between the current and previous onboarding step
        onBoardingStepIndicator[currentStep].sprite = emptyIndicator;
        onBoardingGO[currentStep].transform.DOMoveX(Screen.width * 2, 0.3f).OnComplete(() =>
        {
            onBoardingGO[currentStep].SetActive(false);
            currentStep--;
        });

        onBoardingGO[currentStep - 1].SetActive(true);
        onBoardingStepIndicator[currentStep - 1].sprite = fullIndicator;
        float y = onBoardingGO[currentStep - 1].transform.position.y;
        onBoardingGO[currentStep - 1].transform.position = new Vector3(-Screen.width, y, 0);
        onBoardingGO[currentStep - 1].transform.DOMoveX(Screen.width / 2, 0.3f);
    }
}