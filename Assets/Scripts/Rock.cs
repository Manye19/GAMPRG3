using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : BreakableObject
{
    // Start is called before the first frame update
    void Start()
    {
        objName = "Rock";
        HP = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnHit(GameObject tool, Transform interactorPos)
    {
        if(tool.GetComponent<Pickaxe>())
        {
            this.HP--;

            if (HP <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
