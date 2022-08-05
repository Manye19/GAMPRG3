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
    public GameObject InventoryUI;
    public GameObject BedUI;
    public SliderBarUI staminaBarUI;

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
        BedUI.SetActive(p_bool);
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

        TimeManager.instance.onDayChangingEvent.Invoke();

        dayText.text = $"DAY {p_dayCount + 1}";
        dayCountText.text = $"DAY {p_dayCount + 1}";
        faintedText.text = "STARTS";

        yield return new WaitForSeconds(3.5f);

        TimeManager.instance.NewDay();

        // Transition Anim END
        transitionAnim.SetTrigger("Start");

        //Play Game
        TimeManager.instance.onPauseGameTimeEvent.Invoke(true);
        PlayerManager.instance.playerToolController.CanUseUpdate(true);
    }
}
