using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class PlayerHeartRateEffects : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayedFearStatus;
    [SerializeField] Color color1;
    [SerializeField] Color color2;

    PlayerHeartRate playerHeartRate;
    Dictionary<float, bool> FearStatusDict;
    int KeyUsed = 0;

    void Start()
    {
        playerHeartRate = GetComponent<PlayerHeartRate>();
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

    //void FearStatus(float heartrate)
    //{

    //    //WORKING BUT DOESNT GO DOWN CORRECTLY. THIS ONE ITERATES ONLY ONCE GOING UP
    //    for (int i = 0; i < FearStatusDict.Count; i++)
    //    {
    //        // If heartrate is higher than the current threshold and the threshold is not enabled
    //        if (heartrate >= FearStatusDict.ElementAt(i).Key && FearStatusDict.ElementAt(i).Value == false)
    //        {
    //            // Store the current threshold index
    //            KeyUsed = i;
    //            // Enable the current threshold
    //            FearStatusDict[FearStatusDict.ElementAt(i).Key] = true;
    //            FearStatusText(FearStatusDict.ElementAt(i).Key);
    //        }
    //        // If heartrate is higher than the current threshold and the threshold is not enabled, but the index is not the same as the stored one
    //        else if (heartrate >= FearStatusDict.ElementAt(i).Key && FearStatusDict.ElementAt(i).Value == false && i != KeyUsed)
    //        {
    //            // Disable the previously stored threshold
    //            FearStatusDict[FearStatusDict.ElementAt(KeyUsed).Key] = false;
    //            // Enable the current threshold
    //            FearStatusDict[FearStatusDict.ElementAt(i).Key] = true;
    //            FearStatusText(FearStatusDict.ElementAt(i).Key);
    //            // Reset the stored threshold index
    //            KeyUsed = 0;
    //        }
    //        // If heartrate is lower than the current threshold and the threshold is enabled, but the index is not the same as the stored one
    //        else if (heartrate < FearStatusDict.ElementAt(i).Key && FearStatusDict.ElementAt(i).Value == true && i != KeyUsed)
    //        {
    //            // Disable the previously stored threshold
    //            FearStatusDict[FearStatusDict.ElementAt(KeyUsed).Key] = false;
    //            // Enable the current threshold
    //            FearStatusDict[FearStatusDict.ElementAt(i).Key] = true;
    //            FearStatusText(FearStatusDict.ElementAt(i).Key);
    //            // Reset the stored threshold index
    //            KeyUsed = 0;

    //        }
    //    }

    //}

    void FearStatus(float heartrate)
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
                FearStatusText(FearStatusDict.ElementAt(i).Key);
            }
            // If heartrate is higher than the current threshold and the threshold is not enabled, but the index is not the same as the stored one
            else if (heartrate >= FearStatusDict.ElementAt(i).Key && FearStatusDict.ElementAt(i).Value == false && i != KeyUsed)
            {
                // Disable the previously stored threshold
                FearStatusDict[FearStatusDict.ElementAt(KeyUsed).Key] = false;
                // Enable the current threshold
                FearStatusDict[FearStatusDict.ElementAt(i).Key] = true;
                FearStatusText(FearStatusDict.ElementAt(i).Key);
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
                FearStatusText(FearStatusDict.ElementAt(i - 1).Key);
                // Reset the stored threshold index
                KeyUsed = 0;
            }
        }
    }
    void FearStatusText(float heartrate)
    {
        Debug.Log("FearStatusText " + heartrate);
        switch (heartrate)
        {
            case 250f:
                displayedFearStatus.text = "Dying";
                break;
            case 200f:
                displayedFearStatus.text = "Freaking Out";
                break;
            case 170f:
                displayedFearStatus.text = "Panicking";
                break;
            case 150f:
                displayedFearStatus.text = "Terrified";
                break;
            case 100f:
                displayedFearStatus.text = "Scared";
                break;
            case 70f:
                displayedFearStatus.text = "Nervous";
                break;
            case 50f:
                displayedFearStatus.text = "Calms";
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
            yield return new WaitForSeconds(1);
            displayedFearStatus.color = color1;
            yield return new WaitForSeconds(1);
        }
    }
    void TransparentFearStatus()
    {
        displayedFearStatus.color = new Color(1, 1, 1, 0);
    }
}
