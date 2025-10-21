using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
  [SerializeField] private Dialogue dialogue;
  private DialogueBox currentDialogueBox;

  public void Activate()
  {
    currentDialogueBox = DialogueManager.CreateBox();

    DialogueManager.RefreshDialogueBox(currentDialogueBox, dialogue, 0);
  }

  public void ContinueDialogue(int index)
  {
    if (currentDialogueBox != null)
    {
      DialogueManager.RefreshDialogueBox(currentDialogueBox, dialogue, index);
    }
  }

  public void EndDialogue()
  {
    if (currentDialogueBox != null)
    {
      DialogueManager.FinishDialogue(currentDialogueBox);
      currentDialogueBox = null;
    }
  }
}
