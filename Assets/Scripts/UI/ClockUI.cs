using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{
    [SerializeField] private RectTransform hand;
    const float hoursToDegrees = 220 / 24;

    private void OnEnable()
    {
        TimeManager.onHourChangedEvent.AddListener(UpdateTime);
    }

    private void OnDisable()
    {
        TimeManager.onHourChangedEvent.RemoveListener(UpdateTime);
    }

    private void UpdateTime(int hour)
    {
        hand.localRotation = Quaternion.Euler(0, 0, 90 + hoursToDegrees * ((hour + TimeManager.hoursInDay - TimeManager.sunriseHour) % TimeManager.hoursInDay));
    }
}
