using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GM_Instance;

    public GameObject Player;
    public GameObject Camera;

    public PlayerController PlayerController;

    public GameObject DialogueBox;
    [SerializeField] private TextMeshProUGUI _deathClockText;
    public float DeathClockTime = 300f; // 5 minutes to finish game

  private void Awake() => GM_Instance = this;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Camera = GameObject.FindWithTag("MainCamera");
        PlayerController =  Player.GetComponent<PlayerController>();
        _deathClockText = GameObject.Find("DeathClockText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        DeathClock();
        BackToTitle();
    }

    private bool _saveCalled = false;
    private void DeathClock()
    {
        DeathClockTime -= Time.deltaTime;

        if (GM_Instance.DeathClockTime <= 0f && !_saveCalled)
        {
            _saveCalled = true;
            PlayerPrefs.SetString("ending", "bad");
            PlayerPrefs.Save();
            SceneManager.LoadScene("Menu");
        }
        
        DeathClockTime = Mathf.Clamp(DeathClockTime, 0f, 300f);
        
        int minutes = Mathf.FloorToInt(DeathClockTime / 60f);
        int seconds = Mathf.FloorToInt(DeathClockTime % 60f);

        string formatTime = $"{minutes:0}:{seconds:00}";
        
        _deathClockText.text = formatTime;
    }

    private void BackToTitle()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("Menu");
    }

    public GameObject Create(GameObject obj, Transform parent)
    {
        return Instantiate(obj, parent);
    }

    public void Destroy(GameObject obj)
    {
        UnityEngine.MonoBehaviour.Destroy(obj);
    }
}
