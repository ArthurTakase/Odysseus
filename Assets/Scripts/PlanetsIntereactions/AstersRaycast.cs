using UnityEngine;
using System;

public class AstersRaycast : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private new Camera camera;
    public Action action;

    [Header("Sun")]
    [SerializeField] private CapsuleCollider sunCollider;
    [SerializeField] private GameObject _sunShield;
    [SerializeField] private GameObject _sunHelp;

    [Header("Moon")]
    [SerializeField] private CapsuleCollider moonCollider;
    [SerializeField] private GameObject _moonShield;
    [SerializeField] private Transform _moon;
    [SerializeField] private GameObject _moonHelp;

    private bool _isRaycastColliding;
    private float _initialY;
    private RaycastHit hit;
    private bool isMoonHit = false;
    private bool isSunHit = false;
    private float lastVibrationTime;
    public static AstersRaycast Instance;

    private void Awake()
    {
        Instance = this;
    }


    public void Start()
    {
        _initialY = _moon.position.y;
    }

    public bool IsRaycastColliding()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        bool isHit = Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask);

        if (!isHit)
        {
            isMoonHit = false;
            isSunHit = false;
            return false;
        }

        isMoonHit = hit.collider.name == moonCollider.name;
        isSunHit = hit.collider.name == sunCollider.name;
        return isHit;
    }

    public void Update()
    {
        if (InfosPanel.Instance.isOpen)
        {
            if (_moonShield.activeSelf) _moonShield.SetActive(false);
            if (_sunShield.activeSelf) _sunShield.SetActive(false);
            return;
        }

        _isRaycastColliding = IsRaycastColliding();
        bool needHelp = PlayerPrefs.GetInt("showHelp", 0) == 1;

        if (_moonShield.activeSelf != isMoonHit)
        {
            _moonShield.SetActive(isMoonHit);
            if (needHelp && !_moonHelp.activeSelf) _moonHelp.SetActive(isMoonHit);
            else if (_moonHelp.activeSelf) _moonHelp.SetActive(false);
        }

        if (_sunShield.activeSelf != isSunHit)
        {
            _sunShield.SetActive(isSunHit);
            if (needHelp && !_sunHelp.activeSelf) _sunHelp.SetActive(isSunHit);
            else if (_sunHelp.activeSelf) _sunHelp.SetActive(false);
        }

        if (_isRaycastColliding)
        {
            if (isMoonHit) MoonAction();
            if (isSunHit) SunAction();
        }
    }

    private void MoonAction()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 newPosition = _moonShield.transform.position;
                newPosition.y += touch.deltaPosition.y * 0.001f * Time.deltaTime;
                _moon.position = newPosition;

                float currentTime = Time.time;
                if (currentTime - lastVibrationTime > 0.2f)
                {
                    MyVibration.SmallVibration();
                    lastVibrationTime = currentTime;
                }

                if (_moon.position.y != _initialY)
                    TrophyManager.Instance.UnlockTrophy(TrophyName.MoveMoon);
            }
        }
    }

    private void SunAction()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            action?.Invoke();
    }
}