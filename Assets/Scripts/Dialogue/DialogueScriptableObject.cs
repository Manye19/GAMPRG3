using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogues Scriptable Object", menuName = "Scriptable Objects/Dialogues")]
public class DialogueScriptableObject : ScriptableObject
{
    [NonReorderable] public List<Dialogue> dialogues = new List<Dialogue>();
}
