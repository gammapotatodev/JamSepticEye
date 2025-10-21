using System.Collections;
using UnityEngine;

public class DoorSystem : MonoBehaviour, IActivatable
{
  [SerializeField] private Vector3 closedRotation;
  [SerializeField] private Vector3 openedRotation;

  private bool isOpen = false;

  public float moveSpeed = 2f;

  private void Awake()
  {
    foreach (Transform child in transform)
      child.gameObject.isStatic = false;

    gameObject.isStatic = false;
  }

  private void Start()
  {
    closedRotation = transform.eulerAngles;
    //foreach (Transform child in transform)
    //{
    //  child.gameObject.isStatic = false;
    //}
  }

  public void Activate()
  {

    isOpen = !isOpen;
    Vector3 targetRotation = isOpen? openedRotation : closedRotation;
    StartCoroutine(MoveDoor(targetRotation));
  }

  private IEnumerator MoveDoor(Vector3 targetRotation)
  {
    Quaternion targetQuat = Quaternion.Euler(targetRotation);
    Quaternion startQuat = transform.rotation;

    float time = 0f;
    while (time < 1f)
    {
      time += Time.deltaTime * moveSpeed;
      transform.rotation = Quaternion.Lerp(startQuat, targetQuat, time);
      yield return null;
    }
    transform.rotation = targetQuat;
  }

}
