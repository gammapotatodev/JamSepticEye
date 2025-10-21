using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI interactionText;

  [SerializeField] private Vector2 offset = new Vector2(0, 50);

  private IInteractable currentInteractable;

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
    {
      currentInteractable.Interact();
    }

    if (currentInteractable != null && interactionText != null && interactionText.gameObject.activeSelf)
    {
      UpdatePromptPosition();
    }
  }

  public void ShowPrompt(IInteractable interactable)
  {
    currentInteractable = interactable;

    if (interactionText != null)
    {
      interactionText.gameObject.SetActive(true);
      interactionText.text = interactable.GetPrompt();
      UpdatePromptPosition();
    }
  }

  public void HidePrompt(IInteractable interactable)
  {
    if (currentInteractable == interactable)
    {
      currentInteractable = null;

      if (interactionText != null)
      {
        interactionText.gameObject.SetActive(false);
        interactionText.text = "";
      }
    }
  }

  private void UpdatePromptPosition()
  {
    Vector3 worldPosition = currentInteractable.GetPosition();

    Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

    if (screenPosition.z > 0)
    {
      screenPosition += new Vector3(offset.x, offset.y, 0);

      screenPosition.x = Mathf.Clamp(screenPosition.x, 0, Screen.width);
      screenPosition.y = Mathf.Clamp(screenPosition.y, 0, Screen.height);

      RectTransform textTransform = interactionText.GetComponent<RectTransform>();
      textTransform.position = screenPosition;
    }
    else
    {
      interactionText.gameObject.SetActive(false);
    }
  }

}
