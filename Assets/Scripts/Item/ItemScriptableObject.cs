using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Scriptable Object", menuName = "Scriptable Objects/Item")]
public class ItemScriptableObject : ScriptableObject
{
    public string scriptableObjectName;
    public Sprite scriptableObjectIcon;
    public float buyPrice;
    public float sellPrice;
}
