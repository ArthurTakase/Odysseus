using UnityEngine;

public class AnimationTarget : MonoBehaviour
{
    public TargetType targetType;
    private bool isFound = false;

    public void OnTargetFound()
    {
        if (isFound) return;
        isFound = true;
        AnimationManager.Instance.AddTarget(targetType);
    }

    public void OnTargetLost()
    {
        if (!isFound) return;
        isFound = false;
        AnimationManager.Instance.RemoveTarget(targetType);
    }
}