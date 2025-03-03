using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
    public Image offScreenTargetIndicator;
    public Image iconTargetIndicator;
    public float outOfSightOffset = 20f;
    public Vector3 indicatorPosition;

    private GameObject target;
    private Camera mainCamera;
    private RectTransform canvasRect;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void InitialiseTargetIndicator(GameObject target, Camera mainCamera, RectTransform canvasRect, Sprite targetSprite)
    {
        this.target = target;
        this.mainCamera = mainCamera;
        this.canvasRect = canvasRect;

        iconTargetIndicator.sprite = targetSprite;
    }

    public void UpdateTargetIndicator()
    {
        if (target == null || target.activeSelf == false)
        {
            if (offScreenTargetIndicator.gameObject.activeSelf == true) offScreenTargetIndicator.gameObject.SetActive(false);
            if (iconTargetIndicator.gameObject.activeSelf == true) iconTargetIndicator.gameObject.SetActive(false);
            return;
        }

        SetIndicatorPosition();
    }

    protected void SetIndicatorPosition()
    {
        indicatorPosition = mainCamera.WorldToScreenPoint(target.transform.position);

        //In case the target is both in front of the camera and within the bounds of its frustrum
        if (indicatorPosition.z >= 0f & indicatorPosition.x <= canvasRect.rect.width * canvasRect.localScale.x
         & indicatorPosition.y <= canvasRect.rect.height * canvasRect.localScale.x & indicatorPosition.x >= 0f & indicatorPosition.y >= 0f)
        {
            indicatorPosition.z = 0f;
            TargetOutOfSight(false, indicatorPosition);
        }

        //In case the target is in front of the camera, but out of sight
        else if (indicatorPosition.z >= 0f)
        {
            indicatorPosition = OutOfRangeindicatorPositionB(indicatorPosition);
            TargetOutOfSight(true, indicatorPosition);
        }
        else //In case the target is behind the camera
        {
            indicatorPosition *= -1f;
            indicatorPosition = OutOfRangeindicatorPositionB(indicatorPosition);
            TargetOutOfSight(true, indicatorPosition);
        }

        rectTransform.position = indicatorPosition;

    }

    private Vector3 OutOfRangeindicatorPositionB(Vector3 indicatorPosition)
    {
        indicatorPosition.z = 0f;

        Vector3 canvasCenter = new Vector3(canvasRect.rect.width / 2f, canvasRect.rect.height / 2f, 0f) * canvasRect.localScale.x;
        indicatorPosition -= canvasCenter;

        float divX = (canvasRect.rect.width / 2f - outOfSightOffset) / Mathf.Abs(indicatorPosition.x);
        float divY = (canvasRect.rect.height / 2f - outOfSightOffset) / Mathf.Abs(indicatorPosition.y);

        if (divX < divY)
        {
            float angle = Mathf.Atan2(indicatorPosition.y, indicatorPosition.x); // Calcul de l'angle en radians

            // Ajustement correct de l'axe X
            indicatorPosition.x = Mathf.Sign(indicatorPosition.x) * (canvasRect.rect.width / 2f - outOfSightOffset) * canvasRect.localScale.x;
            indicatorPosition.y = Mathf.Tan(angle) * indicatorPosition.x;
        }
        else
        {
            float angle = Mathf.Atan2(indicatorPosition.y, indicatorPosition.x);

            // Ajustement correct de l'axe Y
            indicatorPosition.y = Mathf.Sign(indicatorPosition.y) * (canvasRect.rect.height / 2f - outOfSightOffset) * canvasRect.localScale.y;
            indicatorPosition.x = indicatorPosition.y / Mathf.Tan(angle);
        }

        indicatorPosition += canvasCenter;
        return indicatorPosition;
    }

    private void TargetOutOfSight(bool oos, Vector3 indicatorPosition)
    {
        if (oos)
        {
            if (offScreenTargetIndicator.gameObject.activeSelf == false) offScreenTargetIndicator.gameObject.SetActive(true);
            if (iconTargetIndicator.gameObject.activeSelf == false) iconTargetIndicator.gameObject.SetActive(true);
            offScreenTargetIndicator.rectTransform.rotation = Quaternion.Euler(RotationOutOfSightTargetindicator(indicatorPosition));
            return;
        }

        if (offScreenTargetIndicator.gameObject.activeSelf == true) offScreenTargetIndicator.gameObject.SetActive(false);
        if (iconTargetIndicator.gameObject.activeSelf == true) iconTargetIndicator.gameObject.SetActive(false);
    }


    private Vector3 RotationOutOfSightTargetindicator(Vector3 indicatorPosition)
    {
        Vector3 canvasCenter = new Vector3(canvasRect.rect.width / 2f, canvasRect.rect.height / 2f, 0f) * canvasRect.localScale.x;
        float angle = Vector3.SignedAngle(Vector3.up, indicatorPosition - canvasCenter, Vector3.forward);
        return new Vector3(0f, 0f, angle);
    }
}