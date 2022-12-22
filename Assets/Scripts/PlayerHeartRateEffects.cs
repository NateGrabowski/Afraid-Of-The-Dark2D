using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

public class PlayerHeartRateEffects : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayedFearStatus;
    [SerializeField] Color color1;
    [SerializeField] Color color2;
    [SerializeField] GameObject RedScreen;
    PlayerHeartRate playerHeartRate;
    PlayerMovement playerMovement;
    Dictionary<float, bool> FearStatusDict;
    int KeyUsed = 0;

    void Start()
    {
        playerHeartRate = GetComponent<PlayerHeartRate>();
        playerMovement = GetComponent<PlayerMovement>();
        FearStatusDict = new Dictionary<float, bool>()
        {
            { 50f, false },
            { 70f, false },
            { 100f, false },
            { 150f, false },
            { 170f, false },
            { 200f, false },
            { 250f, false },
        };
    }
    private void FixedUpdate()
    {
        FearStatus(playerHeartRate.currentHeartRate);
    }

    void FearStatus(float heartrate) //Converts Heart Rate for FearManager to Use
    {
        for (int i = 0; i < FearStatusDict.Count; i++)
        {
            // If heartrate is higher than the current threshold and the threshold is not enabled
            if (heartrate >= FearStatusDict.ElementAt(i).Key && FearStatusDict.ElementAt(i).Value == false)
            {
                // Store the current threshold index
                KeyUsed = i;
                // Enable the current threshold
                FearStatusDict[FearStatusDict.ElementAt(i).Key] = true;
                FearStatusManager(FearStatusDict.ElementAt(i).Key);
            }
            // If heartrate is higher than the current threshold and the threshold is not enabled, but the index is not the same as the stored one
            else if (heartrate >= FearStatusDict.ElementAt(i).Key && FearStatusDict.ElementAt(i).Value == false && i != KeyUsed)
            {
                // Disable the previously stored threshold
                FearStatusDict[FearStatusDict.ElementAt(KeyUsed).Key] = false;
                // Enable the current threshold
                FearStatusDict[FearStatusDict.ElementAt(i).Key] = true;
                FearStatusManager(FearStatusDict.ElementAt(i).Key);
                // Reset the stored threshold index
                KeyUsed = 0;
            }
            // If heartrate is lower than the current threshold and the threshold is enabled
            else if (heartrate < FearStatusDict.ElementAt(i).Key && FearStatusDict.ElementAt(i).Value == true)
            {
                // Disable the current threshold
                FearStatusDict[FearStatusDict.ElementAt(i).Key] = false;
                // Enable the next lower threshold
                FearStatusDict[FearStatusDict.ElementAt(i - 1).Key] = true;
                FearStatusManager(FearStatusDict.ElementAt(i - 1).Key);
                // Reset the stored threshold index
                KeyUsed = 0;
            }
        }
    }

    void FearStatusManager(float heartrate) //Manages all Fear Actions based on Heart Rate
    {
        switch (heartrate)
        {
            case 250f:
                FadeInDeathScreen(heartrate);
                FearStatusText(heartrate, "Dying");
                break;
            case 200f:
                FadeInDeathScreen(heartrate);
                FearStatusText(heartrate, "Freaking Out");
                break;
            case 170f:
                FadeInDeathScreen(heartrate);
                StartCoroutine(SwitchColors(1));
                FearStatusText(heartrate, "Panicking");
                break;
            case 150f:
                FadeInDeathScreen(heartrate);
                FearStatusText(heartrate, "Terrified");
                break;
            case 100f:
                FadeInDeathScreen(heartrate);
                FearStatusText(heartrate, "Scared");
                break;
            case 70f:
                FearStatusText(heartrate, "Nervous");
                break;
            case 50f:
                FearStatusText(heartrate, "Calm");
                break;
            default:
                break;
        }
        IncrementPlayerMovement(heartrate);
    }//


    void IncrementPlayerMovement(float heartrate)
    {
        if (heartrate >= 250)
        {
            StartCoroutine(DecreaseSpeed());
        }
        else
        {
            playerMovement.movementSpeed = 2.5f + (heartrate / 100);
        }
    }

    IEnumerator DecreaseSpeed()
    {
        float speedBeforeDecrease = playerMovement.movementSpeed;
        float elapsedTime = 0f;
        while (elapsedTime < 5f)
        {
            playerMovement.movementSpeed = Mathf.Lerp(speedBeforeDecrease, 1.5f, elapsedTime / 5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        playerMovement.movementSpeed = 1.5f;
    }



    void FadeInDeathScreen(float heartrate) // Increments transparency
    {
        var color = RedScreen.GetComponent<Image>().color;
        switch (heartrate)
        {
            case 250f:
                color.a = 0.3f;
                RedScreen.GetComponent<Image>().color = color;
                break;
            case 200f:
                color.a = 0.1f;
                RedScreen.GetComponent<Image>().color = color;
                break;
            case 170f:
                color.a = 0.05f;
                RedScreen.GetComponent<Image>().color = color;
                break;
            case 150f:
                color.a = 0.01f;
                RedScreen.GetComponent<Image>().color = color;
                break;
            case 100f:
                color.a = 0.0f;
                RedScreen.GetComponent<Image>().color = color;
                break;
        }

    }

    void FearStatusText(float heartrate, string message) //Displays Fear Status
    {
        switch (heartrate)
        {
            case 250f:
                displayedFearStatus.text = message;
                break;
            case 200f:
                displayedFearStatus.text = message;
                break;
            case 170f:
                displayedFearStatus.text = message;
                break;
            case 150f:
                displayedFearStatus.text = message;
                break;
            case 100f:
                displayedFearStatus.text = message;
                break;
            case 70f:
                displayedFearStatus.text = message;
                break;
            case 50f:
                displayedFearStatus.text = message;
                break;
            default:
                break;
        }
    }

    IEnumerator SwitchColors(int iterations)
    {
        for (int i = 0; i < iterations; i++)
        {
            displayedFearStatus.color = color2;
            yield return new WaitForSeconds(0.5f);
            displayedFearStatus.color = color1;
            yield return new WaitForSeconds(0.5f);
        }
    }
    void Fade() //TODO: Not working properly
    {
        // calculate the alpha value that we want to set for the text
        float alpha = Mathf.Abs(Mathf.Sin(Time.time * 0.5f));

        // set the alpha value of the displayedFearStatus's color
        displayedFearStatus.color = new Color(displayedFearStatus.color.r, displayedFearStatus.color.g, displayedFearStatus.color.b, alpha);

    }

}
