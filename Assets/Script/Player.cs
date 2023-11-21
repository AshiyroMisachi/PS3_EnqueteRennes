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
    public float timer;

    //Var Button Camera Mode 2
    public GameObject arrow_left;
    public GameObject arrow_right;
    public GameObject arrow_up;
    public GameObject arrow_down;

    //Var Raycast
    public LayerMask mask;
    void Start()
    {
        //Enable the gyroscope
        Input.gyro.enabled = true;

        //Hide UI
        popUpText.alpha = 0f;
        if (cameraMode)
        {
            //Desactive Arrow UI
            arrow_right.gameObject.SetActive(false);
            arrow_up.gameObject.SetActive(false);
            arrow_down.gameObject.SetActive(false);
            arrow_left.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Update timer
        timer = Time.deltaTime;

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
            Camera myCamera = Camera.main;
            Vector3 touchPos = new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, myCamera.farClipPlane);
            Vector3 touchPosInWorld = myCamera.ScreenToWorldPoint(touchPos);
            Debug.DrawLine(myCamera.transform.position, touchPosInWorld, Color.red);
            if (Physics.Raycast(myCamera.transform.position, touchPosInWorld - myCamera.transform.position, out var info, 500, mask))
            {
                info.transform.GetComponent<Proof>().getPickUp(this);
            }
        }
    }
    //Reduce the alpha of the text PopUp
    public void textAlpha(bool start)
    {
        if (start)
        {
            Debug.Log("Start textAlpha");
            popUpText.alpha = 1f;
            //Launch textAlpha(false) in two seconds
        }
        else
        {
            popUpText.alpha = 0f;
        }

    }

    //Camera Mode 2, Move by clicking button
    public void rotateDown()
    {
        Debug.Log("Rotate Down");
    }
    public void rotateUp()
    {
        Debug.Log("Rotate Up");
    }
    public void rotateLeft()
    {
        Debug.Log("Rotate Left");
    }
    public void rotateRight()
    {
        Debug.Log("Rotate Right");
    }
}
