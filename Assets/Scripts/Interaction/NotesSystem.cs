using UnityEngine;

public class NotesSystem : MonoBehaviour, IActivatable
{
  public GameObject letterImage;
  private GameObject instantiatedLetter;


  public void Activate()
  {
    if (letterImage != null && instantiatedLetter == null)
    {
      Canvas canvas = FindFirstObjectByType<Canvas>();
      if (canvas != null)
      {
        instantiatedLetter = Instantiate(letterImage, canvas.transform);
        RectTransform rectTransform = instantiatedLetter.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
          rectTransform.anchoredPosition = Vector2.zero;
          rectTransform.localScale = Vector3.one;
        }
      }
    }
    else
    {
      Destroy(instantiatedLetter);
      instantiatedLetter = null;
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if(other.CompareTag("Player"))
    {
      Destroy(instantiatedLetter);
      instantiatedLetter = null;
    }
  }
}
