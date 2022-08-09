using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParsnipSeed : Tools
{
    public GameObject parsnipPlant;

    // Start is called before the first frame update
    void Start()
    {
        itemName = "Parsnip Seed";
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
