using UnityEngine;

public class BloodMoon : ISubAnimation
{
    [SerializeField] private Aster moon;
    [SerializeField] private WorldSpaceTag BloodMoonWorldSpaceTag;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip animationBloodMoon;
    [SerializeField] private AnimationClip idle;
    [SerializeField] private GameObject infosPanel;
    [SerializeField] private AudioClip audioClip;
    private readonly SubtitlesData subtitlesData = new();

    public override bool AreConditionsMet(ResultsData resultsData)
    {
        return resultsData.isMoonEarthSunAlligned
                && resultsData.isEarthBetweenMoonSun
                && resultsData.isMoonSameHeightAsEarth;
    }

    public override void Stop(bool soft = false)
    {
        BloodMoonWorldSpaceTag.Toggle(false);
        animator.Play(idle.name, 0, 0);
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
        BloodMoonWorldSpaceTag.Toggle(true);
        animator.Play(animationBloodMoon.name, 0, 0);
        animator.speed = 0.5f;
        moon.Breath();
        TrophyManager.Instance.UnlockTrophy(TrophyName.BloodMoon);

        AnimationManager.Instance.SetInfosBtnAction(infosPanel);
        AnimationManager.Instance.ToggleInfosButton(true);
        AnimationManager.Instance.SetSoundBtnAction(audioClip, subtitlesData.BLOODMOON);
        AnimationManager.Instance.ToggleSoundButton(true);
    }
}