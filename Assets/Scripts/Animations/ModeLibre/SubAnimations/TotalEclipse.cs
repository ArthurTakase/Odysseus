using UnityEngine;

public class TotalEclipse : ISubAnimation
{
    [SerializeField] private Aster moon;
    [SerializeField] private WorldSpaceTag TotalEclipseWorldSpaceTag;
    [SerializeField] private GameObject ParticleSystemTotalEclipse;
    [SerializeField] private GameObject infosPanel;
    [SerializeField] private AudioClip audioClip;
    private readonly SubtitlesData subtitlesData = new();

    public override bool AreConditionsMet(ResultsData resultsData)
    {
        return resultsData.isMoonSameHeightAsEarth
                && resultsData.isMoonBetweenEarthSun
                && resultsData.isMoonEarthSunAlligned;
    }

    public override void Stop(bool soft = false)
    {
        TotalEclipseWorldSpaceTag.Toggle(false);
        ParticleSystemTotalEclipse.SetActive(false);
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
        TotalEclipseWorldSpaceTag.Toggle(true);
        ParticleSystemTotalEclipse.SetActive(true);
        moon.Breath();
        TrophyManager.Instance.UnlockTrophy(TrophyName.TotalEclipse);

        AnimationManager.Instance.SetInfosBtnAction(infosPanel);
        AnimationManager.Instance.ToggleInfosButton(true);
        AnimationManager.Instance.SetSoundBtnAction(audioClip, subtitlesData.TOTALECLIPSE);
        AnimationManager.Instance.ToggleSoundButton(true);
    }
}