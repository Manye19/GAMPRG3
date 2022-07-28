using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderBarUI : MonoBehaviour
{
    public Slider slider;
    private float current;
    private float currentMax;

    public void UpdateBar(float p_current, float p_currentMax)
    {
        current = p_current;
        currentMax = p_currentMax;
        slider.value = current / currentMax;        
    }

    public void SetMaxBar(float p_value)
    {
        slider.maxValue = p_value;
        slider.value = p_value;
    }

    public void SetBar(float p_value)
    {
        slider.value = p_value;
    }
}
