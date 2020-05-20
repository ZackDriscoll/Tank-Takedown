using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private TankData data;
    private TankMotor motor;

    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            motor.Move(data.moveSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            motor.Rotate(data.rotateSpeed);
        }
    }
}
