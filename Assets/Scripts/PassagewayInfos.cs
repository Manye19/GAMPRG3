using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PassagewayInfos
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
