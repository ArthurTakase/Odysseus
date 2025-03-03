using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Copilote : MonoBehaviour
{
    [SerializeField] private Toggle toggle;

    public void Start()
    {
        bool showHelp = PlayerPrefs.GetInt("showHelp", 0) == 0;
        bool isFirstTime = PlayerPrefs.GetInt("isFirstTime", 0) == 0;
        toggle.isOn = showHelp && !isFirstTime;
    }

    public void SetHelp(bool active)
    {
        PlayerPrefs.SetInt("showHelp", active ? 1 : 0);
        toggle.isOn = active;
    }

    public void SetHelpFirstTime(bool active)
    {
        SetHelp(active);
        PlayerPrefs.SetInt("isFirstTime", 1);
        if (!active) return;

        OnBoarding.Instance.openBtn.SetActive(false);
        OnBoarding.Instance.transform
            .DOMoveY(-Screen.height, 0f)
            .OnComplete(() => OnBoarding.Instance.Open(true));
    }
}