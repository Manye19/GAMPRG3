using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public SliderBarUI staminaBarUI;
    private float currentStamina;
    [SerializeField] private float maxStamina;

    public StaminaDecreaseEvent OnStaminaModifiedEvent = new StaminaDecreaseEvent();
    public static StaminaDepletedEvent OnStaminaDepletedEvent = new StaminaDepletedEvent();

    private void Awake()
    {
        //Set Stamina from Max Stamina
        currentStamina = maxStamina;
    }
    private void OnEnable()
    {
        //Update Stamina Bar        
        //Listen to Stamina decrements
        OnStaminaModifiedEvent.AddListener(staminaBarUI.UpdateBar);
        
        //Listen to Stamina <= 0
        //OnStaminaDepletedEvent.AddListener();
    }
    private void OnDisable()
    {
        //Update Stamina Bar

        OnStaminaModifiedEvent.RemoveListener(staminaBarUI.UpdateBar);

        //Unlisten to Stamina <= 0
        //OnStaminaDepletedEvent.RemoveListener();
    }

    public void ModifyStamina(float p_amount)
    {
        if (currentStamina > 0)
        {
            currentStamina -= p_amount;
            OnStaminaModifiedEvent.Invoke(currentStamina, maxStamina);
        }

        if (currentStamina <= 0)
        {
            OnStaminaDepletedEvent.Invoke();
        }
    }
}
