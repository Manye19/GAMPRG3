using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Scriptable Object", menuName = "Scriptable Objects/Item")]
public class SO_Item : ScriptableObject
{
    public string name;
    public Sprite icon;
}