using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SwapGraphics : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {   
        string platform = PlayerPrefs.GetString("platform");

        if (platform == "web") WebGFX();
        else PcGFX();
    }

    private void PcGFX()
    {
        /*var urpData = Camera.main.GetUniversalAdditionalCameraData();
        urpData.SetRenderer(2); // webgl graphics*/
        QualitySettings.SetQualityLevel(1); // pc quality

        Camera.main.farClipPlane = 100f; // clip far
        GameObject.Find("WebGL Light").SetActive(false);
    }

    private void WebGFX()
    {
        /*var urpData = Camera.main.GetUniversalAdditionalCameraData();
        urpData.SetRenderer(2); // webgl graphics*/
        QualitySettings.SetQualityLevel(2); // webgl quality

        Camera.main.farClipPlane = 30f; // clip near
        GameObject.Find("WebGL Light").SetActive(true);
        GameObject.Find("WebGL Light").GetComponent<Light>().enabled = true;
    }
}
