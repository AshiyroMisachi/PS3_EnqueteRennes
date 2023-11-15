using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    public float valueGyrX;
    public float valueGyrY;
    public float valueGyrZ;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get the value of the Gyroscope
        Debug.Log(Input.acceleration);
        valueGyrX = Input.acceleration.x;
        valueGyrY = Input.acceleration.y;
        valueGyrZ = Input.acceleration.z;


        //Rotation of the camera
        if (valueGyrZ > 0.4)
        {
            transform.eulerAngles += new Vector3(0.1f, 0f, 0f);
            Debug.Log("Descend");
        }
        else if (valueGyrZ < -0.4)
        {
            transform.eulerAngles += new Vector3(-0.1f, 0f, 0f);
            Debug.Log("Monte");
        }
    }
}
