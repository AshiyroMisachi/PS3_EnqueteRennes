using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public bool cameraMode;
    void Start()
    {
        //Enable the gyroscope
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Change the rotation of the camera acording to the phone's rotation
        //Need to freeze Z axis
        if (cameraMode)
        {
            transform.rotation = Input.gyro.attitude;
            transform.Rotate(0f, 0f, 180f, Space.Self);
            transform.Rotate(90f, 180f, 0f, Space.World);
        }
    }
}
