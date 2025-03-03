using DG.Tweening;
using UnityEngine;

public class Aster : MonoBehaviour
{
    const float DEFAULT_DURATION = 1.5f;
    const float DEFAULT_DURATION_BREATH = 0.8f;
    const bool DEFAULT_DISABLE_AFTER_ANIMATION = true;
    const float DEFAULT_SCALE_BREATH = 1.2f;
    private Vector3 initialScale;
    [SerializeField] AudioSource audioSource;
    public Vector3 lastPosition = default;
    [SerializeField] GameObject explodeEffectPrefab = default;
    private GameObject explodeEffect = null;

    public void SetupInitialScale()
    {
        initialScale = transform.localScale;
    }

    public void FadeOut(
        float duration = DEFAULT_DURATION,
        bool disableAfterAnimation = DEFAULT_DISABLE_AFTER_ANIMATION,
        AudioClip audioClip = default
    )
    {
        if (!SetupAudioSource()) return;
        gameObject.SetActive(true);
        gameObject.transform.localScale = initialScale;
        AudioManager.Instance.Play(audioSource, audioClip != default ? audioClip : AudioManager.Instance.transitionBase, false);
        gameObject.transform.DOScale(Vector3.zero, duration).SetEase(Ease.InOutBack).OnComplete(() =>
        {
            if (disableAfterAnimation) gameObject.SetActive(false);
            if (explodeEffect != null) Destroy(explodeEffect);
        });
    }

    public void FadeIn(float duration = DEFAULT_DURATION, AudioClip audioClip = default)
    {
        if (!SetupAudioSource()) return;
        gameObject.SetActive(true);
        gameObject.transform.localScale = Vector3.zero;
        gameObject.transform.DOScale(initialScale, duration).SetEase(Ease.InOutBack);
        MyVibration.SmallVibration();
        AudioManager.Instance.Play(audioSource, audioClip != default ? audioClip : AudioManager.Instance.smallSweep, true);

        if (explodeEffectPrefab != default)
        {
            if (explodeEffect != null) Destroy(explodeEffect);
            explodeEffect = Instantiate(explodeEffectPrefab, transform.position, Quaternion.identity);
            Destroy(explodeEffect, duration);
        }
    }

    public void Breath(
        float duration = DEFAULT_DURATION_BREATH,
        float scale = DEFAULT_SCALE_BREATH
    )
    {
        gameObject.transform.localScale = initialScale;
        gameObject.transform.DOScale(initialScale * scale, duration).SetEase(Ease.InOutBack) // scale up
            .OnComplete(() => gameObject.transform.DOScale(initialScale, duration).SetEase(Ease.InOutBack)); // scale down
    }

    public void ResetScale(float duration = DEFAULT_DURATION)
    {
        gameObject.transform.DOScale(initialScale, duration).SetEase(Ease.InOutBack);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

    public bool HasMoved(float tolerance)
    {
        if (lastPosition == default
            || Vector3.Distance(lastPosition, transform.position) > tolerance)
        {
            lastPosition = transform.position;
            return true;
        }

        return false;
    }

    public void ResetLastPosition()
    {
        lastPosition = default;
    }

    private bool SetupAudioSource()
    {
        if (audioSource == default) audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == default)
        {
            Debug.LogError("AudioSource not found in Aster");
            return false;
        }
        return true;
    }
}