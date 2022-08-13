using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatManager : MonoBehaviour
{
    protected float currentStat;
    protected float maxStat;
    public virtual void Init()
    {
        currentStat = maxStat;
    }

    public virtual void ModifyStat(float p_amount, UnityEvent<float, float> p_onChangedEvent, UnityEvent p_OnDepleted)
    {        
        if (currentStat >= 0)
        {
            currentStat += p_amount;
            p_onChangedEvent.Invoke(currentStat, maxStat);
        }
        if (currentStat <= 0)
        {
            p_OnDepleted?.Invoke();
        }
    }
}
