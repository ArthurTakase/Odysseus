using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button successBtn;
    [SerializeField] private GameObject successPanel;
    [SerializeField] private GameObject middleScreenUiIndicator;

    [Header("UI Infos")]
    [SerializeField] private GameObject infosParent;
    [SerializeField] private TextMeshProUGUI angleText;
    [SerializeField] private Image moonHeightImage;
    [SerializeField] private Sprite moonHeight_same;
    [SerializeField] private Sprite moonHeight_near;
    [SerializeField] private Sprite moonHeight_near_top;
    [SerializeField] private Sprite moonHeight_far;
    [SerializeField] private Sprite moonHeight_far_top;
    [SerializeField] private Image alignmentImage;
    [SerializeField] private Sprite alignment_earth_moon_sun;
    [SerializeField] private Sprite alignment_earth_sun_moon;
    [SerializeField] private Sprite alignment_moon_earth_sun;
    [SerializeField] private Sprite notAligned;

    private Vector3 successBtnInitialPosition;

    private void Start()
    {
        successBtnInitialPosition = successBtn.gameObject.transform.position;
    }

    public void Reset()
    {
        middleScreenUiIndicator.SetActive(false);
        infosParent.SetActive(false);

        successBtn.gameObject.transform.DOMove(successBtnInitialPosition + Vector3.right * 100, 0f);
        successBtn.gameObject.SetActive(false);
    }

    public void Launch()
    {
        infosParent.SetActive(true);
        middleScreenUiIndicator.SetActive(true);

        // Success btn
        InfosPanel.Instance.SetTrophyBtnInitialPosition(successBtnInitialPosition);
        successBtn.onClick.RemoveAllListeners();
        successBtn.onClick.AddListener(() =>
        {
            InfosPanel.Instance.SetContent(successPanel);
            InfosPanel.Instance.Open();
        });

        successBtn.gameObject.SetActive(true);
        successBtn.gameObject.transform.position = successBtnInitialPosition + Vector3.right * 100;
        successBtn.gameObject.transform
            .DOMove(successBtnInitialPosition, 0.5f)
            .SetEase(Ease.OutBack);
    }

    public void UpdateText(AnimationData data)
    {
        angleText.text = $"{Math.Round(data.angle)}°";
    }

    public void UpdateUIPictures(ResultsData resultsData)
    {
        // Set picture for moon height
        if (resultsData.isMoonSameHeightAsEarth) moonHeightImage.sprite = moonHeight_same;
        else if (resultsData.isMoonSameHeightAsEarthForBlueMoon) moonHeightImage.sprite = resultsData.isMoonOnTopOfEarth ? moonHeight_near_top : moonHeight_near;
        else moonHeightImage.sprite = resultsData.isMoonOnTopOfEarth ? moonHeight_far_top : moonHeight_far;

        // Set picture for alignment
        if (!resultsData.isMoonEarthSunAlligned) alignmentImage.sprite = notAligned;
        else
        {
            if (resultsData.isMoonBetweenEarthSun) alignmentImage.sprite = alignment_earth_moon_sun;
            else if (resultsData.isEarthBetweenMoonSun) alignmentImage.sprite = alignment_moon_earth_sun;
            else alignmentImage.sprite = alignment_earth_sun_moon;
        }
    }
}