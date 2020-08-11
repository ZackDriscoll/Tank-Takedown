using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform tf;
    public TankData data;
    public TankMotor motor;
    public Rigidbody rb;

    public float bulletSpeed = 10.0f;
    public float bulletDuration = 3.0f;
    public float damage;

    public string shooterName;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        tf = gameObject.GetComponent<Transform>();
        data = gameObject.GetComponent<TankData>();
        motor = gameObject.GetComponent<TankMotor>();
        rb = gameObject.GetComponent<Rigidbody>();

        rb.AddForce(tf.forward * bulletSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //count down the bullet lifetime timer and if its < 0, destroy it
        bulletDuration -= Time.deltaTime;
        if (bulletDuration <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    

    void OnCollisionEnter(Collision otherObject)
    {
        //Destroy an enemy or player if they're hit by the bullet
        if (otherObject.gameObject.tag == "Enemy" || otherObject.gameObject.tag == "Player")
        {
            if (shooterName == "Player One")
            {
                otherObject.gameObject.GetComponent<TankData>().DealDamage(GameManager.Instance.playerOne.GetComponent<TankData>().bulletDamage); 
            }
            else if (shooterName == "Player Two")
            {
                otherObject.gameObject.GetComponent<TankData>().DealDamage(GameManager.Instance.playerTwo.GetComponent<TankData>().bulletDamage);
            }
            else
            {
                otherObject.gameObject.GetComponent<TankData>().DealDamage(otherObject.gameObject.GetComponent<TankData>().bulletDamage);
            }
        }

        audioSource.clip = AudioClips.Instance.bulletHit;
        audioSource.Play();

        GetComponent<Renderer>().enabled = false;

        Destroy(this.gameObject, 0.5f);
    }
}
