using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DigitalClockUI : MonoBehaviour
{
    private TMP_Text displayText;

    private void Awake()
    {
        displayText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        TimeManager.onTimeChangedEvent.AddListener(UpdateTime);
    }

    private void OnDisable()
    {
        TimeManager.onTimeChangedEvent.RemoveListener(UpdateTime);
    }

    private void UpdateTime(int hour, int minuteByTens)
    {
        displayText.text = $"{hour:00}:{minuteByTens:00}";
    }
}
