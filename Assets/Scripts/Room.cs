using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PassagewayInfo
{
    [SerializeField] private string name;
    [SerializeField] public Passageway passageway;
    [SerializeField] private Transform playerDestinationPosition;
    [SerializeField] private Passageway connectedToPassageway;

    public void GetPassagewayInfos(out Transform p_playerDestinationPosition, out Passageway p_connectedToPassageway)
    {
        p_playerDestinationPosition = playerDestinationPosition;
        p_connectedToPassageway = connectedToPassageway;
    }
}

public class RoomEnteredEvent : UnityEvent<Passageway> { }
public class Room : MonoBehaviour
{
    public string roomName;
    public List<PassagewayInfo> passagewayInfos = new List<PassagewayInfo>();
    
    public RoomEnteredEvent onRoomEnteredEvent = new RoomEnteredEvent();

    private void OnEnable()
    {
        onRoomEnteredEvent.AddListener(UIManager.instance.RoomEntered);
        foreach(PassagewayInfo passagewayInfo in passagewayInfos)
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
