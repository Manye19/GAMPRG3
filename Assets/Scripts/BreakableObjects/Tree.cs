using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : BreakableObject
{
    Health health;
    // Start is called before the first frame update
    void Start()
    {
        objName = "Tree";
        HP = 3;
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
        if (tool.GetComponent<Axe>())
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
