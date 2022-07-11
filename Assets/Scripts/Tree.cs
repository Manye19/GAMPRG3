using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : BreakableObject
{
    // Start is called before the first frame update
    void Start()
    {
        objName = "Tree";
        HP = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnHit(GameObject tool, Transform interactorPos)
    {
        if (tool.GetComponent<Axe>())
        {
            this.HP--;

            if (HP <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
