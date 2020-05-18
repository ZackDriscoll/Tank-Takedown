using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer1Example : MonoBehaviour
{
    //This timer starts at zero and counts up to the set time

    //Wait 3 sec before firing next shot
    public float timerDelay = 3.0f;

    private float timeUntilNextEvent = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeUntilNextEvent > 0)
        {
            timeUntilNextEvent -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Timer 1 has ended.");
            timeUntilNextEvent = timerDelay;
        }
    }
}
