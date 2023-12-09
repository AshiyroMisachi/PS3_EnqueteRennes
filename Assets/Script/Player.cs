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

    // Var Zoom
    public float zoomOutMin = 60;
    public float zoomOutMax = 10;

    //Var Player
    public bool cameraMode;
    public int numberProof;
    public string newsPaperScene;
    public GameObject policeReport;

    public TextMeshProUGUI popUpText;
    public float timerText;

    //The two different canvas, if currentMode is true it's shearchMode
    public bool currentMode;
    public GameObject shearchMode;
    public GameObject inspectionMode;

    //Var Button Camera Mode 2
    //Left, Right, Up, Down
    public GameObject[] arrows;

    //Var InspectionMode
    public GameObject insProof;
    public TextMeshProUGUI insNameProof;
    public TextMeshProUGUI insDescriptionProof;

    //Var Raycast
    public LayerMask mask;
    public bool raycastOneTime;

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
        if (!dataholder.levelStarted)
        {
            dataholder.levelStarted = true;
            dataholder.setupArray(numberProof);
            dataholder.proofsLevel[0] = true;
            dataholder.proofsName[0] = "Rapport de Police";
            dataholder.proofsDescription[0] = "Ceci est le rapport du meurtre d�livr� par la police";

            //Setup mode inspection Police Report
            currentMode = false;
            insProof = Instantiate(policeReport, transform);
            insProof.transform.localPosition = new Vector3(0, 0, 1);
            insNameProof.text = "Rapport de Police";
            insDescriptionProof.text = "Voil� le rapport qu'on a pu me d�livrer avant que le corps ne soit emport�.";
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
            if (currentMode) //Search Mode
            {
                //Setup
                shearchMode.SetActive(true);
                inspectionMode.SetActive(false);

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
                if (Input.touchCount > 0 && Input.touchCount != 2)
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

                //Raycast when touch screen to detect object
                if (Input.touchCount == 2)
                {
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);

                    // Stock the previous positions of each input
                    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                    // Stock the magnitude (distance) between the previous position and the current position 
                    float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                    float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                    // Check the difference between current and previous magnitude
                    float difference = currentMagnitude - prevMagnitude;

                    zoomCamera(difference);
                }

                //Hide Text after pickup an object
                if (timerText > 2f)
                {
                    popUpText.alpha -= 0.005f;
                }
            }
            else //Inspection Mode
            {
                shearchMode.SetActive(false);
                inspectionMode.SetActive(true);
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
    //Switch Inspection Mode to SearchMode
    public void BackSearchMode()
    {
        currentMode = true;
        Destroy(insProof);
    }

    // Camera zoom, Zoom by pinching with 2 fingers
    public void zoomCamera(float differencePinching)
    {
        float zoomSpeed = 5f;

        // Dezoom
        if (differencePinching < 0 && Camera.main.fieldOfView < 60)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, Camera.main.fieldOfView + 5f, Time.deltaTime * zoomSpeed);
        }

        // Zoom
        if (differencePinching > 0 && Camera.main.fieldOfView > 30)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, Camera.main.fieldOfView - 5f, Time.deltaTime * zoomSpeed);
        }
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
}
