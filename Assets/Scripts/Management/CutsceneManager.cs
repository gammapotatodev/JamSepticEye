using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
  public VideoPlayer videoPlayer;
  public VideoClip[] cutscenes;
  public GameObject uiCanvas;
  private int currentCutsceneIndex = 0;


  void Start()
  {
    videoPlayer.loopPointReached += EndReached;
    if (uiCanvas != null)
      uiCanvas.SetActive(true); //= true;
    
    string ending = PlayerPrefs.GetString("ending");
    if (ending == "intro")
    {
      PlayCutscene(0);
      PlayerPrefs.DeleteAll();
    }
    if (ending == "good")
    {
      PlayCutscene(1);
      PlayerPrefs.DeleteAll();
    }
    else if (ending == "bad")
    {
      PlayCutscene(2);
      PlayerPrefs.DeleteAll();
    }
  }

  public void PlayCutscene(int index)
  {
    if (index < 0 || index >= cutscenes.Length) return;

    currentCutsceneIndex = index;
    videoPlayer.clip = cutscenes[index];
    videoPlayer.Prepare();
    videoPlayer.prepareCompleted += OnPrepareCompleted;

    if (uiCanvas != null)
      uiCanvas.SetActive(false);

    Time.timeScale = 0f;
  }

  public void SkipCutscene()
  {
    if (videoPlayer.isPlaying)
    {
      videoPlayer.Stop();
      EndReached(videoPlayer);
    }
  }

  private void OnPrepareCompleted(VideoPlayer vp)
  {
    vp.prepareCompleted -= OnPrepareCompleted; 
    vp.Play();
  }

  private void EndReached(VideoPlayer vp)
  {
    Time.timeScale = 1f;
    if (vp != null)
    {
      vp.Stop();              
      vp.targetTexture = null; 
    }
    if (uiCanvas != null)
      uiCanvas.SetActive(true);
    Debug.Log("Cutscene ended!");
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.F))
    {
      SkipCutscene();
    }
  }

}
