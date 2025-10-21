using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
  public int number;
  private CutsceneManager cutsceneManager;

  void Start()
  {
    cutsceneManager = FindFirstObjectByType<CutsceneManager>();
    if (cutsceneManager == null)
    {
      Debug.LogError("No CutsceneManager!");
      return;
    }
    cutsceneManager.PlayCutscene(number);
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      cutsceneManager.SkipCutscene();
    }
  }
}
