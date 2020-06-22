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
    public float maxHealth = 30.0f;
    public float currentHealth;

    public float bulletDamage = 10.0f;
    public float fireRate;

    //Variables to determine how far the AI can see
    public float sightDistance = 20.0f;
    public float SniperSightDistance = 30.0f;

    //Variable to hold the AI's field of view
    public float FOV = 70.0f;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void DealDamage(float damage)
    {
        damage = bulletDamage;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
