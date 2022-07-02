using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    public string roomName;
    public List<PassagewayInfos> passagewayInfos = new List<PassagewayInfos>();
    
    public RoomEnteredEvent onRoomEnteredEvent = new RoomEnteredEvent();

    private void OnEnable()
    {
        onRoomEnteredEvent.AddListener(UIManager.instance.RoomEntered);        
        foreach(PassagewayInfos passagewayInfo in passagewayInfos)
        {
            Room room = this;
            Transform playerDestinationPosition;
            Passageway connectedToPassageway;
            passagewayInfo.GetPassagewayInfos(out playerDestinationPosition, out connectedToPassageway);
            passagewayInfo.passageway.AssignPassageway(room, playerDestinationPosition, connectedToPassageway);
        }
    }
    private void OnDisable()
    {
        
    }
}
