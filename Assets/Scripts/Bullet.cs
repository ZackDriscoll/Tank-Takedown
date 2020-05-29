using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
public class Bullet : MonoBehaviour
{
    public Transform tf;
    public TankData data;

    public float bulletSpeed = 10.0f;
    public float bulletDamage = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
        data = gameObject.GetComponent<TankData>();
    }

    // Update is called once per frame
    void Update()
    {
        //Always move forward
        tf.position += tf.forward * bulletSpeed * Time.deltaTime;
    }

    void DealDamage()
    {
        data.currentHealth -= bulletDamage;

        if (data.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider otherObject)
    {
        //Destroy an enemy if they're hit by the bullet
        if (otherObject.gameObject.tag == "Enemy")
        {
            DealDamage();
            Destroy(this.gameObject);
        }
        else if (otherObject.gameObject.tag == "Player")
        {
            //Destroy the player if they're hit by the bullet
            DealDamage();
            Destroy(this.gameObject);
        }
        else
        {
            //Destroys itself on contact with any other object
            Destroy(this.gameObject);
        }
    }
}
