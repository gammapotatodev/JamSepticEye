using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static GameManager;

public class CameraEffects : MonoBehaviour
{
    private Camera _mainCamera;
    
    public Volume volume;
    private LensDistortion lensDistortion;
    
    [SerializeField] private float _zoomSpeed = 10f;
    
    void Start()
    {
        volume = GameObject.Find("Post Processing").GetComponent<Volume>();
        
        if (volume != null && volume.profile.TryGet(out lensDistortion))
        {
            // Ensure it's overridden so it can be changed at runtime
            lensDistortion.intensity.overrideState = true;
        }
        
        _mainCamera = Camera.main;
    }
    
    void Update()
    {
        UpdateFOV();
    }
    
    public void SetDistortion(float value, float speed)
    {
        if (lensDistortion != null)
        {
            lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value,  value, Time.deltaTime * speed);
        }
    }
    
    private void UpdateFOV()
    {
        if (GM_Instance.PlayerController.PlayerForm == PlayerForm.Ghost)
        {
            SetDistortion(-0.7f, 0.7f);
        }
        else if (GM_Instance.PlayerController.PlayerForm == PlayerForm.Returning)
        {
            SetDistortion(0f, 3f);
        }
        else
        {
            SetDistortion(0f, 3f);
        }
    }
}
