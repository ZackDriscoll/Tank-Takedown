using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
public class SampleAIController : MonoBehaviour
{
    public Transform[] waypoints;
    public float closeEnough = 1.0f;
    public int currentWaypoint = 0;

    private TankData data;
    private TankMotor motor;
    private Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        data = gameObject.GetComponent<TankData>();
        motor = gameObject.GetComponent<TankMotor>();
        tf = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (motor.RotateTowards(waypoints[currentWaypoint].position, data.rotateSpeed))
        {
            //Do nothing
        }
        else
        {
            motor.Move(data.moveSpeed);
        }

        if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) < (closeEnough * closeEnough))
        {
            if (currentWaypoint < waypoints.Length - 1)
            {
                currentWaypoint++;
            }
        }
    }
}
