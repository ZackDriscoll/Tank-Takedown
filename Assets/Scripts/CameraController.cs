using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += offset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;

        transform.LookAt(player.transform.position);
    }
}
