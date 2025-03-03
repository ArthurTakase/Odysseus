using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TrophyType
{
    Bronze,
    Silver,
    Gold
}

public enum TrophyState
{
    Locked,
    Unlocked
}

public enum TrophyName
{
    MoveMoon,
    DisableLights,
    MoonInLight,
    Alligned,
    BlueMoon,
    BloodMoon,
    TotalEclipse,
    ListenAudio
}

public class Trophy : MonoBehaviour
{
    public TrophyType trophyType;
    public TrophyState trophyState;
    public TrophyName trophyName;
    [HideInInspector] public Image trophyImage;
    [HideInInspector] public TextMeshProUGUI trophyTitle;

    private void Awake()
    {
        GameObject icon = transform.Find("Icon").gameObject;
        trophyImage = icon.GetComponent<Image>();

        GameObject titleParent = transform.Find("subcontainer").gameObject;
        GameObject title = titleParent.transform.Find("subtitle").gameObject;
        trophyTitle = title.GetComponent<TextMeshProUGUI>();
    }
}