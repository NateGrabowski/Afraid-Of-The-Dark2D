using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHeartRateEffects : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayedFearStatus;
    [SerializeField] Color color1;
    [SerializeField] Color color2;
    PlayerHeartRate playerHeartRate;


    void Start()
    {
        playerHeartRate = GetComponent<PlayerHeartRate>();

    }
    private void Update()
    {
        TextFearStatus(playerHeartRate.currentHeartRate);
    }

    void TextFearStatus(float heartrate)
    {
        if (heartrate >= 250f)
        {
            displayedFearStatus.text = "Dying";
        }
        else if (heartrate >= 200f)
        {
            displayedFearStatus.text = "Freaking Out";
        }
        else if (heartrate >= 170f)
        {
            displayedFearStatus.text = "Panicking";
        }
        else if (heartrate >= 150f)
        {
            displayedFearStatus.text = "Terrified";
        }
        else if (heartrate >= 100f)
        {
            displayedFearStatus.text = "Scared";
            StartCoroutine(SwitchColors(5));
        }
        else if (heartrate >= 70f)
        {
            displayedFearStatus.text = "Nervous";
        }
        else if (heartrate >= 50f)
        {
            displayedFearStatus.text = "Calm";
        }

    }
    IEnumerator SwitchColors(int interations)
    {
        while (0 < interations)
        {
            displayedFearStatus.color = color1;
            yield return new WaitForSeconds(0.1f);
            displayedFearStatus.color = color2;
            yield return new WaitForSeconds(0.1f);
            interations--;
        }
    }
    void TransparentFearStatus()
    {
        displayedFearStatus.color = new Color(1, 1, 1, 0);
    }
}
