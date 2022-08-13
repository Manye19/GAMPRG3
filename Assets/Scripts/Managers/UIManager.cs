using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }

    [Header("Assorted Objects")]
    public Animator transitionAnim;
    public GameObject inventoryUI;
    public GameObject bedUI;
    public GameObject shopUI;
    public GameObject dayEndShipUI;
    public TMP_Text goldAmountText;
    public SliderBarUI healthBarUI;
    public SliderBarUI staminaBarUI;
    public Transform itemsShippedParentPanel;
    public ItemUI itemUIPrefab;
    private bool isDepositedAvail;
    public TMP_Text totalText;
    private float sumOfGold;

    [Header("Text Objects")]
    public TMP_Text dayText;
    public TMP_Text dayCountText;
    public TMP_Text faintedText;

    private void Awake()
    {
        _instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        TimeManager.instance.onDayEndedEvent.AddListener(DayEnd);
    }    

    public void RoomEntered(Passageway p_passageway)
    {
        StartCoroutine(LoadRoom());
    }

    IEnumerator LoadRoom()
    {
        dayCountText.text = "";
        faintedText.text = "";
        PlayerManager.instance.playerMovement.isMoving = false;
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        PlayerManager.instance.playerMovement.isMoving = true;
        transitionAnim.SetTrigger("Start");
    }

    public void OpenBedUI(bool p_bool)
    {
        PlayerManager.instance.playerToolController.CanUseUpdate(!p_bool);
        bedUI.SetActive(p_bool);
    }

    public void OpenShopUI(bool p_bool)
    {
        PlayerManager.instance.playerToolController.CanUseUpdate(!p_bool);
        shopUI.SetActive(p_bool);
    }

    public void UpdateGoldUI(float p_current, float p_max)
    {
        goldAmountText.text = p_current.ToString();
    }

    public void DayEnd(bool p_isFainted, int p_dayCount)
    {
        StartCoroutine(DayEndCoroutine(p_isFainted, p_dayCount));
    }

    public IEnumerator DayEndCoroutine(bool p_isFainted, int p_dayCount)
    {
        //Pause Game
        TimeManager.instance.onPauseGameTimeEvent.Invoke(false);
        PlayerManager.instance.playerToolController.CanUseUpdate(false);
        dayCountText.text = $"DAY {p_dayCount}";
        
        if (!p_isFainted)
        {
            faintedText.text = "ENDED";
        }
        else
        {
            faintedText.text = "YOU FAINTED";
        }
        
        // Transition Anim START
        transitionAnim.SetTrigger("Start");

        yield return new WaitForSeconds(3.5f);

        dayText.text = $"DAY {p_dayCount + 1}";
        dayCountText.text = $"DAY {p_dayCount + 1}";
        faintedText.text = "STARTS";

        yield return new WaitForSeconds(3.5f);

        isDepositedAvail = ShippingBox.instance.IsItemDepositedAvail();
        
        if (isDepositedAvail)
        {
            for (int i = 0; i < ShippingBox.instance.itemsDeposited.Count; i++)
            {
                if (ShippingBox.instance.itemsDeposited[i].amount > 0)
                {
                    ItemData itemData = ShippingBox.instance.itemsDeposited[i];
                    ItemUI itemUI = Instantiate(itemUIPrefab);
                    itemUI.transform.SetParent(itemsShippedParentPanel, false);
                    itemUI.Init(itemData.amount.ToString(), itemData.so_Item);
                    itemData.SetItemUI(itemUI);
                    float subTotal = itemData.amount * itemData.so_Item.sellPrice;
                    sumOfGold += itemData.amount * itemData.so_Item.sellPrice;
                    itemUI.itemSubTotalText.text = subTotal.ToString();
                }
            }
            dayEndShipUI.SetActive(true);
            totalText.text = sumOfGold.ToString();
            Debug.Log(sumOfGold);
            PlayerManager.instance.playerGold.ModifyGold(sumOfGold);
            yield return new WaitForSeconds(5f);
            for (int i = 0; i < itemsShippedParentPanel.childCount; i++)
            {
                Destroy(itemsShippedParentPanel.transform.GetChild(i).gameObject);
            }
            dayEndShipUI.SetActive(false);
        }

        sumOfGold = 0;
        TimeManager.instance.onDayChangingEvent.Invoke();
        TimeManager.instance.NewDay();

        // Transition Anim END
        transitionAnim.SetTrigger("Start");

        //Play Game
        TimeManager.instance.onPauseGameTimeEvent.Invoke(true);
        PlayerManager.instance.playerToolController.CanUseUpdate(true);
    }
}
