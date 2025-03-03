using UnityEngine;
using UnityEngine.UI;

public class MyVibration : MonoBehaviour
{
    private static bool vibrationEnabled = true;
    [SerializeField] private Toggle vibrationToggle;

    public void Awake()
    {
        Vibration.Init();

        if (PlayerPrefs.HasKey("Vibration")) vibrationEnabled = PlayerPrefs.GetInt("Vibration") == 1;
        else PlayerPrefs.SetInt("Vibration", 1);

        vibrationToggle.isOn = vibrationEnabled;
    }

    public static void SmallVibration()
    {
        if (!vibrationEnabled) return;
        Vibration.VibratePop();
    }

    public static void MediumVibration()
    {
        if (!vibrationEnabled) return;
        Vibration.Vibrate();
    }

    public void ToggleVibration()
    {
        vibrationEnabled = !vibrationEnabled;
        vibrationToggle.isOn = vibrationEnabled;
        PlayerPrefs.SetInt("Vibration", vibrationEnabled ? 1 : 0);
    }
}