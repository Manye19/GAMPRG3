using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool isAlive;
    private float currentHealth;
    private float maxHealth;

    public HealthModifyEvent onHealthModifyEvent = new HealthModifyEvent();
    public DeathEvent onDeathEvent = new DeathEvent();

    public void SetValues(float p_maxHealth)
    {
        maxHealth = p_maxHealth;
    }

    public void Init()
    {
        isAlive = true;
        currentHealth = maxHealth;
    }

    public void ModifyHealth(float p_float)
    {
        currentHealth += p_float;
        // check if dead; HP <= 0
        if (currentHealth <= 0)
        {
            isAlive = false;
            onDeathEvent.Invoke();
        }
    }
}
