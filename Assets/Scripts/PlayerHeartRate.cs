using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerHeartRate : MonoBehaviour
{
    [Header("Heartrate Modifiers")]
    [SerializeField] float maxHeartRate = 250f;
    [SerializeField] float minHeartRate = 50f;
    [SerializeField] float heartRateIncreaseRate = 1f;
    [SerializeField] float heartRateDecreaseRate = 0.5f;
    [Header("GUI")]
    [SerializeField] TextMeshProUGUI displayedHeartRate;

    public float currentHeartRate { get; private set; } = 75f;
    public float timeInDarkness; //Trigger used for exponentially increasing heartrate





    void FixedUpdate()
    {
        InDarkness(timeInDarkness);
        ConstrainHeartRate();
        displayedHeartRate.text = "bpm = " + Mathf.Round(currentHeartRate).ToString();


    }
    float InDarkness(float timeInDarkness)
    {
        //increase heartrate exponentially based on timeInDarkness
        if (timeInDarkness >= 120f)
        {
            //trigger UI, "you are extremely terrified"
            return currentHeartRate += heartRateIncreaseRate * Time.deltaTime;
        }
        else if (timeInDarkness >= 90f)
        {
            //trigger UI, "you are really terrified"
            return currentHeartRate += heartRateIncreaseRate * Time.deltaTime;
        }
        else if (timeInDarkness >= 60f)
        {
            //trigger UI, "you are terrified"
            return currentHeartRate += heartRateIncreaseRate * Time.deltaTime;
        }
        else if (timeInDarkness >= 45f)
        {
            //trigger UI, "you are terrified"
            return currentHeartRate += heartRateIncreaseRate * Time.deltaTime;
        }
        else if (timeInDarkness >= 30f)
        {
            //trigger UI, "you are frightened"
            return currentHeartRate += heartRateIncreaseRate * Time.deltaTime;
        }
        else if (timeInDarkness >= 20f)
        {
            //trigger UI, "you are scared"
            return currentHeartRate += heartRateIncreaseRate * Time.deltaTime;
        }
        else if (timeInDarkness >= 5f)
        {
            //trigger UI, "you are getting worried"
            return currentHeartRate += heartRateIncreaseRate * Time.deltaTime;
        }
        else // steadily increase heart rate
        {
            return currentHeartRate += heartRateIncreaseRate * Time.deltaTime;
        }
    }
    float LeavingDarkness()
    {
        return currentHeartRate -= heartRateDecreaseRate * Time.deltaTime;
    }
    void ConstrainHeartRate()
    {
        // cap the heart rate
        if (currentHeartRate > maxHeartRate)
        {
            currentHeartRate = maxHeartRate;
        }
        else if (currentHeartRate < minHeartRate)
        {
            currentHeartRate = minHeartRate;
        }
    }
    float GhostContact(float heartrate)
    {
        return currentHeartRate += heartrate;
    }
}
