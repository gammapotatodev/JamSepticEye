using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame() //=> SceneManager.LoadScene("CoreScene");
    {
      PlayerPrefs.SetString("ending", "intro");
      PlayerPrefs.Save();
      SceneManager.LoadScene("CoreScene");
    }
  
    
    public void Debug() => SceneManager.LoadScene("Debug");

    public void QuitGame() => Application.Quit();
}
