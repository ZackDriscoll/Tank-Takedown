using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    //Bullet/Firepoint variables
    public GameObject bulletPrefab;
    public Transform firePoint;

    //Components to manipulate
    public Transform target;
    private TankData data;
    private TankMotor motor;
    private Transform tf;

    //Variables for the waypoint patrol system
    public Transform[] waypoints;
    public float closeEnough = 1.0f;
    public int currentWaypoint = 0;

    //Enum for the different personalities of the AI
    public enum AIPersonality
    {
        Guard,
        Commander,
        Sniper,
        Coward
    };

    //Enum for the different states for the personalites
    public enum AIState
    {
        Patrol,
        Search,
        Chase,
        Attack,
        Flee
    };

    //Variables to manipulate the personality and state
    public AIPersonality currentPersonality;

    private AIState currentAIState;
    public AIState previousAIState;

    public float stateEnterTime;

    //Variables for shooting timer
    public float timerDelay = 3.0f;
    private float nextEventTime;

    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
        tf = GetComponent<Transform>();

        nextEventTime = Time.time + timerDelay;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentPersonality)
        {
            case AIPersonality.Guard:
                currentAIState = AIState.Patrol;
                GuardTankFSM();
                break;
            case AIPersonality.Commander:
                currentAIState = AIState.Patrol;
                CommanderTankFSM();
                break;
            case AIPersonality.Sniper:
                currentAIState = AIState.Search;
                SniperTankFSM();
                break;
            case AIPersonality.Coward:
                currentAIState = AIState.Patrol;
                CowardTankFSM();
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

    private void GuardTankFSM()
    {
        switch (currentAIState)
        {
            case AIState.Patrol:
                Patrol();
                //Check for transitions
                SeesPlayer(data.moveSpeed);
                break;
            case AIState.Attack:
                Attack();
                break;
        }
    }

    private void CommanderTankFSM()
    {
        switch (currentAIState)
        {
            case AIState.Patrol:
                Patrol();
                //Check for transitions
                SeesPlayer(data.moveSpeed);
                break;
            case AIState.Chase:
                Chase();
                break;
        }
    }

    private void SniperTankFSM()
    {
        switch (currentAIState)
        {
            case AIState.Search:
                Search();
                //Check for transitions
                SeesPlayer(data.moveSpeed);
                break;
            case AIState.Attack:
                Attack();
                break;
        }
    }

    private void CowardTankFSM()
    {
        switch (currentAIState)
        {
            case AIState.Patrol:
                Patrol();
                //Check for transitions
                SeesPlayer(data.moveSpeed);
                //Should we flee?
                if (CheckForFlee() == true)
                {
                    currentAIState = AIState.Flee;
                }
                break;
            case AIState.Flee:
                Flee();
                break;
        }
    }

    private void Patrol()
    {
        //Do the patrol behaviors
        //Could be set to the waypoint system
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
            else
            {
                currentWaypoint = 0;
            }
        }
    }

    private void Chase()
    {
        if (SeesPlayer(data.moveSpeed) == true)
        {
            Vector3 vectorToTarget = target.position - tf.position;
            Vector3 vectorAwayFromTarget = vectorToTarget;

            //Set vector equal to 1 unit so that its magnitude is not equal to vectorToTarget
            vectorAwayFromTarget.Normalize();

            Vector3 fleePosition = vectorAwayFromTarget + tf.position;

            //Rotate and move away from target
            motor.RotateTowards(fleePosition, data.rotateSpeed);
            motor.Move(data.moveSpeed);

            Attack();
        }
    }

    private void Search()
    {
        if (SeesPlayer(data.moveSpeed) == true)
        {
            currentAIState = AIState.Attack;
        }
    }

    private void Attack()
    {
        if (Time.time >= nextEventTime)
        {
            Shoot();
            nextEventTime = Time.time + timerDelay;
        }
    }

    private bool CheckForFlee()
    {
        if (SeesPlayer(data.moveSpeed) == true)
        {
            return true;
        }

        return false;
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

    public bool SeesPlayer(float speed)
    {
        //Raycast forward
        RaycastHit hit;

        if (Physics.Raycast(tf.position, tf.forward, out hit, speed))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Sees Player");
                return true;
            }
        }

        return false;
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        newBullet.GetComponent<Bullet>().damage = data.bulletDamage;
    }
}
