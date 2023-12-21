using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Data Holder
    public DataHolder dataholder;
    public TouchManager touchManager;

    public float lerpFactor;
    private Vector3 lastAcceleromter;

    // Var Zoom
    public float zoomOutMin = 60;
    public float zoomOutMax = 10;

    //Var Player
    public bool cameraMode;
    public int numberProof;
    public GameObject policeReport;
    public string[] policeReportName;
    public string[] policeReportDescription;
    public string[] feedBackProofFound;

    public TextMeshProUGUI popUpText;
    public float timerText;

    //The two different canvas, if currentMode is true it's shearchMode
    public bool currentMode;
    public GameObject shearchMode;
    public GameObject inspectionMode;

    //Var Button Camera Mode 2
    //Left, Right, Up, Down
    public GameObject[] arrows;
    public float speedArrows;
    public float angleX;

    //Var InspectionMode
    public GameObject insProof;
    public TextMeshProUGUI insNameProof;
    public TextMeshProUGUI insDescriptionProof;
    public float insSlideSpeed;

    //Var Raycast
    public LayerMask mask;
    public bool raycastOneTime;

    public Light clueLight;
    public GameObject clueLigtUI;
    private bool vibrate = false;

    public GameObject blackImage;
    public GameObject musicCrimeScene;
    public AudioSource feedBackProof;

    void Start()
    {
        //Find dataholder
        dataholder = FindObjectOfType<DataHolder>();
        touchManager = FindObjectOfType<TouchManager>();

        //Enable the gyroscope
        Input.gyro.enabled = true;

        //Initialize parameters
        cameraMode = dataholder.cameraMode;

        //Parameters Switch Scene
        if (!dataholder.levelStarted)
        {
            Instantiate(musicCrimeScene, gameObject.transform.position, Quaternion.identity);
            dataholder.levelStarted = true;
            dataholder.setupArray(numberProof);
            dataholder.proofsLevel[0] = true;
            dataholder.proofsName[0] = policeReportName;
            dataholder.proofsDescription[0] = policeReportDescription;
            dataholder.proofsGameObject[0] = policeReport;
            dataholder.proofsScaleRender[0] = new Vector3(187.210007f, 264.768372f, 1782.95203f);
            dataholder.proofsRotationRender[0] = new Vector3(0, 180, 0);

            //Setup mode inspection Police Report
            currentMode = false;
            insProof = Instantiate(policeReport, transform);
            insProof.transform.localPosition = new Vector3(0, 0.06f, 1);
            insNameProof.text = policeReportName[(int)dataholder.language];
            insDescriptionProof.text = policeReportDescription[(int)dataholder.language];
            blackImage.GetComponent<Animator>().SetTrigger("FadeOut");
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
        gameObject.transform.eulerAngles = dataholder.cameraRotation;

        if (!Application.isEditor)
        {
            speedArrows = 1.5f;
            insSlideSpeed = insSlideSpeed * 8;
        }

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
            if (cameraMode && Vector3.Distance(Input.gyro.attitude.eulerAngles, lastAcceleromter) > 0.35f)
            {
                transform.rotation = Input.gyro.attitude;
                transform.Rotate(0f, 0f, 180f, Space.Self);
                transform.Rotate(90f, 180f, 0f, Space.World);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
            }
            lastAcceleromter = Input.gyro.attitude.eulerAngles;

            Camera myCamera = Camera.main;

            //Light
            if (Physics.Raycast(myCamera.transform.position, myCamera.transform.forward * 500, out var infoBis, 500, mask) && !dataholder.difficulty)
            {
                Proof proofDetected = infoBis.transform.GetComponent<Proof>();
                if (proofDetected != null && proofDetected.GetCanPickUp())
                {
                    if (vibrate)
                    {
                        vibrate = false;
                        Handheld.Vibrate();
                    }
                    clueLight.intensity = 1;
                    clueLigtUI.SetActive(true);

                    // Light appears at mid distance between the origin of the player and the origin of the proof
                    Vector3 lightPosition = gameObject.transform.position - ((gameObject.transform.position - proofDetected.transform.position) / 2);
                    clueLight.transform.position = lightPosition;
                }
            }
            else
            {
                clueLight.intensity = 0;
                clueLigtUI.SetActive(false);
                vibrate = true;
            }
            /*
            //Raycast when touch screen to detect object
            if (Input.touchCount > 0 && Input.touchCount != 2)
            {
                if (!raycastOneTime)
                {
                    //Camera myCamera = Camera.main;
                    Vector3 touchPos = new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, myCamera.farClipPlane);
                    Vector3 touchPosInWorld = myCamera.ScreenToWorldPoint(touchPos);
                    if (!MouseOverUILayerObject.IsPointerOverUIObject())
                    {
                        if (Physics.Raycast(myCamera.transform.position, touchPosInWorld - myCamera.transform.position, out var info, 500, mask))
                        {
                            Proof touchProof = info.transform.GetComponent<Proof>();
                            if (touchProof != null)
                            {
                                touchProof.getPickUp(this);
                            }

                        }
                    }
                }
                raycastOneTime = true;
            }
            else
            {
                raycastOneTime = false;
            }
            */

            //Hide Text after pickup an object
            if (timerText > 2f)
            {
                    popUpText.alpha -= 0.015f;
            }
        }
        else //Inspection Mode
        {
            shearchMode.SetActive(false);
            inspectionMode.SetActive(true);

            //Move Object during Inspection
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                //Slide Right
                if (touch.deltaPosition.x > 10)
                {
                    insProof.transform.localPosition += Vector3.right * insSlideSpeed;
                }
                //Slide Left
                else if (touch.deltaPosition.x < -10)
                {
                    insProof.transform.localPosition += Vector3.left * insSlideSpeed;

                }

                if (touch.deltaPosition.y > 10)
                {
                    insProof.transform.localPosition += Vector3.up * insSlideSpeed;
                }
                else if (touch.deltaPosition.y < -10)
                {
                    insProof.transform.localPosition += Vector3.down * insSlideSpeed;
                }
            }
        }

        //Zoom
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

        //RotationArrows
        angleX = transform.localEulerAngles.x;
        //if()?{} : else{}
        angleX = (angleX > 180) ? angleX - 360 : angleX;
        dataholder.cameraRotation = gameObject.transform.eulerAngles;
    }

    //Camera Mode 2, Move by clicking button
    public void rotateDown()
    {
        if (angleX <= 80)
        {
            transform.eulerAngles += Vector3.right * speedArrows;
        }
    }

    public void rotateUp()
    {
        if (angleX >= -80)
        {
            transform.eulerAngles += Vector3.left * speedArrows;
        }
    }

    public void rotateLeft()
    {
        transform.eulerAngles += Vector3.down * speedArrows;
    }

    public void rotateRight()
    {
        transform.eulerAngles += Vector3.up * speedArrows;
    }

    //Switch Inspection Mode to SearchMode
    public void BackSearchMode()
    {
        currentMode = true;
        Destroy(insProof);
        Camera.main.fieldOfView = 60;
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
    public void LaunchSettings()
    {
        if (dataholder.levelStarted)
        {
            //Launch Settings Scene
            dataholder.UpdateLastScene();
            SceneManager.LoadScene("Option");
        }

    }

    //Move to News Paper
    public void LaunchNewsPaper()
    {
        if (dataholder.levelStarted)
        {
            dataholder.UpdateLastScene();
            SceneManager.LoadScene(dataholder.levelNewsPaper);
        }
    }

    public void LaunchNoteBook()
    {
        if (dataholder.levelStarted)
        {
            dataholder.UpdateLastScene();
            SceneManager.LoadScene(dataholder.levelLastNotebook);
        }
    }

    //TEST
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.touches[0].deltaPosition.x, Input.touches[0].deltaPosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject.layer == 5) //5 = UI layer
            {
                return true;
            }
        }

        return false;
    }

    private bool IsPointerOverUIObject_2()
    {
        var touchPosition = Input.touches[0].deltaPosition;
        var eventData = new PointerEventData(EventSystem.current) { position = touchPosition };
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}
