using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAIController4 : MonoBehaviour
{
    public Transform target;

    private TankData data;
    private TankMotor motor;
    private Transform tf;

    public enum AIPersonality
    {
        Cautious,
        Aggressive
    };

    public enum AIState
    {
        Patrol,
        WaitForBackup,
        Advance,
        Attack,
        Flee
    };

    public AIPersonality currentPersonality;

    private AIState currentAIState;
    public AIState previousAIState;

    public float stateEnterTime;

    // Start is called before the first frame update
    private void Start()
    {
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
        tf = GetComponent<Transform>();
    }

    private void Update()
    {
        switch (currentPersonality)
        {
            case AIPersonality.Cautious:
                currentAIState = AIState.Patrol;
                CautiousTankFSM();
                break;
            case AIPersonality.Aggressive:
                //Do the state machine
                break;
            default:
                Debug.LogWarning("Unimplemented Personality.");
                break;
        }
    }

    public void ChangeState(AIState newState)
    {
        //Save the previous state
        previousAIState = currentAIState;

        //Change to new state
        currentAIState = newState;

        //Save the time we changed states
        stateEnterTime = Time.time;
    }

    private void CautiousTankFSM()
    {
        switch (currentAIState)
        {
            case AIState.Patrol:
                Patrol();
                //Check for transitions
                //Should we flee?
                if (CheckForFlee())
                {
                    Flee();
                }
                //Should we wait for backup?
                break;
            case AIState.WaitForBackup:
                break;
            case AIState.Advance:
                break;
            case AIState.Attack:
                break;
            case AIState.Flee:
                break;
        }
    }

    private void Flee()
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

    private void Patrol()
    {
        //Do the patrol behaviors
        //Could be set to the waypoint system
    }

    private bool CheckForFlee()
    {
        //TODO: Implement Check For Flee.
        return false;
    }
}
