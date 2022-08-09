using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crop Scriptable Object", menuName = "Scriptable Objects/Crop")]
public class CropsScriptableObject : ScriptableObject
{
    public string cropName;
    public List<Sprite> cropSprites;
    public int growTime;
}
