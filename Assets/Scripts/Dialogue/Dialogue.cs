using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public CharacterScriptableObject characterScriptableObject;

    [TextArea]
    public string words;
}
