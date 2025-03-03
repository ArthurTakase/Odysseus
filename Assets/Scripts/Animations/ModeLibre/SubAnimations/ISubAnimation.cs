using UnityEngine;

public class ISubAnimation : MonoBehaviour
{
    public FreeModeEventType eventType = FreeModeEventType.None;

    public virtual bool AreConditionsMet(ResultsData resultsData)
    {
        return false;
    }

    public virtual void Stop(bool soft = false)
    {

    }

    public virtual void Play()
    {

    }
}