using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LightingSched", menuName = "Scriptable Objects/Lighting Schedule")]
public class LightingSchedScriptableObject : ScriptableObject
{
    [NonReorderable] public LightingData[] lightingDataArray;
}
