using System;
using UnityEngine;
using static GameManager;

public enum ActiveDialogue
{
    Active,
    Inactive
}

/// <summary>
/// Create, Update, Remove Dialogue content
///
/// Developed by sso / 2025
/// </summary>
public static class DialogueManager
{
    /// <summary>
    /// Swap acitve dialogue states. -sso
    /// </summary>
    public static ActiveDialogue ActiveDialogue = ActiveDialogue.Inactive;
    
    /// <summary>
    /// Create a dialogue box / Starts Dialogue -sso
    /// </summary>
    /// <param name="dialogue"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static DialogueBox CreateBox()
    {
        if (ActiveDialogue == ActiveDialogue.Active) return null;

        try
        {
            var box = GM_Instance.Create(GM_Instance.DialogueBox, GameObject.Find("Canvas").transform);
            box.SetActive(true);
        
            ActiveDialogue = ActiveDialogue.Active;
        
            return box.GetComponent<DialogueBox>();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            throw;
        }
    }

    /// <summary>
    /// Update an already created Dialogue Box / Auto Finishes Dialogue at the end of its List -sso
    /// </summary>
    /// <param name="box"></param>
    /// <param name="dialogue"></param>
    /// <param name="index"></param>
    public static void RefreshDialogueBox(DialogueBox box, Dialogue dialogue, int index)
    {
        if (index < dialogue.dialogue.Count)
        {
            box.name.text = dialogue.dialogue[index].name;
            box.dialogue.text = dialogue.dialogue[index].text;
        }
        else FinishDialogue(box);
    }

    /// <summary>
    /// Destroy a DialogueBox / Ends Dialogue -sso
    /// </summary>
    /// <param name="box"></param>
    public static void FinishDialogue(DialogueBox box)
    {
        ActiveDialogue = ActiveDialogue.Inactive;
        GM_Instance.Destroy(box.gameObject);
    }
}
