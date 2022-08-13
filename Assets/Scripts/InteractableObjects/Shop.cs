using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopButton;
    public DialogueScriptableObject shopDialogueScriptableObject;

    [Header("Items")]
    public GameObject parsnipSeedsPrefab;
    public ItemScriptableObject parsnipSeedsScriptableObject;

    public void BuyItemButtonClick()
    {
        if (!InventoryManager.instance.IsUnstackableItemAvail(parsnipSeedsPrefab.name))
        {
            //Debug.Log("Item not found, adding...");
            InventoryManager.instance.unstackableItems.Add(parsnipSeedsPrefab);
            InventoryManager.instance.inventoryUI.AddToUI(parsnipSeedsPrefab.GetComponent<Item>().UISprite);
        }
        if (PlayerManager.instance.playerGold.CheckGold() > 0)
        {
            ItemData foundStackableItem = InventoryManager.instance.GetStackableItem(parsnipSeedsScriptableObject.scriptableObjectName);
            //Debug.Log(foundStackableItem.so_Item.scriptableObjectName);
            foundStackableItem.itemUI.UpdateText(1);
            foundStackableItem.amount++;
            PlayerManager.instance.playerGold.ModifyGold(-parsnipSeedsScriptableObject.buyPrice);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shopButton.SetActive(true);
            CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(shopDialogueScriptableObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shopButton.SetActive(false);
            CharacterDialogueUI.onCharacterLeaveEvent.Invoke();
        }
    }
}
