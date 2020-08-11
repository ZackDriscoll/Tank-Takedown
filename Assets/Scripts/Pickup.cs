using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Powerup powerup;

    public AudioSource audioSource;

    public void OnTriggerEnter(Collider other)
    {
        //Get the other object's powerup controller
        PowerupController powerupController = other.gameObject.GetComponent<PowerupController>();

        if (powerupController != null)
        {
            powerupController.AddPowerup(powerup);
            
            audioSource.Play();

            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            Destroy(this.gameObject, 0.5f);
        }
    }
}
