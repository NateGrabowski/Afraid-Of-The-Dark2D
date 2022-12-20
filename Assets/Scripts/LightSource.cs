using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{

    PlayerHeartRate playerHeartRate;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHeartRate = collision.gameObject.GetComponent<PlayerHeartRate>();
            playerHeartRate.timeInDarkness = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHeartRate = collision.gameObject.GetComponent<PlayerHeartRate>();
            playerHeartRate.timeInDarkness++;
        }
    }
}
