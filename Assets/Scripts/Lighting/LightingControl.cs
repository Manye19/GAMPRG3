using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingControl : MonoBehaviour
{
    [SerializeField] private LightingSchedScriptableObject lightingSchedule;
    private Light2D light2D;
    private IEnumerator lightCoroutine;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
        TimeManager.onHourChangedEvent.AddListener(OnTimeCheck);
    }

    private void OnDestroy()
    {
        TimeManager.onHourChangedEvent.RemoveListener(OnTimeCheck);
    }

    private void OnTimeCheck(int p_hour)
    {
        SetLightingIntensity(p_hour);
    }

    private void SetLightingIntensity(int p_hour)
    {
        for (int i = 0; i < lightingSchedule.lightingDataArray.Length; i++)
        {
            if (lightingSchedule.lightingDataArray[i].hour == p_hour)
            {
                float targetLightingIntensity = lightingSchedule.lightingDataArray[i].lightIntensity;
                Color targetColor = lightingSchedule.lightingDataArray[i].color;
                if (lightCoroutine != null)
                {
                    StopCoroutine(lightCoroutine);
                    lightCoroutine = null;
                }
                lightCoroutine = FadeLightRoutine(targetLightingIntensity, targetColor);
                StartCoroutine(lightCoroutine);
                break;
            }
        }
    }
    private IEnumerator FadeLightRoutine(float targetLightingIntensity, Color targetColor)
    {
        float fadeDuration = 5f;
        float fadeSpeed = Mathf.Abs(light2D.intensity - targetLightingIntensity) / fadeDuration;
        while (light2D.intensity != targetLightingIntensity || light2D.color != targetColor)
        {
            //Debug.Log(light2D.color + " != " + targetColor);
            light2D.intensity = Mathf.MoveTowards(light2D.intensity, targetLightingIntensity, fadeSpeed * Time.deltaTime);
            light2D.color = Color.Lerp(light2D.color, targetColor, 0.01f);
            if (light2D.intensity == targetLightingIntensity && light2D.color == targetColor)
            {
                light2D.intensity = targetLightingIntensity;
                light2D.color = targetColor;
                break;
            }
            yield return null;
        }
        light2D.intensity = targetLightingIntensity;
        light2D.color = targetColor;
    }
}
