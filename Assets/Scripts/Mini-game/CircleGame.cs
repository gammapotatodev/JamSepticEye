using UnityEngine;
using static DynamicInstancer;
/// <summary>
/// Dirty code and game sucks, but it works. Will update it.
/// Press F when playing
/// </summary>

public class CircleGame : MonoBehaviour, IActivatable
{
  public GameObject[] bulbs;

  public GameObject time;

  public GameObject key;

  public void Activate()
  {
    foreach(GameObject bulb in bulbs)
    {
      bulb.SetActive(false);
    }

    time.SetActive(true);

    key.SetActive(true);


  }
}
