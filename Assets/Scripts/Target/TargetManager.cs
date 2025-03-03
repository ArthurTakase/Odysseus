using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public RectTransform canvas;
    public List<TargetIndicator> targetIndicators = new();
    public Camera MainCamera;
    public GameObject TargetIndicatorPrefab;
    public static TargetManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (targetIndicators.Count <= 0) return;

        for (int i = 0; i < targetIndicators.Count; i++) targetIndicators[i].UpdateTargetIndicator();
    }

    public TargetIndicator AddTargetIndicator(GameObject target, Sprite targetSprite)
    {
        GameObject targetIndicator = Instantiate(TargetIndicatorPrefab, canvas.transform);
        targetIndicator.transform.SetAsFirstSibling();
        TargetIndicator indicator = targetIndicator.GetComponent<TargetIndicator>();
        indicator.InitialiseTargetIndicator(target, MainCamera, canvas, targetSprite);
        targetIndicators.Add(indicator);

        return indicator;
    }
}