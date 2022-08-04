using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stamina : StatManager
{
    [SerializeField] private float staminaPenalty;
    public float currentMaxStamina { get; private set; }
    private bool isPenalized = false;

    public StaminaDecreaseEvent OnStaminaModifiedEvent = new StaminaDecreaseEvent();
    public static StaminaDepletedEvent OnStaminaDepletedEvent = new StaminaDepletedEvent();

    private void Awake()
    {
        //Set Stamina from Max Stamina
        Init();
    }

    private void OnEnable()
    {
        //Update Stamina Bar        
        //Listen to Stamina decrements
        OnStaminaModifiedEvent.AddListener(UIManager.instance.staminaBarUI.UpdateBar);
        
        TimeManager.instance.onDayChangingEvent.AddListener(StaminaRegen);
        
        //Listen to Stamina <= 0
        OnStaminaDepletedEvent.AddListener(PenalizeStamina);
    }

    private void OnDisable()
    {
        //Update Stamina Bar
        OnStaminaModifiedEvent.RemoveListener(UIManager.instance.staminaBarUI.UpdateBar);

        TimeManager.instance.onDayChangingEvent.AddListener(StaminaRegen);
        
        //Unlisten to Stamina <= 0
        OnStaminaDepletedEvent.RemoveListener(PenalizeStamina);
    }

    public override void Init()
    {
        maxStat = 100;
        base.Init();
        currentMaxStamina = maxStat;
    }

    public void ModifyStamina(float p_amount)
    {
        ModifyStat(p_amount, OnStaminaModifiedEvent, OnStaminaDepletedEvent);
        Debug.Log("Stamina: " + currentStat);        
    }

    public void PenalizeStamina()
    {
        currentMaxStamina -= currentMaxStamina * staminaPenalty;
        isPenalized = true;
        UIManager.instance.staminaBarUI.UpdateBar(currentStat, maxStat);
    }

    public void StaminaRegen()
    {
        if(isPenalized)
        {
            isPenalized = false;
        }
        else
        {
            if (currentStat < maxStat)
            {
                currentMaxStamina += currentMaxStamina * staminaPenalty;
            }
            
            if(currentMaxStamina > maxStat)
            {
                currentMaxStamina = maxStat;
            }
        }

        currentStat = currentMaxStamina;
        UIManager.instance.staminaBarUI.UpdateBar(currentStat, maxStat);
    }
}
