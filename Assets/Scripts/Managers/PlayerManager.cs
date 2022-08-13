using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    public static PlayerManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerManager>();
            }

            return _instance;

        }
    }
    
    public static UpdateCurrentRoomIDEvent onUpdateCurrentRoomIDEvent = new UpdateCurrentRoomIDEvent();

    public int currentRoomID;

    public GameObject player;
    public PlayerMovement playerMovement;
    public Transform playerTransform { get; private set; }
    public ToolController playerToolController;
    public Health playerHealth;
    public Stamina playerStamina;
    public Gold playerGold;

    private void Awake()
    {
        _instance = this;
        // DontDestroyOnLoad(gameObject);
        playerTransform = player.transform;
    }

    private void OnEnable()
    {
        onUpdateCurrentRoomIDEvent.AddListener(UpdateCurrentRoomID);
        playerHealth.onHealthModifyEvent.AddListener(UIManager.instance.healthBarUI.UpdateBar);
        playerGold.onGoldModifyEvent.AddListener(UIManager.instance.UpdateGoldUI);
        TimeManager.instance.onDayChangingEvent.AddListener(playerHealth.HealthRegen);
    }

    private void OnDisable()
    {
        onUpdateCurrentRoomIDEvent.RemoveListener(UpdateCurrentRoomID);
        playerHealth.onHealthModifyEvent.RemoveListener(UIManager.instance.healthBarUI.UpdateBar);
        playerGold.onGoldModifyEvent.AddListener(UIManager.instance.UpdateGoldUI);
        TimeManager.instance.onDayChangingEvent.RemoveListener(playerHealth.HealthRegen);
    }

    private void UpdateCurrentRoomID(int p_index)
    {
        currentRoomID = p_index;
    }
}
