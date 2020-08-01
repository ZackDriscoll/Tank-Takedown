using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
public class InputManager : MonoBehaviour
{
    private TankData data;
    private TankMotor motor;

    //Variables to create and manipulate bullets
    public GameObject bulletPrefab;
    public Transform firePoint;

    //Allows playing of audio sources
    public AudioSource audioSource;

    //Variables to control timer for shooting
    public float timerDelay = 1.50f;
    private float lastEventTime;

    public enum InputScheme { WASD, arrowKeys };
    public InputScheme input;

    // Start is called before the first frame update
    void Start()
    {
        data = gameObject.GetComponent<TankData>();
        motor = gameObject.GetComponent<TankMotor>();

        lastEventTime = Time.time - timerDelay;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    public void MovePlayer(Vector3 newPostion)
    {
        //gameObject.transform.position = new Vector3 (newPostion.x, newPostion.y, newPostion.z);
        motor.characterController.enabled = false;
        motor.characterController.transform.position = newPostion;
        motor.characterController.enabled = true;
    }

    void HandleInput()
    {
        switch (input)
        {
            case InputScheme.arrowKeys:
                //Handle Movement
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    motor.Move(data.moveSpeed);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    motor.Move(-data.reverseSpeed);
                }
                else
                {
                    motor.Move(0);
                }

                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    if (Time.time >= lastEventTime + timerDelay)
                    {
                        /*audioSource.clip = AudioClips.audioClips.tankFire;
                        audioSource.Play();*/

                        motor.Shoot(bulletPrefab, firePoint);
                        lastEventTime = Time.time;
                    }
                }

                //Handle Rotation
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    motor.Rotate(data.rotateSpeed);
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    motor.Rotate(-data.rotateSpeed);
                }
                break;

            case InputScheme.WASD:
                //Handle Movement
                if (Input.GetKey(KeyCode.W))
                {
                    motor.Move(data.moveSpeed);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    motor.Move(-data.reverseSpeed);
                }
                else
                {
                    motor.Move(0);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (Time.time >= lastEventTime + timerDelay)
                    {
                        /*audioSource.clip = AudioClips.audioClips.tankFire;
                        audioSource.Play();*/

                        motor.Shoot(bulletPrefab, firePoint);
                        lastEventTime = Time.time;
                    }
                }

                //Handle Rotation
                if (Input.GetKey(KeyCode.A))
                {
                    motor.Rotate(-data.rotateSpeed);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    motor.Rotate(data.rotateSpeed);
                }
                else
                {
                    motor.Rotate(0);
                }
                break;
            default:
                Debug.LogError("[InputManager] Undefined input scheme.");
                break;
        }

        
    }
}
