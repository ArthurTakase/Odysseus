using UnityEngine;

public class SunStyle : MonoBehaviour
{
    [SerializeField] private Material defaultSunMaterial;
    [SerializeField] private Material sunMaterialWithShader;
    [SerializeField] private Material sunMaterialWithShaderLow;
    [SerializeField] private Material sunMaterialTexture;
    [SerializeField] private UnityEngine.UI.Slider sunLODSlider;

    [Header("Sun Animation 1")]
    [SerializeField] private GameObject A1SunSphere;
    [SerializeField] private GameObject A1SunSurface;
    [SerializeField] private GameObject A1SunGlow;
    [SerializeField] private GameObject A1SunGlowDark;
    [SerializeField] private GameObject A1SunLoop;

    [Header("Sun Animation 2")]
    [SerializeField] private GameObject A2SunSphere;
    [SerializeField] private GameObject A2SunSurface;
    [SerializeField] private GameObject A2SunGlow;
    [SerializeField] private GameObject A2SunGlowDark;
    [SerializeField] private GameObject A2SunLoop;

    private void Start()
    {
        int sunLOD = PlayerPrefs.GetInt("SunLOD", 0);
        sunLODSlider.value = sunLOD;
        SetSunLOD(sunLOD);
    }

    public void SetSunLOD(float lod)
    {
        PlayerPrefs.SetInt("SunLOD", (int)lod);

        switch (lod)
        {
            case 0: // Aucun détail
                A1SunSurface.SetActive(false);
                A1SunGlow.SetActive(false);
                A1SunGlowDark.SetActive(false);
                A1SunLoop.SetActive(false);

                A2SunSurface.SetActive(false);
                A2SunGlow.SetActive(false);
                A2SunGlowDark.SetActive(false);
                A2SunLoop.SetActive(false);

                A1SunSphere.GetComponent<MeshRenderer>().material = sunMaterialTexture;
                A2SunSphere.GetComponent<MeshRenderer>().material = sunMaterialTexture;
                break;
            // case 1: // Faible
            //     A1SunSurface.SetActive(false);
            //     A1SunGlow.SetActive(false);
            //     A1SunGlowDark.SetActive(false);
            //     A1SunLoop.SetActive(false);

            //     A2SunSurface.SetActive(false);
            //     A2SunGlow.SetActive(false);
            //     A2SunGlowDark.SetActive(false);
            //     A2SunLoop.SetActive(false);

            //     A1SunSphere.GetComponent<MeshRenderer>().material = sunMaterialWithShaderLow;
            //     A2SunSphere.GetComponent<MeshRenderer>().material = sunMaterialWithShaderLow;
            //     break;
            // case 2: // Moyen
            //     A1SunSurface.SetActive(false);
            //     A1SunGlow.SetActive(true);
            //     A1SunGlowDark.SetActive(false);
            //     A1SunLoop.SetActive(true);

            //     A2SunSurface.SetActive(false);
            //     A2SunGlow.SetActive(true);
            //     A2SunGlowDark.SetActive(false);
            //     A2SunLoop.SetActive(true);

            //     A1SunSphere.GetComponent<MeshRenderer>().material = sunMaterialWithShader;
            //     A2SunSphere.GetComponent<MeshRenderer>().material = sunMaterialWithShader;
            //     break;
            case 1: // Tout
                A1SunSurface.SetActive(true);
                A1SunGlow.SetActive(true);
                A1SunGlowDark.SetActive(false);
                A1SunLoop.SetActive(true);

                A2SunSurface.SetActive(true);
                A2SunGlow.SetActive(true);
                A2SunGlowDark.SetActive(false);
                A2SunLoop.SetActive(true);

                A1SunSphere.GetComponent<MeshRenderer>().material = defaultSunMaterial;
                A2SunSphere.GetComponent<MeshRenderer>().material = defaultSunMaterial;
                break;
        }
    }
}