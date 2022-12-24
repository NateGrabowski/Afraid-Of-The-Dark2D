using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HeartEffects : MonoBehaviour
{
    Light2D heartLight;

    float maxLightIntensity;

    bool flickerRunning;

    void Start()
    {
        heartLight = GetComponent<Light2D>();
        maxLightIntensity = heartLight.intensity;
        flickerRunning = false;
    }

    void FixedUpdate()
    {
        if (!flickerRunning)
        {
            flickerRunning = true;
            StartCoroutine(HeartFlicker());
        }
    }

    IEnumerator HeartFlicker()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            heartLight.intensity = Mathf.Lerp(maxLightIntensity, 1.5f, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        heartLight.intensity = maxLightIntensity;
        flickerRunning = false;
    }
}
