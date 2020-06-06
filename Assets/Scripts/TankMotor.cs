using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(TankData))]
public class TankMotor : MonoBehaviour
{
    //Need a reference to the character controller and transform components
    private CharacterController characterController;
    private Transform tf;
    public TankData data;

    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        tf = gameObject.GetComponent<Transform>();
        data = gameObject.GetComponent<TankData>();
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

    ///<summary>
    ///Rotates toward a target
    /// </summary>
    /// <param name="target">Target to rotate toward.</param>
    /// <param name="speed">Rotation speed.</param>
    /// <returns>Returns true if it rotated. Returns false if already facing target.</returns>
    public bool RotateTowards(Vector3 target, float speed)
    {
        //Find the vector to the target
        Vector3 vectorToTarget = target - tf.position;

        //Find the quaternion that looks down that vector
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget);

        if (targetRotation == tf.rotation)
        {
            return false;
        }

        tf.rotation = Quaternion.RotateTowards(tf.rotation, targetRotation, speed * Time.deltaTime);

        return true;
    }
}
