using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAIController3 : MonoBehaviour
{
    public Transform target;
    public float avoidanceTime = 2.0f;

    private float exitTime;
    private Transform tf;
    private TankData data;
    private TankMotor motor;

    public enum AvoidStage { notAvoiding, rotateUntilCanMove, moveForSeconds}
    public AvoidStage avoidanceStage;

    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
        data = gameObject.GetComponent<TankData>();
        motor = gameObject.GetComponent<TankMotor>();
    }

    void Update()
    {
        if (!(avoidanceStage == AvoidStage.notAvoiding))
        {
            Avoid();
        }
        else
        {
            Chase();
        }
    }

    void Chase()
    {
        if (CanMove(data.moveSpeed))
        {
            //Move if can move
            if (motor.RotateTowards(target.position, data.rotateSpeed))
            {
                //Do nothing
            }
            else
            {
                motor.Move(data.moveSpeed);
            }
        }
        else
        {
            avoidanceStage = AvoidStage.rotateUntilCanMove;
        }        
    }

    void Avoid()
    {
        if (avoidanceStage == AvoidStage.rotateUntilCanMove)
        {
            motor.Rotate(-1 * data.rotateSpeed);

            if (CanMove(data.moveSpeed))
            {
                avoidanceStage = AvoidStage.moveForSeconds;
                exitTime = avoidanceTime;
            }
        }
        else if (avoidanceStage == AvoidStage.moveForSeconds)
        {
            if (CanMove(data.moveSpeed))
            {
                exitTime -= Time.deltaTime;
                motor.Move(data.moveSpeed);

                if (exitTime <= 0)
                {
                    avoidanceStage = AvoidStage.notAvoiding;
                }
            }
        }
        else
        {
            avoidanceStage = AvoidStage.rotateUntilCanMove;
        }
    }

    public bool CanMove(float speed)
    {
        //Raycast forward
        RaycastHit hit;

        if (Physics.Raycast(tf.position, tf.forward, out hit, speed))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                return false;
            }
        }

        return true;
    }
}
