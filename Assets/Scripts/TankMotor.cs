using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(TankData))]
public class TankMotor : MonoBehaviour
{
    //Need a reference to the character controller component
    private CharacterController characterController;
    private Transform tf;

    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        tf = gameObject.GetComponent<Transform>();
    }

    //Handle Moving the tank
    public void Move(float speed)
    {
        //Create a vector to hold our speed data
        //Start with a vector pointing the same direction as the GameObject this script is on
        //Speed vector should be of a length of speed
        Vector3 speedVector = tf.forward * speed;

        //Send speedVector to SimpleMove to handle movement
        //Note: SimpleMove already applies Time.deltaTime to ensure framerate independence
        characterController.SimpleMove(speedVector);
    }

    //Handle Rotating the tank
    public void Rotate(float speed)
    {
        //Create a vector to hold our rotation data
        //Start by rotating by one degree per frame draw
        //Adjust rotation based off speed
        //Multiply by Time.deltaTime to ensure framerate independence
        Vector3 rotateVector = Vector3.up * speed * Time.deltaTime;

        //Pass our rotation vector into transform.rotate
        tf.Rotate(rotateVector, Space.Self);
    }
}
