using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager _instance;
    public static TimeManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TimeManager>();
            }

            return _instance;
        }
    }

    public static TimeChangedEvent onTimeChangedEvent = new TimeChangedEvent();
    public DayEndedEvent onDayEndedEvent = new DayEndedEvent();
    public DayChangingEvent onDayChangingEvent = new DayChangingEvent();
    public static HourChangedEvent onHourChangedEvent = new HourChangedEvent();
    public PauseGameTimeEvent onPauseGameTimeEvent = new PauseGameTimeEvent();
    
    public int dayCount;
    [SerializeField] private int startHour;
    

    public static int minute;
    public static int minuteByTens;
    private int hour;

    [SerializeField] private float minToRealSeconds;

    public bool DoTimer = true;
    public IEnumerator coroutineTime;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        minute = 0;
        hour = startHour;
        coroutineTime = DoTimerCoroutine();
        onHourChangedEvent.Invoke(hour);
        StartCoroutine(coroutineTime);
    }

    private void OnEnable()
    {
        PlayerManager.instance.playerHealth.onDeathEvent.AddListener(FaintedEndDay);
        Stamina.OnStaminaDepletedEvent.AddListener(FaintedEndDay);
        Bed.onBedInteractedEvent.AddListener(EndDay);
        onTimeChangedEvent.AddListener(OnTimeCheck);
        onPauseGameTimeEvent.AddListener(SetPauseGame);
    }

    private void OnDisable()
    {
        PlayerManager.instance.playerHealth.onDeathEvent.RemoveListener(FaintedEndDay);
        Stamina.OnStaminaDepletedEvent.RemoveListener(FaintedEndDay);
        Bed.onBedInteractedEvent.RemoveListener(EndDay);
        onTimeChangedEvent.RemoveListener(OnTimeCheck);
        onPauseGameTimeEvent.RemoveListener(SetPauseGame);
    }

    private IEnumerator DoTimerCoroutine()
    {
        while(DoTimer)
        {
            yield return new WaitForSeconds(minToRealSeconds);
            minute++;            
            
            if (minute % 10 == 0)
            {
                minuteByTens = minute;
            }

            if (minute >= SVConstants.MINUTES_IN_HOUR)
            {
                hour++;

                if (hour > SVConstants.HOURS_IN_DAY)
                {
                    hour = 1;
                }

                onHourChangedEvent.Invoke(hour);
                minute = 0;
                minuteByTens = 0;
            }

            onTimeChangedEvent.Invoke(hour, minuteByTens);
        }
    }

    private void OnTimeCheck(int p_hour, int p_minuteByTens)
    {
        if (p_hour == SVConstants.END_HOUR)
        {
            hour = startHour;
            EndDay();
        }
    }

    public void FaintedEndDay()
    {
        onDayEndedEvent.Invoke(true, dayCount);
    }

    public void EndDay()
    {
        onDayEndedEvent.Invoke(false, dayCount);
    }

    public void NewDay()
    {
        hour = startHour;
        minute = 0;
        minuteByTens = 0;
        onHourChangedEvent.Invoke(hour);
        onTimeChangedEvent.Invoke(hour, minuteByTens);
        dayCount++;
    }

    private void SetPauseGame(bool p_bool)
    {
        //Debug.Log("Time is: " + p_bool);
        DoTimer = p_bool;
        if (coroutineTime != null)
        {
            StopCoroutine(coroutineTime);
            coroutineTime = null;
        }

        if (p_bool)
        {
            coroutineTime = DoTimerCoroutine();
            StartCoroutine(coroutineTime);
        }
    }
}
