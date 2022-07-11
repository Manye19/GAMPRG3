using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomEnteredEvent : UnityEvent<Passageway> { }
public class CameraMovedEvent : UnityEvent<Vector2, Vector2> { }
