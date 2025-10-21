using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public enum InteractionType
{
  Button,
  Toggle,
  Plate,
  Note,
  ElectricBox,
  Pillar,
  Dialogue,
  Bowl
}

public interface IActivatable
{
  void Activate();

}

public class InteractableObject : MonoBehaviour, IInteractable
{
  private PlayerInteraction playerInteraction;

  public string interactionPrompt;

  public InteractionType interactionType = InteractionType.Button;

  public GameObject targetObject;

  public UnityEvent onInteract;

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      playerInteraction = other.GetComponent<PlayerInteraction>();
      playerInteraction.ShowPrompt(this);
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player") && playerInteraction != null)
    {
      playerInteraction.HidePrompt(this);
      playerInteraction = null;
    }
  }

  public void Interact()
  {
    // maybe foreach ????

    switch(interactionType)
    {
      case InteractionType.Button:
        ActivateProp();
        break;
      case InteractionType.Toggle:
        ActivateProp();
        break;
      case InteractionType.Plate:
        ActivateProp();
        break;
      case InteractionType.Note:
        ActivateProp();
        break;
      case InteractionType.ElectricBox:
        ActivateProp();
        break;
      case InteractionType.Pillar:
        ActivateProp();
        break;
      case InteractionType.Dialogue:
        ActivateDialogue();
        break;
      case InteractionType.Bowl:
        ActivateProp();
        break;
    }
    onInteract?.Invoke();
  }

  public string GetPrompt()
  {
    return interactionPrompt;
  }

  public Vector3 GetPosition()
  {
    return transform.position;
  }

  private void ActivateProp()
  {
    if (targetObject != null)
    {
      IActivatable activatable = targetObject.GetComponent<IActivatable>();
      activatable?.Activate();
      Debug.Log($"Prop activated: {targetObject.name}");
    }
  }
  private void ActivateDialogue()
  {
    if (targetObject != null)
    {
      DialogueSystem dialogueSystem = targetObject.GetComponent<DialogueSystem>();
      dialogueSystem?.Activate();
      Debug.Log($"Dialogue activated: {targetObject.name}");
    }
  }

}
