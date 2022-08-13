using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _instance;
    public static InventoryManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<InventoryManager>();
            }

            return _instance;
        }
    }

    public AddItemEvent onAddItemEvent = new AddItemEvent();

    public GameObject[] startingItems;
    public List<GameObject> unstackableItems = new List<GameObject>();
    [NonReorderable] public List<ItemData> stackableItems = new List<ItemData>();
    public GameObject parsnipSeedsPrefab;
    public ItemScriptableObject parsnipSeedsScriptableObject;

    public int inventorySlots = 10;
    public InventoryUI inventoryUI;

    private void Awake()
    {
        _instance = this;

        for (int i = 0; i < startingItems.Length; i++)
        {
            Item item = startingItems[i].GetComponent<Item>();
            if (item.isStackable)
            {
                AddItem(startingItems[i], item.itemScriptableObject, item.itemName, 10);
            }
            else
            {
                AddItem(startingItems[i], null, null, 0);
            }
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

    public void AddItem(GameObject GO_Item, ItemScriptableObject so_Item, string p_name, int p_amount)
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
                ItemData foundStackableItem = GetStackableItem(so_Item.scriptableObjectName);
                // get found item; increment it thru back and front end
                foundStackableItem.itemUI.UpdateText(p_amount);
                foundStackableItem.amount += p_amount;
            }
        }
    }   

    public void RemoveItem(GameObject GO_Item, ItemScriptableObject so_Item)
    {
        ItemData foundStackableItem = GetStackableItem(so_Item.scriptableObjectName);
        if (foundStackableItem.amount > 0)
        {            
            foundStackableItem.itemUI.UpdateText(-1);
            foundStackableItem.amount--;
        }
        if (foundStackableItem.amount <= 0)
        {
            unstackableItems.Remove(GO_Item);
            inventoryUI.RemoveToUI(GO_Item.GetComponent<Item>().UISprite);
        }
    }

    #region Functions
    public GameObject GetUnstackableItem(string p_name)
    {
        for (int i = 0; i < InventoryManager.instance.unstackableItems.Count; i++)
        {
            if (InventoryManager.instance.unstackableItems[i].gameObject.name == p_name)
            {
                return InventoryManager.instance.unstackableItems[i];
            }            
        }
        return null;
    }

    public bool IsUnstackableItemAvail(string p_name)
    {
        bool p_bool = false;
        for (int i = 0; i < InventoryManager.instance.unstackableItems.Count; i++)
        {
            if (InventoryManager.instance.unstackableItems[i].gameObject.name == p_name)
            {
                p_bool = true;
                return p_bool;
            }
        }
        return p_bool;
    }

    public ItemData GetStackableItem(string p_name)
    {
        for (int i = 0; i < InventoryManager.instance.stackableItems.Count; i++)
        {
            if (InventoryManager.instance.stackableItems[i].name == p_name)
            {
                return InventoryManager.instance.stackableItems[i];
            }
        }
        return null;
    }
    #endregion
}
