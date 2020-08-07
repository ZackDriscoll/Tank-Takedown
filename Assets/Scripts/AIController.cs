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

    public LayerMask layerMask = 8;

    //Enum for the different personalities of the AI
    public enum AIPersonality
    {
        Guard,
        Kamikaze,
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

    public AIState currentAIState;
    public AIState previousAIState;

    public float stateEnterTime;
    public float attackTime = 3.5f;

    //Variables for shooting timer
    public float timerDelay = 3.0f;
    private float nextEventTime;

    [SerializeField]private float safeDistance;

    void Initialize()
    {
        float distanceToWaypoint = 0;

        int index = 0;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (Vector3.Distance(tf.position, waypoints[i].position) < distanceToWaypoint)
            {
                index = i;
                distanceToWaypoint = Vector3.Distance(tf.position, waypoints[i].position);
            }
        }

        currentWaypoint = index;
    }

    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
        tf = GetComponent<Transform>();

        nextEventTime = Time.time + timerDelay;

        switch (currentPersonality)
        {
            case AIPersonality.Guard:
                currentAIState = AIState.Patrol;
                break;
            case AIPersonality.Kamikaze:
                currentAIState = AIState.Patrol;
                break;
            case AIPersonality.Sniper:
                currentAIState = AIState.Search;
                break;
            case AIPersonality.Coward:
                currentAIState = AIState.Patrol;
                break;
            default:
                Debug.LogWarning("Unimplemented Personality.");
                break;
        }

        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentPersonality)
        {
            case AIPersonality.Guard:
                GuardTankFSM();
                break;
            case AIPersonality.Kamikaze:
                KamikazeTankFSM();
                break;
            case AIPersonality.Sniper:
                SniperTankFSM();
                break;
            case AIPersonality.Coward:
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
                if (GameManager.Instance.playerOne != null)
                {
                    if (SeesPlayer(data.sightDistance, GameManager.Instance.playerOne))
                    {
                        Debug.Log("Player was seen and we're changing state.");
                        ChangeState(AIState.Attack);
                    } 
                }

                if (GameManager.Instance.playerTwo != null)
                {
                    if (SeesPlayer(data.sightDistance, GameManager.Instance.playerTwo))
                    {
                        Debug.Log("Player was seen and we're changing state.");
                        ChangeState(AIState.Attack);
                    } 
                }
                break;
            case AIState.Attack:
                Debug.Log("Current State = Attack");
                Attack();
                //Check for transitions
                if (GameManager.Instance.playerOne != null)
                {
                    if (!SeesPlayer(data.sightDistance, GameManager.Instance.playerOne))
                    {
                        ChangeState(AIState.Patrol);
                    } 
                }

                if (GameManager.Instance.playerTwo != null)
                {
                    if (SeesPlayer(data.sightDistance, GameManager.Instance.playerTwo))
                    {
                        ChangeState(AIState.Patrol);
                    }
                }
                break;
        }
    }

    private void KamikazeTankFSM()
    {
        switch (currentAIState)
        {
            case AIState.Patrol:
                Patrol();
                //Check for transitions
                if (GameManager.Instance.playerOne != null)
                {
                    if (SeesPlayer(data.sightDistance, GameManager.Instance.playerOne))
                    {
                        ChangeState(AIState.Chase);
                    } 
                }

                if (GameManager.Instance.playerTwo != null)
                {
                    if (SeesPlayer(data.sightDistance, GameManager.Instance.playerTwo))
                    {
                        ChangeState(AIState.Chase);
                    }
                }
                break;
            case AIState.Chase:
                Chase();
                //Check for transitions
                if (GameManager.Instance.playerOne != null)
                {
                    if (!SeesPlayer(data.sightDistance, GameManager.Instance.playerOne))
                    {
                        ChangeState(AIState.Patrol);
                    } 
                }

                if (GameManager.Instance.playerTwo != null)
                {
                    if (!SeesPlayer(data.sightDistance, GameManager.Instance.playerTwo))
                    {
                        ChangeState(AIState.Patrol);
                    }
                }
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
                if (GameManager.Instance.playerOne != null)
                {
                    if (SeesPlayer(data.SniperSightDistance, GameManager.Instance.playerOne))
                    {
                        ChangeState(AIState.Attack);
                    } 
                }

                if (GameManager.Instance.playerTwo != null)
                {
                    if (SeesPlayer(data.SniperSightDistance, GameManager.Instance.playerTwo))
                    {
                        ChangeState(AIState.Attack);
                    }
                }
                break;
            case AIState.Attack:
                Attack();
                if (GameManager.Instance.playerOne != null)
                {
                    if (SeesPlayer(data.SniperSightDistance, GameManager.Instance.playerOne))
                    {
                        ChangeState(AIState.Search);
                    }
                }

                if (GameManager.Instance.playerTwo != null)
                {
                    if (SeesPlayer(data.SniperSightDistance, GameManager.Instance.playerTwo))
                    {
                        ChangeState(AIState.Search);
                    }
                }
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
                if (GameManager.Instance.playerOne != null)
                {
                    if (SeesPlayer(data.SniperSightDistance, GameManager.Instance.playerOne))
                    {
                        if (CheckForFlee())
                        {
                            ChangeState(AIState.Flee); 
                        }
                    }
                }

                if (GameManager.Instance.playerTwo != null)
                {
                    if (SeesPlayer(data.SniperSightDistance, GameManager.Instance.playerTwo))
                    {
                        if (CheckForFlee())
                        {
                            ChangeState(AIState.Flee);
                        }
                    }
                }
                break;
            case AIState.Flee:
                Flee();
                //Check for transitions
                if (GameManager.Instance.playerOne != null)
                {
                    if (SeesPlayer(data.SniperSightDistance, GameManager.Instance.playerOne) && Vector3.Distance(tf.position, GameManager.Instance.playerOne.transform.position) > safeDistance)
                    {
                        ChangeState(AIState.Patrol);
                    }
                }

                if (GameManager.Instance.playerTwo != null)
                {
                    if (SeesPlayer(data.SniperSightDistance, GameManager.Instance.playerTwo) && Vector3.Distance(tf.position, GameManager.Instance.playerTwo.transform.position) > safeDistance)
                    {
                        ChangeState(AIState.Patrol);
                    }
                }
                break;
        }
    }

    private void Patrol()
    {
        //Do the patrol behaviors
        //Could be set to the waypoint system
        Debug.Log(waypoints.Length);
        if (motor.RotateTowards(waypoints[currentWaypoint].position, data.rotateSpeed, false))
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
        if (GameManager.Instance.playerOne != null)
        {
            if (SeesPlayer(data.sightDistance, GameManager.Instance.playerOne))
            {
                Vector3 vectorToTarget = target.position - tf.position;
                Vector3 vectorAwayFromTarget = vectorToTarget;

                //Set vector equal to 1 unit so that its magnitude is not equal to vectorToTarget
                vectorAwayFromTarget.Normalize();

                Vector3 fleePosition = vectorAwayFromTarget + tf.position;

                //Rotate and move away from target
                motor.RotateTowards(fleePosition, data.rotateSpeed, false);
                motor.Move(data.moveSpeed);
            } 
        }

        if (GameManager.Instance.playerTwo != null)
        {
            if (SeesPlayer(data.sightDistance, GameManager.Instance.playerTwo))
            {
                Vector3 vectorToTarget = target.position - tf.position;
                Vector3 vectorAwayFromTarget = vectorToTarget;

                //Set vector equal to 1 unit so that its magnitude is not equal to vectorToTarget
                vectorAwayFromTarget.Normalize();

                Vector3 fleePosition = vectorAwayFromTarget + tf.position;

                //Rotate and move away from target
                motor.RotateTowards(fleePosition, data.rotateSpeed, false);
                motor.Move(data.moveSpeed);
            }
        }
    }

    private void Search()
    {
        motor.Rotate(data.rotateSpeed);
    }

    private void Attack()
    {
        Debug.Log("Attacking");
        motor.RotateTowards(target.position, data.rotateSpeed, false);
        if (Time.time >= nextEventTime)
        {
            motor.Shoot(bulletPrefab, firePoint);
            nextEventTime = Time.time + timerDelay;
        }
    }

    private bool CheckForFlee()
    {
        if (GameManager.Instance.playerOne != null)
        {
            if (SeesPlayer(data.sightDistance, GameManager.Instance.playerOne))
            {
                return true;
            }             
        }

        if (GameManager.Instance.playerTwo != null)
        {
            if (SeesPlayer(data.sightDistance, GameManager.Instance.playerTwo))
            {
                return true;
            }
        }

        return false;
    }

    private void Flee()
    {
        //Rotate and move away from target
        motor.RotateTowards(target.position, data.rotateSpeed, true);
        motor.Move(data.moveSpeed);
    }

    public bool SeesPlayer(float sight, GameObject player)
    {
        Vector3 vectorToTarget = player.transform.position - tf.position;        

        float angleToTarget = Vector3.Angle(vectorToTarget, tf.forward);

        if (angleToTarget < (data.FOV / 2.0f))
        {
            //Raycast forward
            RaycastHit hit;

            if (Physics.Raycast(tf.position, vectorToTarget, out hit, sight, ~layerMask))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Sees Player");
                    target = hit.transform;
                    return true;
                }
            }
        }        

        return false;
    }

    //Will destroy itself and the player on collision
    void OnTriggerEnter(Collider otherObject)
    {
        Debug.Log(otherObject);
        if (otherObject.gameObject.GetComponent<InputManager>())
        {
            Debug.Log("Destroy Player.");
            GameManager.Instance.Respawn(otherObject.gameObject);
            Destroy(this.gameObject);
        }        
    }
}
