using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    //Variables to control movement and rotation speed
    public float moveSpeed = 5.0f;
    public float reverseSpeed = 2.50f;
    public float rotateSpeed = 90.0f;

    //Tank Health System
    public float maxHealth = 50.0f;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }
}
