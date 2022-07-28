using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : Item
{
    public virtual void OnUse()
    {
        StartCoroutine(ShowAndHideSprite());
    }

    IEnumerator ShowAndHideSprite()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
