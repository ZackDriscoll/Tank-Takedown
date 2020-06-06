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

    // Start is called before the first frame update
    void Start()
    {
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
            otherObject.gameObject.GetComponent<TankData>().DealDamage(damage);
            Destroy(this.gameObject);
        }
        else
        {
            //Destroys itself on contact with any other object
            Destroy(this.gameObject);
        }
    }
}
