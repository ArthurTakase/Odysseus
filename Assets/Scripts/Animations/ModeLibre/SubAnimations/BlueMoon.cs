using UnityEngine;

public class BlueMoon : ISubAnimation
{
    [SerializeField] private Aster moon;
    [SerializeField] private WorldSpaceTag BlueMoonWorldSpaceTag;
    [SerializeField] private GameObject ParticleSystemBlueMoon;
    [SerializeField] private GameObject infosPanel;
    [SerializeField] private AudioClip audioClip;
    private readonly SubtitlesData subtitlesData = new();

    public override bool AreConditionsMet(ResultsData resultsData)
    {
        return resultsData.isMoonEarthSunAlligned
                && resultsData.isEarthBetweenMoonSun
                && resultsData.isMoonSameHeightAsEarthForBlueMoon;
    }

    public override void Stop(bool soft = false)
    {
        BlueMoonWorldSpaceTag.Toggle(false);
        ParticleSystemBlueMoon.SetActive(false);
        moon.ResetScale(1.5f);
        if (!soft)
        {
            AnimationManager.Instance.ToggleInfosButton(false);
            AnimationManager.Instance.ToggleSoundButton(false);
            InfosPanel.Instance.Close();
        }
    }

    public override void Play()
    {
        BlueMoonWorldSpaceTag.Toggle(true);
        ParticleSystemBlueMoon.SetActive(true);
        moon.Breath();
        TrophyManager.Instance.UnlockTrophy(TrophyName.BlueMoon);

        AnimationManager.Instance.SetInfosBtnAction(infosPanel);
        AnimationManager.Instance.ToggleInfosButton(true);
        AnimationManager.Instance.SetSoundBtnAction(audioClip, subtitlesData.BLUEMOON);
        AnimationManager.Instance.ToggleSoundButton(true);
    }
}