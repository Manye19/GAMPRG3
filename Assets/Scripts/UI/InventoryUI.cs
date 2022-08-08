using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform inventorySlotsPanel;
    public ItemUI itemUIPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (itemUIPrefab != null) 
            GenerateInventoryUIs();
    }

    public void GenerateInventoryUIs()
    {
        for (int i = 0; i < InventoryManager.instance.stackableItems.Count; i++)
        {
            ItemData currentItemData = InventoryManager.instance.stackableItems[i];
            ItemUI newItemUI = Instantiate(itemUIPrefab);
            newItemUI.transform.SetParent(inventorySlotsPanel, false);
            newItemUI.Init("0", currentItemData.so_Item.scriptableObjectIcon);
            currentItemData.SetItemUI(newItemUI);
        }
    }
    
    public void AddToUI(Sprite UISprite)
    {
        foreach(Transform slot in this.gameObject.transform)
        {
            Image imageComponent = slot.GetChild(0).GetComponent<Image>();
            

            if(!imageComponent.enabled)
            {
                imageComponent.enabled = true;
                imageComponent.sprite = UISprite;

                break;
            }
        }
    }
}
