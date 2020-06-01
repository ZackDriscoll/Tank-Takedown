using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
public class Bullet : MonoBehaviour
{
    public Transform tf;
    public TankData data;
    public TankMotor motor;

    public float bulletSpeed = 10.0f;
    public float bulletDuration = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
        data = gameObject.GetComponent<TankData>();
        motor = gameObject.GetComponent<TankMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        //Always move forward
        tf.position += tf.forward * bulletSpeed * Time.deltaTime;

        //count down the bullet lifetime timer and if its < 0, destroy it
        bulletDuration -= Time.deltaTime;
        if (bulletDuration <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    

    void OnCollisionEnter(Collision otherObject)
    {
        //Destroy an enemy if they're hit by the bullet
        if (otherObject.gameObject.tag == "Enemy")
        {
            otherObject.gameObject.GetComponent<TankMotor>().DealDamage(motor.bulletDamage);
            Destroy(this.gameObject);
        }
        else if (otherObject.gameObject.tag == "Player")
        {
            //Destroy the player if they're hit by the bullet
            otherObject.gameObject.GetComponent<TankMotor>().DealDamage(motor.bulletDamage);
            Destroy(this.gameObject);
        }
        else
        {
            //Destroys itself on contact with any other object
            Destroy(this.gameObject);
        }
    }
}
