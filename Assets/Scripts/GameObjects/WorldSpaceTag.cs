using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class WorldSpaceTag : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image image;
    public GameObject container;
    public new Camera camera;
    private Vector3 defaultScale;

    private void Awake()
    {
        defaultScale = container.transform.localScale;
    }

    void Update()
    {
        container.transform.LookAt(camera.transform);
        container.transform.Rotate(0, 180, 0);
    }

    public void UpdateText(string newText)
    {
        text.text = newText;
    }

    public void Toggle(bool active)
    {
        if (active == container.activeSelf) return;

        // use dotween to animate the height going from 0 to default height
        if (active)
        {
            container.SetActive(true);
            container.transform
                .DOScale(defaultScale, 1f)
                .SetEase(Ease.OutBack);
        }
        else
        {
            container.transform
                .DOScale(Vector3.zero, 1f)
                .SetEase(Ease.InBack)
                .OnComplete(() => container.SetActive(false));
        }
    }

    public void HideImediately()
    {
        container.transform
            .DOScale(Vector3.zero, 0f)
            .SetEase(Ease.InBack)
            .OnComplete(() => container.SetActive(false));
    }
}