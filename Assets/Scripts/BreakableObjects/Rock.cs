using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : BreakableObject
{
    Health health;
    // Start is called before the first frame update
    void Start()
    {
        objName = "Rock";
        HP = 1;
    }

    protected override void OnEnable()
    {
        health = GetComponent<Health>();
        health.onDeathEvent.AddListener(SpawnDrops);
    }

    protected override void OnDisable()
    {
        health = GetComponent<Health>();
        health.onDeathEvent.RemoveListener(SpawnDrops);
    }

    public override void OnHit(GameObject tool, Transform interactorPos)
    {
        if(tool.GetComponent<Pickaxe>())
        {
            this.HP--;

            if (HP <= 0)
            {
                health.onDeathEvent.Invoke();
                Destroy(this.gameObject);
            }
        }
    }
}
