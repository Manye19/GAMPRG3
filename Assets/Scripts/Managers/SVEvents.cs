using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#region Room Events
public class RoomEnteredEvent : UnityEvent<Passageway> { }
public class CameraMovedEvent : UnityEvent<Vector2, Vector2> { }
public class UpdateCurrentRoomIDEvent : UnityEvent<int> { }
#endregion

#region Stamina Events
//Implement Stamina Events
public class StaminaDecreaseEvent : UnityEvent<float, float> { }
public class StaminaDepletedEvent : UnityEvent { }
#endregion

#region ToolUse Events
//Implement ToolUse Events
public class ToolUsedEvent : UnityEvent<float> { };
#endregion

#region Health Events
//Implement Health Events
public class HealthModifyEvent : UnityEvent<float, float> { };
public class DeathEvent : UnityEvent { };
#endregion

#region Gold Events
public class GoldModifyEvent : UnityEvent<float, float> { }
#endregion

#region Inventory Events
//Implement Inventory Events
public class AddItemEvent : UnityEvent<GameObject, ItemScriptableObject, string, int> { };
public class RemoveItemEvent : UnityEvent<GameObject, ItemScriptableObject, string, int> { };
#endregion

#region TimeManager Events
//Implement TimeManager Events
public class TimeChangedEvent : UnityEvent <int, int> { };
public class DayEndedEvent : UnityEvent<bool, int> { };
public class DayChangingEvent : UnityEvent { };
public class HourChangedEvent : UnityEvent<int> { };
public class PauseGameTimeEvent : UnityEvent<bool> { };
#endregion

#region InteractableObject Events
//Implement InteractableObject Events
public class InteractEvent : UnityEvent { }
public class BedInteractedEvent : UnityEvent { }
#endregion

#region CharacterDialogue Events
public class CharacterSpokenToEvent : UnityEvent<DialogueScriptableObject> { }
public class CharacterLeaveEvent : UnityEvent { }
#endregion