using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject pickupPrefab;
    public List<GameObject> pickupPrefabs;
    public float spawnDelay;    
    private float nextSpawnTime;
    private Transform tf;
    private GameObject currentPickup;

    // Start is called before the first frame update
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
        nextSpawnTime = Time.time + spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        //If we don't have a currently spawned in pickup, we watn to spawn a new one
        if (currentPickup == null)
        {
            //Once the timer has run down, spawn in a new pickup
            if (Time.time > nextSpawnTime)
            {
                //Select a random prefab from the list
                pickupPrefab = pickupPrefabs[Random.Range(0, pickupPrefabs.Count)];
                //Spawn it
                currentPickup = Instantiate(pickupPrefab, tf.position, Quaternion.identity);
                //Reset the timer
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
        else
        {
            //If we already have a pickup spawned, then we should just reset the timer
            nextSpawnTime = Time.time + spawnDelay;
        }
    }
}
