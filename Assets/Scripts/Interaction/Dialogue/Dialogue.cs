using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueGroup
{
    public string name = "name";
    public string text = "hello world";
}

[CreateAssetMenu(fileName = "Dialogue", menuName = "JSE/Dialogue")]
public class Dialogue : ScriptableObject
{
    public List<DialogueGroup> dialogue = new List<DialogueGroup>();
}
