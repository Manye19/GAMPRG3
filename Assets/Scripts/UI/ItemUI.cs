using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    public ItemData itemData;

    public Image itemIconImage;
    public TMP_Text itemAmountText;
    public TMP_Text itemSellPriceText;
    public TMP_Text itemSubTotalText;

    public void Init(string p_itemAmount, ItemScriptableObject itemScriptableObject)
    {
        itemData.name = itemScriptableObject.scriptableObjectName;
        itemSellPriceText.text = itemScriptableObject.sellPrice.ToString();
        itemAmountText.text = p_itemAmount;
        itemIconImage.sprite = itemScriptableObject.scriptableObjectIcon;
    }

    public void UpdateText(int p_amount)
    {
        itemAmountText.text = (itemData.amount + p_amount).ToString();        
        itemData.amount += p_amount;
    }
}
