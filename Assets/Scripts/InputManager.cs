using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
public class InputManager : MonoBehaviour
{
    private TankData data;
    private TankMotor motor;

    public enum InputScheme { WASD, arrowKeys };
    public InputScheme input = InputScheme.WASD;

    // Start is called before the first frame update
    void Start()
    {
        data = gameObject.GetComponent<TankData>();
        motor = gameObject.GetComponent<TankMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
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
                    motor.Move(-data.moveSpeed);
                }
                else
                {
                    motor.Move(0);
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
                    motor.Move(-data.moveSpeed);
                }
                else
                {
                    motor.Move(0);
                }

                //Handle Rotation
                if (Input.GetKey(KeyCode.A))
                {
                    motor.Rotate(data.rotateSpeed);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    motor.Rotate(-data.rotateSpeed);
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
