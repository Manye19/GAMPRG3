using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    public int currentRoomID;
    public string roomName;
    public Transform camPanLimTransform;
    [HideInInspector] public Vector2 camPanLimit;
    public List<PassagewayInfos> passagewayInfos = new List<PassagewayInfos>();    
    public RoomEnteredEvent onRoomEnteredEvent = new RoomEnteredEvent();

    private void OnEnable()
    {
        camPanLimit = Vector2Abs(transform.position - camPanLimTransform.position);
        onRoomEnteredEvent.AddListener(UIManager.instance.RoomEntered);        
        foreach(PassagewayInfos passagewayInfo in passagewayInfos)
        {
            Room room = this;
            Transform playerDestinationPosition;
            Passageway connectedToPassageway;
            passagewayInfo.GetPassagewayInfos(out playerDestinationPosition, out connectedToPassageway);
            passagewayInfo.passageway.AssignPassageway(room, playerDestinationPosition, transform.position, camPanLimit, connectedToPassageway);
        }
    }
    private void OnDisable()
    {
        
    }

    Vector2 Vector2Abs(Vector2 p_vector2)
    {
        Vector2 ans = new Vector2(Mathf.Abs(p_vector2.x), Mathf.Abs(p_vector2.y));
        return ans;
    }
}
