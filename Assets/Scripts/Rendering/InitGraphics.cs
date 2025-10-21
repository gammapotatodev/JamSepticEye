// fml - sso

using UnityEngine;

public class InitGraphics : MonoBehaviour
{
    void Start()
    {
        PlatformGraphicAdjustment();
    }

    private void PlatformGraphicAdjustment()
    {
#if UNITY_STANDALONE_LINUX
        Linux();
#endif
        
#if UNITY_STANDALONE_OSX
        Mac();
#endif
        
#if UNITY_STANDALONE_WIN
        Windows();
#endif
        
#if UNITY_WEBGL
        Web();
#endif
    }

    private void Linux() => PlayerPrefs.SetString("platform", "linux");
    
    private void Mac() => PlayerPrefs.SetString("platform", "mac");

    private void Windows() => PlayerPrefs.SetString("platform", "windows");

    private void Web() => PlayerPrefs.SetString("platform", "web");
}
