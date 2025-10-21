using System.Collections;
using UnityEngine;

public class PlateSystem : MonoBehaviour
{
  private bool isActivated = false;

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("DeadBody") || other.CompareTag("Player"))
    {
      isActivated = true;
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if ((other.CompareTag("DeadBody") || other.CompareTag("DeadBody")))
    {
      isActivated = false;
    }
  }

  public bool IsActivated()
  {
    return isActivated;
  }

}
