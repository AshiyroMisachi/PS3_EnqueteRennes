using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Var Player
    public bool cameraMode;
    public List<Proof> proofs;
    public TextMeshProUGUI popUpText;
    public float timerText;

    //Var Button Camera Mode 2
    //Left, Right, Up, Down
    public GameObject[] arrows;

    //Var Police report
    public GameObject policeReport;

    //Var Raycast
    public LayerMask mask;
    private bool raycastOneTime;

    void Start()
    {
        //Enable the gyroscope
        Input.gyro.enabled = true;

        //Hide UI
        popUpText.alpha = 0f;
        if (cameraMode)
        {
            //Desactive Arrow UI
            for (int i = 0; i < arrows.Length; i++)
            {
                arrows[i].gameObject.SetActive(false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        

        //Update timer
        timerText += Time.deltaTime;

        //Change the rotation of the camera acording to the phone's rotation
        //Need to freeze Z axis
        if (cameraMode)
        {
            transform.rotation = Input.gyro.attitude;
            transform.Rotate(0f, 0f, 180f, Space.Self);
            transform.Rotate(90f, 180f, 0f, Space.World);
        }

        //Raycast when touch screen to detect object
        if (Input.touchCount > 0)
        {
            if(!raycastOneTime)
            {
                Camera myCamera = Camera.main;
                Vector3 touchPos = new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, myCamera.farClipPlane);
                Vector3 touchPosInWorld = myCamera.ScreenToWorldPoint(touchPos);
                Debug.DrawLine(myCamera.transform.position, touchPosInWorld, Color.red);
                if (Physics.Raycast(myCamera.transform.position, touchPosInWorld - myCamera.transform.position, out var info, 500, mask))
                {
                    info.transform.GetComponent<Proof>().getPickUp(this);
                }
            }
            raycastOneTime = true;
        }
        else
        {
            raycastOneTime = false;
        }

        //Hide Text after pickup an object
        if (timerText > 2f)
        {
            popUpText.alpha -= 0.005f;
        }
    }
    //Camera Mode 2, Move by clicking button
    public void rotateDown()
    {
        transform.eulerAngles += Vector3.right;
    }
    public void rotateUp()
    {
        transform.eulerAngles += Vector3.left;
    }
    public void rotateLeft()
    {
        transform.eulerAngles += Vector3.down;
    }
    public void rotateRight()
    {
        transform.eulerAngles += Vector3.up;
    }

    //Destroy Police report
    public void destroyPolicereport()
    {
        Destroy(policeReport);
    }
}
