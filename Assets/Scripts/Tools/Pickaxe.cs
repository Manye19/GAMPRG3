using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : Tools
{
    // Start is called before the first frame update
    void Start()
    {
        itemName = "Pickaxe";
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
