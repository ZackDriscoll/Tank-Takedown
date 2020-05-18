using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer3Example : MonoBehaviour
{
    //This timer activates based on the last time the player did a thing

    public float timerDelay = 3.0f;

    private float lastEventTime;

    // Start is called before the first frame update
    void Start()
    {
        lastEventTime = Time.time - timerDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= lastEventTime + timerDelay)
        {
            Debug.Log("Timer 3 completed.");
            lastEventTime = Time.time;
        }
    }
}
