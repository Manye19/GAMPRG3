using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippingBox : MonoBehaviour
{
    private static ShippingBox _instance;
    public static ShippingBox instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ShippingBox>();
            }

            return _instance;

        }
    }

    [NonReorderable] public List<ItemData> itemsDeposited;
    public GameObject shippingPanel;

    private void Awake()
    {
        _instance = this;
    }

    private void OnEnable()
    {
        TimeManager.instance.onDayChangingEvent.AddListener(ResetShippingBin);
    }

    public void AddItem(ItemScriptableObject p_itemScriptableObject)
    {   
        ItemData foundStackableItem = InventoryManager.instance.GetStackableItem(p_itemScriptableObject.scriptableObjectName);
        if (foundStackableItem.amount > 0)
        {            
            if (IsItemDepositedFound(p_itemScriptableObject.scriptableObjectName))
            {
                Debug.Log("Item deposited found! Adding...");
                itemsDeposited[GetItemDepositedPos(p_itemScriptableObject.scriptableObjectName)].amount++;
                foundStackableItem.itemUI.UpdateText(-1);
                foundStackableItem.amount--;
            }            
        }
        else
        {
            Debug.Log("You have nothing to deposit!");
        }
    }

    public void RemoveItem(ItemScriptableObject p_itemScriptableObject)
    {
        int num = GetItemDepositedPos(p_itemScriptableObject.scriptableObjectName);
        if (itemsDeposited[num].amount > 0)
        {
            itemsDeposited[num].amount--;            
            ItemData foundStackableItem = InventoryManager.instance.GetStackableItem(p_itemScriptableObject.scriptableObjectName);
            foundStackableItem.itemUI.UpdateText(1);
            foundStackableItem.amount++;
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            shippingPanel.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shippingPanel.SetActive(false);
        }
    }

    #region Functions
    private void ResetShippingBin()
    {
        for(int i = 0; i < itemsDeposited.Count; i++)
        {
            itemsDeposited[i].amount = 0;
        }
    }

    public bool IsItemDepositedFound(string p_name)
    {
        bool p_bool = false;
        for (int i = 0; i < itemsDeposited.Count; i++)
        {
            if (itemsDeposited[i].name.Equals(p_name))
            {
                p_bool = true;
                return p_bool;
            }
        }
        return p_bool;
    }

    public bool IsItemDepositedAvail()
    {
        bool p_bool = false;
        for (int i = 0; i < itemsDeposited.Count; i++)
        {
            if (itemsDeposited[i].amount > 0)
            {
                p_bool = true;
                return p_bool;
            }
        }
        return p_bool;
    }

    public int GetItemDepositedPos(string p_name)
    {
        for (int i = 0; i < itemsDeposited.Count; i++)
        {
            if (itemsDeposited[i].name.Equals(p_name))
            {
                return i;
            }
        }
        return 0;
    }
    #endregion
}
