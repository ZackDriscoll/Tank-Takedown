using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Powerup powerup;
    public AudioClip FeedbackAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        //Get the other object's powerup controller
        PowerupController powerupController = other.gameObject.GetComponent<PowerupController>();

        if (powerupController != null)
        {
            powerupController.AddPowerup(powerup);

            if (FeedbackAudioClip != null)
            {
                AudioSource.PlayClipAtPoint(FeedbackAudioClip, transform.position, 1.0f);
            }

            Destroy(this.gameObject);
        }
    }
}
