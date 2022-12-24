using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerHeartRateEffects : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI displayedFearStatus;
    [SerializeField] Color color1;
    [SerializeField] Color color2;
    [SerializeField] GameObject RedScreen;
    [Header("HeartBeats")]
    [SerializeField] AudioSource bpm50;
    [SerializeField] AudioSource bpm70;
    [SerializeField] AudioSource bpm100;
    [SerializeField] AudioSource bpm150;
    [SerializeField] AudioSource bpm170;
    [SerializeField] AudioSource bpm200;
    [SerializeField] AudioSource bpm250;
    PlayerHeartRate playerHeartRate;
    PlayerMovement playerMovement;
    Animator anim;
    Dictionary<float, bool> FearStatusDict;
    int KeyUsed = 0;

    void Start()
    {
        playerHeartRate = GetComponent<PlayerHeartRate>();
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        FearStatusDict = new Dictionary<float, bool>()
        {
            { 50f, false },
            { 70f, false },
            { 100f, false },
            { 150f, false },
            { 170f, false },
            { 200f, false },
            { 250f, false },
            { 300f, false },
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
            case 300f:
                FadeInDeathScreen(heartrate);
                FearStatusText("You're Dead!");
                HeartAttack();
                break;
            case 250f:
                FadeInDeathScreen(heartrate);
                StartCoroutine(SwitchColors(3));
                FearStatusText("Dying");
                break;
            case 200f:
                FadeInDeathScreen(heartrate);
                StartCoroutine(SwitchColors(2));
                FearStatusText("Freaking Out");
                break;
            case 170f:
                FadeInDeathScreen(heartrate);
                StartCoroutine(SwitchColors(2));
                FearStatusText("Panicking");
                break;
            case 150f:
                FadeInDeathScreen(heartrate);
                FearStatusText("Terrified");
                break;
            case 100f:
                FadeInDeathScreen(heartrate);
                FearStatusText("Scared");
                break;
            case 70f:
                FearStatusText("Nervous");
                break;
            case 50f:
                FearStatusText("Calm");
                break;
            default:
                break;
        }
        IncrementPlayerMovement(heartrate);
        HeartBeatSoundEffect(heartrate);
    }//

    void HeartAttack()
    {
        //zoom in on player
        //player dies
        playerMovement.StopMoving();
        anim.SetTrigger("GeraltDying");
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }

    void HeartBeatSoundEffect(float heartrate)
    {
        bpm50.Stop(); bpm70.Stop(); bpm100.Stop(); bpm150.Stop(); bpm170.Stop(); bpm200.Stop(); bpm250.Stop();
        switch (heartrate)
        {
            case 250f:
                bpm250.Play();
                break;
            case 200f:
                bpm200.Play();
                break;
            case 170f:
                bpm170.Play();
                break;
            case 150f:
                bpm150.Play();
                break;
            case 100f:
                bpm100.Play();
                break;
            case 70f:
                bpm70.Play();
                break;
            case 50f:
                bpm50.Play();
                break;
            default:
                break;
        }
    }
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
        while (elapsedTime < 3.5f)
        {
            playerMovement.movementSpeed = Mathf.Lerp(speedBeforeDecrease, 1.5f, elapsedTime / 3.5f);
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
            case 300f:
                color.a = 0.35f;
                RedScreen.GetComponent<Image>().color = color;
                break;
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

    void FearStatusText(string message) //Displays Fear Status
    {
        displayedFearStatus.text = message;
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
