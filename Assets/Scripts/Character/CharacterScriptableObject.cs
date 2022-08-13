using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Scriptable Object", menuName = "Scriptable Objects/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    public new string name;
    public Sprite avatar;
}
