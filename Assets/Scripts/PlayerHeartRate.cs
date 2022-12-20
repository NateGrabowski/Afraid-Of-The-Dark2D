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
    [SerializeField] float heartRateDecreaseRate = 10f;
    [SerializeField] float inDarknessModifier = 8f;
    [Header("GUI")]
    [SerializeField] TextMeshProUGUI displayedHeartRate;

    public float currentHeartRate { get; private set; } = 75f;
    public float timeInDarkness; //Trigger used for exponentially increasing heartrate


    void FixedUpdate()
    {
        Debug.Log(timeInDarkness);
        if (timeInDarkness > 0f)
        {
            timeInDarkness += 1f * Time.deltaTime;
            InDarkness(timeInDarkness);
        }
        else
        {
            LeavingDarkness();
        }
        ConstrainHeartRate();
        displayedHeartRate.text = "bpm = " + Mathf.Round(currentHeartRate).ToString(); //This populates the On Screen heartrate GUI

    }
    float InDarkness(float timeInDarkness)
    {
        float heartRateIncrease = (heartRateIncreaseRate * timeInDarkness) / inDarknessModifier;
        //increase heartrate exponentially based on timeInDarkness


        if (timeInDarkness >= 60f)
        {
            //trigger UI, "you are terrified"
            return currentHeartRate += heartRateIncrease * Time.deltaTime;
        }
        else if (timeInDarkness >= 45f)
        {
            //trigger UI, "you are terrified"
            return currentHeartRate += heartRateIncrease * Time.deltaTime;
        }
        else if (timeInDarkness >= 30f)
        {
            //trigger UI, "you are frightened"
            return currentHeartRate += heartRateIncrease * Time.deltaTime;
        }
        else if (timeInDarkness >= 20f)
        {
            //trigger UI, "you are scared"
            return currentHeartRate += heartRateIncrease * Time.deltaTime;
        }
        else if (timeInDarkness >= 5f)
        {
            //trigger UI, "you are getting worried"
            return currentHeartRate += heartRateIncrease * Time.deltaTime;
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
