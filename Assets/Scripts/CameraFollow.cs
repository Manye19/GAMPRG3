using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector2 offSet;
    public Vector2 panLimit;

    private void Awake()
    {

    }

    private void OnEnable()
    {

        CameraManager.onCameraMovedEvent.AddListener(CameraMoved);
        Debug.Log(offSet);
        Debug.Log(panLimit);

    }

    public void CameraMoved(Vector2 p_newPos, Vector2 p_panLim)
    {
        offSet = new Vector3(p_newPos.x, p_newPos.y);
        panLimit = p_panLim;
    }

    private void Update()
    {
        Vector3 desiredPos = PlayerManager.instance.playerTransform.position;
        desiredPos.x = Mathf.Clamp(desiredPos.x, offSet.x + -panLimit.x, offSet.x + panLimit.x);
        desiredPos.y = Mathf.Clamp(desiredPos.y, offSet.y + -panLimit.y, offSet.y + panLimit.y);
        desiredPos.z = transform.position.z;        
        transform.position = desiredPos;
    }
}
