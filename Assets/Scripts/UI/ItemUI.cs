using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    public ItemData itemData;

    public TMP_Text itemAmountText;
    public Image itemIconImage;

    public void Init(string p_itemAmount, Sprite p_itemIcon)
    {
        itemAmountText.text = p_itemAmount;
        itemIconImage.sprite = p_itemIcon;
    }

    public void UpdateText(int p_amount)
    {
        itemAmountText.text = (itemData.amount + p_amount).ToString();        
        itemData.amount += p_amount;
    }
}
