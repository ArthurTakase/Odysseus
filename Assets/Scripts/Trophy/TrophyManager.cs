using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TrophyManager : MonoBehaviour
{
    public static TrophyManager Instance;
    [SerializeField] private List<Trophy> trophies = new();
    [SerializeField] private Color32 bronzeColor = new(205, 127, 50, 255);
    [SerializeField] private Color32 silverColor = new(192, 192, 192, 255);
    [SerializeField] private Color32 goldColor = new(255, 215, 0, 255);
    [SerializeField] private GameObject trophyNotification;
    [SerializeField] private TextMeshProUGUI trophyNotificationText;
    private Vector3 trophyNotificationInitialPosition;
    private Coroutine trophyNotificationCoroutine;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        trophyNotificationInitialPosition = trophyNotification.transform.position;
        trophyNotification.SetActive(false);

        foreach (Trophy trophy in trophies)
        {
            if (PlayerPrefs.HasKey("T_" + trophy.trophyName.ToString()))
            {
                trophy.trophyState = TrophyState.Unlocked;

                switch (trophy.trophyType)
                {
                    case TrophyType.Bronze: trophy.trophyImage.color = bronzeColor; break;
                    case TrophyType.Silver: trophy.trophyImage.color = silverColor; break;
                    case TrophyType.Gold: trophy.trophyImage.color = goldColor; break;
                }
            }
        }
    }

    public void UnlockTrophy(TrophyName trophyName)
    {
        Trophy trophy = trophies.Find(t => t.trophyName == trophyName);
        if (trophy == null) return;

        if (trophy.trophyState == TrophyState.Unlocked) return;
        trophy.trophyState = TrophyState.Unlocked;

        PlayerPrefs.SetInt("T_" + trophyName.ToString(), 1);

        switch (trophy.trophyType)
        {
            case TrophyType.Bronze: trophy.trophyImage.color = bronzeColor; break;
            case TrophyType.Silver: trophy.trophyImage.color = silverColor; break;
            case TrophyType.Gold: trophy.trophyImage.color = goldColor; break;
        }

        MyVibration.MediumVibration();

        AudioManager.Instance.Play(AudioManager.Instance.trophy, 0.3f);

        if (trophyNotificationCoroutine != null) StopCoroutine(trophyNotificationCoroutine);
        trophyNotificationCoroutine = StartCoroutine(ShowTrophyNotification(trophy));
    }

    public IEnumerator ShowTrophyNotification(Trophy trophy)
    {
        trophyNotificationText.text = trophy.trophyTitle.text;

        trophyNotification.SetActive(true);
        trophyNotification.transform.position = trophyNotificationInitialPosition + Vector3.up * 200;
        trophyNotification.transform
            .DOMove(trophyNotificationInitialPosition, 0.5f)
            .SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(2f);

        trophyNotification.transform
            .DOMove(trophyNotificationInitialPosition + Vector3.up * 200, 0.5f)
            .SetEase(Ease.InBack)
            .OnComplete(() => trophyNotification.SetActive(false));
    }
}