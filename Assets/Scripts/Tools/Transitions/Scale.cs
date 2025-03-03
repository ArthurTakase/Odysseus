using DG.Tweening;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform
            .DOScale(Vector3.one, 0.5f)
            .SetEase(Ease.InOutExpo);
    }

    public void OnDisable()
    {
        transform
            .DOScale(Vector3.zero, 0.5f)
            .SetEase(Ease.InOutExpo);
    }
}