using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TankData : MonoBehaviour
{
    //Variables to control movement and rotation speed
    public float moveSpeed = 5.0f;
    public float reverseSpeed = 2.50f;
    public float rotateSpeed = 90.0f;

    //Allow the playing of audio sources
    public AudioSource audioSource;

    //Tank Health System
    public float maxHealth = 30.0f;
    public float currentHealth;
    public int lives = 3;

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
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            audioSource.clip = AudioClips.Instance.tankDeath;
            audioSource.Play();

            if (tag == "Enemy")
            {
                GameManager.Instance.scores.Last<ScoreData>().score += 10;

                if (SaveManager.Instance.score > SaveManager.Instance.score)
                {
                    GameManager.Instance.scores.Last<ScoreData>().SaveScore(); 
                }
            }

            lives--;

            if (lives > 0)
            {
                if (gameObject.GetComponent<InputManager>())
                {
                    GameManager.Instance.Respawn(this.gameObject);
                }
            }
            else
            {
                GameManager.Instance.titleMenu.GameOver();
                Destroy(gameObject);
            }
        }
    }
}
