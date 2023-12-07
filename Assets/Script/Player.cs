using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Data Holder
    public DataHolder dataholder;

    public float lerpFactor;

    //Var Player
    public bool cameraMode;
    public int numberProof;

    public TextMeshProUGUI popUpText;
    public float timerText;

    public string newsPaperScene;

    //Var Button Camera Mode 2
    //Left, Right, Up, Down
    public GameObject[] arrows;

    //Var Police report
    public GameObject policeReport;

    //Var Raycast
    public LayerMask mask;
    private bool raycastOneTime;

    public Light prefabLight;
    private Light clueLight;

    void Start()
    {
        //Find dataholder
        dataholder = FindObjectOfType<DataHolder>();

        //Enable the gyroscope
        Input.gyro.enabled = true;

        //Initialize parameters
        cameraMode = dataholder.cameraMode;

        //Parameters Switch Scene
        if (dataholder.levelStarted)
        {
            Destroy(policeReport);
        }
        else
        {
            dataholder.setupArray(numberProof);
            dataholder.proofsLevel[0] = true;
            dataholder.proofsName[0] = "Rapport de Police";
            dataholder.proofsDescription[0] = "Ceci est le rapport du meurtre délivré par la police";
        }

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

        Camera myCamera = Camera.main;

        clueLight = Instantiate(prefabLight, myCamera.transform.position, Quaternion.Euler(0, 0, 0));

    }

    void Update()
    {
        if (dataholder.levelStarted)
        {
            //Update timer
            timerText += Time.deltaTime;

            //Change the rotation of the camera acording to the phone's rotation
            //Need to freeze Z axis
            if (cameraMode)
            {

                transform.rotation = Input.gyro.attitude;
                
                // Attempt to make a smooth rotation of the camera when using gyroscoping (not working atm)

                //Vector3 newRotation = Vector3.Lerp(transform.eulerAngles, Input.gyro.attitude.eulerAngles, lerpFactor);
                //transform.rotation = Quaternion.Euler(newRotation.x, newRotation.y, newRotation.z);
                
                transform.Rotate(0f, 0f, 180f, Space.Self);
                transform.Rotate(90f, 180f, 0f, Space.World);
            }

            Camera myCamera = Camera.main;

            if (Physics.Raycast(myCamera.transform.position, myCamera.transform.forward * 500, out var infoBis, 500, mask))
            {
                Proof proofDetected = infoBis.transform.GetComponent<Proof>();

                clueLight.intensity = 1;

                // Light appears at mid distance between the origin of the player and the origin of the proof
                Vector3 lightPosition = gameObject.transform.position - ((gameObject.transform.position - proofDetected.transform.position) / 2);

                clueLight.transform.position = lightPosition;
            }

            else
            {
                clueLight.intensity = 0;
            }

            //Raycast when touch screen to detect object
            if (Input.touchCount > 0)
            {
                if (!raycastOneTime)
                {
                    //Camera myCamera = Camera.main;
                    Vector3 touchPos = new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, myCamera.farClipPlane);
                    Vector3 touchPosInWorld = myCamera.ScreenToWorldPoint(touchPos);
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

    //Move to settings
    public void launchSettings()
    {
        if (dataholder.levelStarted)
        {
            //Launch Settings Scene
            dataholder.updateLastScene();
            SceneManager.LoadScene("Option");
        }

    }

    //Move to News Paper
    public void launchNewsPaper()
    {
        if (dataholder.levelStarted)
        {
            dataholder.updateLastScene();
            SceneManager.LoadScene(newsPaperScene);
        }
    }

    public void LaunchNoteBook()
    {
        if (dataholder.levelStarted)
        {
            dataholder.updateLastScene();
            SceneManager.LoadScene("NoteBook");
        }
    }
    //Destroy Police report
    public void destroyPolicereport()
    {
        dataholder.levelStarted = true;
        Destroy(policeReport);
    }
}
