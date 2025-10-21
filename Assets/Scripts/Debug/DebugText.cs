using TMPro;
using UnityEngine;
using static GameManager;

public class DebugText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugText;
    private string _text;
    
    void Update()
    {
        var fps = FPS().ToString("F0");
        var pPos = "x,"+PlayerPosition().x.ToString("F0")+" y,"+PlayerPosition().y.ToString("F0")+" z,"+PlayerPosition().z.ToString("F0");
        var cPos = "x,"+CameraPosition().x.ToString("F0")+" y,"+PlayerPosition().y.ToString("F0")+" z,"+PlayerPosition().z.ToString("F0");
        var form = PlayersForm().ToString();
        var time = GhostLastingTime().ToString("F2");
        
        _text = $"fps / {fps} \nplrPos / {pPos} \ncamPos / {cPos} \nform / {form} \nformTime / {time}";
        
        debugText.text = _text;
    }

    private float FPS() { return 1.0f / Time.deltaTime; }

    private Vector3 PlayerPosition() { return GM_Instance.Player.transform.position; }
    private Vector3 CameraPosition() { return GM_Instance.Camera.transform.position; }

    private PlayerForm PlayersForm() { return GM_Instance.PlayerController.PlayerForm; }

    private float GhostLastingTime() { return GM_Instance.PlayerController.ghostLastingTime; }
}
