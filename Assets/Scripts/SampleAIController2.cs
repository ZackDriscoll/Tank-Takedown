using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
public class SampleAIController2 : MonoBehaviour
{
    public enum AttackMode
    {
        Chase,
        Flee
    };
    public AttackMode attackMode;
    public Transform target;

    private TankData data;
    private TankMotor motor;
    private Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackMode == AttackMode.Chase)
        {
            //Rotate towards our target
            motor.RotateTowards(target.position, data.rotateSpeed);

            //Move towards our target
            motor.Move(data.moveSpeed);
        }
        if (attackMode == AttackMode.Flee)
        {
            Vector3 vectorToTarget = target.position - tf.position;
            Vector3 vectorAwayFromTarget = -vectorToTarget;

            //Set vector equal to 1 unit so that its magnitude is not equal to vectorToTarget
            vectorAwayFromTarget.Normalize();

            Vector3 fleePosition = vectorAwayFromTarget + tf.position;

            //Rotate and move away from target
            motor.RotateTowards(fleePosition, data.rotateSpeed);
            motor.Move(data.moveSpeed);
        }
    }
}
