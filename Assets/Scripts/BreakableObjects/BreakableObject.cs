using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : Unit
{
    public Item itemPrefab;    
    public string objName;
    public int HP;

    [NonReorderable] public List<BreakableObjectDrop> breakableObjectDrops = new List<BreakableObjectDrop>();
    //public BreakableObjectHitEvent onBreakableObjectHitEvent = new BreakableObjectHitEvent();

    protected override void OnEnable()
    {
        
    }

    protected override void OnDisable()
    {
        
    }

    public virtual void OnHit(GameObject tool, Transform interactorPos)
    {

    }

    public void SpawnDrops()
    {
        int index = Random.Range(0, breakableObjectDrops.Count);
        BreakableObjectDrop breakableObjectDrop = breakableObjectDrops[index];
        int amount = Random.Range(breakableObjectDrop.minAmount, breakableObjectDrop.maxAmount);
        for (int i = 0; i < amount; i++)
        {
            Item item = Instantiate(itemPrefab);
            //item.transform.position = (Vector2)transform.position;
            item.transform.position = new Vector2(GetRand(-transform.localPosition.x, transform.localPosition.x), transform.localPosition.y);
            item.so_Item = breakableObjectDrop.so_Item;
            item.GetComponent<SpriteRenderer>().sprite = breakableObjectDrop.so_Item.icon;
        }
    }

    public float GetRand(float min, float max)
    {
        float rand = Random.Range(min, max);
        return rand;
    }
}
