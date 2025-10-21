using UnityEngine;

public class PlateForDoors : MonoBehaviour
{
  public GameObject[] doors;
  [SerializeField] private Vector3 closedRotation;
  [SerializeField] private Vector3 openedRotation;
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("DeadBody") || other.CompareTag("Player"))
    {
      
    }
  }

  private void OpenDoor()
  {

  }
}
