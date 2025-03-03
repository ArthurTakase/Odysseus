using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Subtitles
{
    public string text;
    public float duration;

    public Subtitles(string text, float duration)
    {
        this.text = text;
        this.duration = duration;
    }
}

public class SubtitlesManager : MonoBehaviour
{
    public static SubtitlesManager Instance;
    [SerializeField] private GameObject subtitlesPanel;
    [SerializeField] private TMPro.TextMeshProUGUI subtitlesText;
    private Coroutine currentSubtitlesCoroutine;
    [HideInInspector] public bool isFinished = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        subtitlesPanel.SetActive(false);
    }

    public void ShowSubtitles(List<Subtitles> subtitles)
    {
        isFinished = false;

        if (currentSubtitlesCoroutine != null)
            StopCoroutine(currentSubtitlesCoroutine);

        subtitlesPanel.SetActive(true);
        currentSubtitlesCoroutine = StartCoroutine(ShowSubtitlesRoutine(subtitles));
    }

    private IEnumerator ShowSubtitlesRoutine(List<Subtitles> subtitles)
    {
        foreach (Subtitles subtitle in subtitles)
        {
            subtitlesText.text = subtitle.text;
            yield return new WaitForSeconds(subtitle.duration);
        }

        subtitlesPanel.SetActive(false);

        isFinished = true;
    }

    public void HideSubtitlesNow()
    {
        if (currentSubtitlesCoroutine != null)
            StopCoroutine(currentSubtitlesCoroutine);

        subtitlesPanel.SetActive(false);

        isFinished = true;
    }
}
