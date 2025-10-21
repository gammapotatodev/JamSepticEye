using UnityEngine;
using static DialogueManager;

public class DialogueTest : MonoBehaviour
{
    [SerializeField] private Dialogue _debugDialogue;
    private DialogueBox box;
    
    private int index = 0;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            box = CreateBox(); // creates an empty dialogue box, you need to fill its contents
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RefreshDialogueBox(box, _debugDialogue, index); // updates the content inside the dialogue box
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            index++; // add 1 to the index, pushes it through the dialogue list and displays new contents
            RefreshDialogueBox(box, _debugDialogue, index);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            FinishDialogue(box); // this ends the dialogue and destroys the box you made
        }
    }
}
