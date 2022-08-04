using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : StatManager
{
    public bool isAlive;

    public HealthModifyEvent onHealthModifyEvent = new HealthModifyEvent();
    public DeathEvent onDeathEvent = new DeathEvent();

    public override void Init()
    {
        isAlive = true;
        maxStat = 100;
        base.Init();
    }

    public void ModifyHealth(float p_amount)
    {
        ModifyStat(p_amount, onHealthModifyEvent, onDeathEvent);
    }
}
