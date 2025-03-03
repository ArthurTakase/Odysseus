using UnityEngine;

public class OpenOnFirstTime : MonoBehaviour
{
    public void Start()
    {
        Debug.Log("FirstTime : " + PlayerPrefs.GetInt("isFirstTime", 0));
        bool isFirstTime = PlayerPrefs.GetInt("isFirstTime", 0) != 1;
        if (!isFirstTime)
        {
            gameObject.SetActive(false);
            return;
        }
    }
}