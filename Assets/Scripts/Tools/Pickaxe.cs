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

    /*public override void OnUse()
    {
        StartCoroutine(ShowAndHideSprite());
    }

    IEnumerator ShowAndHideSprite()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }*/
}
