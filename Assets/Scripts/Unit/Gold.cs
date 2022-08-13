using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : StatManager
{
    public GoldModifyEvent onGoldModifyEvent = new GoldModifyEvent();

    private void Awake()
    { 
        Init();
    }
    public override void Init()
    {
        maxStat = 100;
        base.Init();
    }

    public void ModifyGold(float p_amount)
    {
        ModifyStat(p_amount, onGoldModifyEvent, null);
    }
    
    public float CheckGold()
    {
        return currentStat;
    }
}
