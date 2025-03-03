using UnityEngine;

public class Alligned : ISubAnimation
{
    [SerializeField] private Aster moon;

    public override bool AreConditionsMet(ResultsData resultsData)
    {
        return resultsData.isMoonEarthSunAlligned;
    }

    public override void Stop(bool soft = false)
    {
        moon.ResetScale(1.5f);
    }

    public override void Play()
    {
        moon.Breath();
        TrophyManager.Instance.UnlockTrophy(TrophyName.Alligned);
    }
}