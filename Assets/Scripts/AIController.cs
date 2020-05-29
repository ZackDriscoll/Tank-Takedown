using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    //This timer counts up from a certain time.

    public float timerDelay = 3.0f;

    private float nextEventTime;

    // Start is called before the first frame update
    void Start()
    {
        nextEventTime = Time.time + timerDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextEventTime)
        {
            Shoot();
            nextEventTime = Time.time + timerDelay;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
