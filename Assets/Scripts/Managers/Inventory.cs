using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory _instance;
    public static Inventory instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Inventory>();
            }

            return _instance;
        }
    }

    public AddItemEvent onAddItemEvent = new AddItemEvent();

    public GameObject[] startingItems;
    public List<GameObject> unstackableItems = new List<GameObject>();
    [NonReorderable] public List<ItemData> stackableItems = new List<ItemData>();

    public int inventorySlots = 10;
    public InventoryUI inventoryUI;

    private void Awake()
    {
        _instance = this;

        for (int i = 0; i < startingItems.Length; i++)
        {
            AddItem(startingItems[i], null, null, 0);
        }
    }

    private void OnEnable()
    {
        onAddItemEvent.AddListener(AddItem);
    }

    private void OnDisable()
    {
        onAddItemEvent.RemoveListener(AddItem);
    }

    void AddItem(GameObject GO_Item, ItemScriptableObject so_Item, string p_name, int p_amount)
    {
        if (GO_Item.TryGetComponent(out Item item))
        {
            if (unstackableItems.Count < inventorySlots && !item.isStackable)
            {
                unstackableItems.Add(GO_Item);
                inventoryUI.AddToUI(GO_Item.GetComponent<Item>().UISprite);
            }
            else
            {
                ItemData foundStackableItem = GetStackableItem(so_Item.name);
                // get found item; increment it thru back and front end
                foundStackableItem.itemUI.UpdateText(p_amount);
                foundStackableItem.amount += p_amount;
            }
        }
    }

    public static ItemData GetStackableItem(string p_item)
    {
        for (int i = 0; i < Inventory.instance.stackableItems.Count; i++)
        {
            if (Inventory.instance.stackableItems[i].name == p_item)
            {
                return Inventory.instance.stackableItems[i];
            }
        }
        return null;
    }
}
