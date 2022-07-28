using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Tools
{
    // Start is called before the first frame update
    void Start()
    {
        itemName = "Axe";
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
