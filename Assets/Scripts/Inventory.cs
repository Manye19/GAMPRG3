using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] startingItems;
    public List<GameObject> inventoryItems = new List<GameObject>();
    public int inventorySlots = 10;
    public InventoryUI inventoryUI;

    private void Awake()
    {
        for (int i = 0; i < startingItems.Length; i++)
        {
            AddItem(startingItems[i]);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddItem(GameObject Item)
    {
        if (inventoryItems.Count < inventorySlots)
        {
            if(Item.GetComponent<Item>())
            {
                inventoryItems.Add(Item);
                inventoryUI.AddToUI(Item.GetComponent<Item>().UISprite);
            }
        }
    }

}
