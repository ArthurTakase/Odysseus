using UnityEngine;

public class TargetObject : MonoBehaviour
{
    TargetIndicator targetIndicator;
    public Sprite targetSprite;

    private void Start()
    {
        targetIndicator = TargetManager.Instance.AddTargetIndicator(this.gameObject, targetSprite);
    }

    private void OnDisable()
    {
        if (targetIndicator != null)
            targetIndicator.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (targetIndicator != null)
            targetIndicator.gameObject.SetActive(true);
    }
}