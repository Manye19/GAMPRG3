using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : BreakableObject
{
    Health health;
    private Animator animator;
    [SerializeField] GameObject treeBody;
    [SerializeField] GameObject treeStump;

    // Start is called before the first frame update
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        objName = "Tree";
        HP = 8;
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
            else if(HP == 3)
            {
                animator.SetTrigger("Fall");
                health.onDeathEvent.Invoke();
            }
        }
    }

    public void RemoveBody()
    {
        treeBody.gameObject.SetActive(false);
    }
}
