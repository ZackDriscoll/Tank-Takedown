using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    //public List<Camera> cameras;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += offset;

        /*cameras = GetComponents<Camera>();
         GetCameraPositions();*/
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GameManager.Instance.player.transform.position + offset;

        transform.LookAt(GameManager.Instance.player.transform.position);
    }

    private void GetCameraPositions()
    {
        /*if (cameras.Count == 1)
        {
            cameras[0].rect = new Rect(0, 0, 1, 1);
        }
        else
        {
            cameras[0].rect = new Rect(0, 0, 1, 0.5f);
            cameras[0].rect = new Rect(0, 0.5f, 1, 0.5f);
        }*/
    }
}
