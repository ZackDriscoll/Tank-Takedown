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
    public enum LoopType { 
        Stop, 
        Loop, 
        PingPong
    };

    public LoopType loopType;

    private TankData data;
    private TankMotor motor;
    private Transform tf;
    private bool isPatrolForward = true;

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
            if (loopType == LoopType.Stop)
            {
                StopLoop();
            }
            else if (loopType == LoopType.PingPong)
            {
                PingPongLoop();
            }
            else if (loopType == LoopType.Loop)
            {
                LoopLoop();
            }
            else
            {
                Debug.LogWarning("[SampleAIController] Unimplemented Loop Type.");
            }
        }
    }

    void StopLoop()
    {
        if (currentWaypoint < waypoints.Length - 1)
        {
            currentWaypoint++;
        }
    }

    void PingPongLoop()
    {
        if (isPatrolForward)
        {
            if (currentWaypoint < waypoints.Length - 1)
            {
                currentWaypoint++;
            }
            else
            {
                isPatrolForward = false;
                currentWaypoint--;
            }
        }
        else
        {
            if (currentWaypoint > 0)
            {
                currentWaypoint--;
            }
            else
            {
                isPatrolForward = true;
                currentWaypoint++;
            }
        }
    }

    void LoopLoop()
    {
        if (currentWaypoint < waypoints.Length - 1)
        {
            currentWaypoint++;
        }
        else
        {
            currentWaypoint = 0;
        }
    }

    void Patrol()
    {
        
    }
}
