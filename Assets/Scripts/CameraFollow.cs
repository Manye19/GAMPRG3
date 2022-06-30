using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{    
    public float smoothSpeed = 0.125f;
    public Vector3 offSet;
    void LateUpdate()
    {
        Vector3 desiredPos = PlayerManager.instance.player.transform.position + offSet;
        transform.position = desiredPos;
    }
}
