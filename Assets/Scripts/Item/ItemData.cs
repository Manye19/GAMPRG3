using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData 
{
    public string name;
    public SO_Item so_Item;
    public int amount;

    public ItemUI itemUI;
    public void SetItemUI(ItemUI p_itemUI)
    {
        itemUI = p_itemUI;
    }
}
