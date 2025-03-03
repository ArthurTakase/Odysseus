using DG.Tweening;
using UnityEngine;

public class HelpPanel : MonoBehaviour
{
    public void Toggle(bool active)
    {
        Debug.Log("Toggle HelpPanel " + PlayerPrefs.GetInt("showHelp", 0));
        if (PlayerPrefs.GetInt("showHelp", 0) == 0) return;

        if (active) Open();
        else Close();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
}