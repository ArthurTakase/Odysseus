using DG.Tweening;
using UnityEngine;

public class Credits : MonoBehaviour
{
    private GameObject _credits;
    private Vector3 _initialPosition;
    private float _height = 0f;
    public float speed = 1f;
    public bool isFinished = false;
    public GameObject background;

    private void Awake()
    {
        _credits = gameObject;
        _initialPosition = _credits.transform.position;
        _height = _credits.GetComponent<RectTransform>().rect.height;
        background.SetActive(false);
    }

    public void OnEnable()
    {
        background.SetActive(true);
        background.transform.localScale = Vector3.zero;
        background.transform
            .DOScale(Vector3.one, 1f)
            .SetEase(Ease.InOutSine);

        _credits.transform.position = _initialPosition;
        isFinished = false;
    }

    public void OnDisable()
    {
        background.transform.localScale = Vector3.one;
        background.transform
            .DOScale(Vector3.zero, 1f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => background.SetActive(false));

        _credits.transform.position = _initialPosition;
        isFinished = false;
    }

    private void Update()
    {
        _credits.transform.position += Vector3.up * speed * Time.deltaTime;

        if (_credits.transform.position.y >= Screen.height + (_height * 1.5f))
        {
            isFinished = true;
            _credits.transform.position = _initialPosition;
            _credits.SetActive(false);
        }
    }
}