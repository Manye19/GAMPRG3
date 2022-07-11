using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passageway : MonoBehaviour
{
    private Room room;
    private Transform playerDestinationPosition;
    public Vector2 camDestinationPosition { get; private set; }
    public Vector2 camPanLim { get; private set; }
    private Passageway connectedToPassageway;
    public void AssignPassageway(Room p_room, Transform p_playerDestinationPosition, Vector2 p_camDestinationPosition, Vector2 p_camPanLim, Passageway p_connectedToPassageway)
    {
        room = p_room;
        playerDestinationPosition = p_playerDestinationPosition;
        camDestinationPosition = p_camDestinationPosition;
        camPanLim = p_camPanLim;
        connectedToPassageway = p_connectedToPassageway;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            connectedToPassageway.room.onRoomEnteredEvent.Invoke(connectedToPassageway);
            StartCoroutine(C_WarpDelay(collision));
        }
    }

    IEnumerator C_WarpDelay(Collider2D collision)
    {
        yield return new WaitForSeconds(1f);
        CameraManager.onCameraMovedEvent.Invoke(connectedToPassageway.camDestinationPosition, connectedToPassageway.camPanLim);
        collision.gameObject.transform.position = connectedToPassageway.playerDestinationPosition.position;
    }
}
