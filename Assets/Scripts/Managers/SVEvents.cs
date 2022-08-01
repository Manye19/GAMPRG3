using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomEnteredEvent : UnityEvent<Passageway> { }
public class CameraMovedEvent : UnityEvent<Vector2, Vector2> { }

//Implement Stamina Events
public class StaminaDecreaseEvent : UnityEvent<float, float> { }
public class StaminaDepletedEvent : UnityEvent { }

//Implement ToolUse Events
public class ToolUsedEvent : UnityEvent<float> { };

//Implement Health Events
public class HealthModifyEvent : UnityEvent<float> { };
public class DeathEvent : UnityEvent { };

//Implement Inventory Events
public class AddItemEvent : UnityEvent<GameObject, ItemScriptableObject, string, int> { };