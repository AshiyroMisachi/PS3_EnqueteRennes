using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionNoteBook : MonoBehaviour
{
    //Reference
    public DataHolder dataHolder;
    public GameObject proofShelf;

    //Prefab
    public GameObject proofNote;

    public GameObject feedBackImage;

    //Var NoteBook
    public GameObject[] proofNotes;
    public TextMeshProUGUI proofName;
    public TextMeshProUGUI proofDescription;
    public GameObject gameObjectRender;
    public Vector3 baseScaleRender;

    //Var Inspection
    //If true = notebook, false = inspecitonMode
    private bool currentMode = true; 
    public GameObject noteBookCanvas, inspectionCanvas, backButton;
    public TextMeshProUGUI insName, insDesc;
    public float insSlideSpeed, scaleSpeed;

    public AudioSource feedbackEnterNotebook, feedbackSwitchPage;

    private void Start()
    {
        dataHolder = FindObjectOfType<DataHolder>();
        dataHolder.UpdateNoteBookScene();
        proofNotes = new GameObject[dataHolder.proofsLevel.Length];
        inspectionCanvas.SetActive(false);

        //Spawn Proof Note
        for (int i = 0; i < dataHolder.proofsLevel.Length; i++)
        {
            if (dataHolder.proofsLevel[i])
            {
                Debug.Log("New Proof");
                GameObject newProofNote = Instantiate(proofNote);
                newProofNote.GetComponent<RectTransform>().SetParent(proofShelf.GetComponent<RectTransform>(), false);
                string[] nameList = (string[])dataHolder.proofsName[i];
                newProofNote.GetComponent<ProofNote>().myName = nameList[(int)dataHolder.language];
                newProofNote.GetComponent<TextMeshProUGUI>().text = nameList[(int)dataHolder.language];
                string[] descList = (string[])dataHolder.proofsDescription[i];
                newProofNote.GetComponent<ProofNote>().myDescription = descList[(int)dataHolder.language];
                newProofNote.GetComponent<ProofNote>().myGameObject = dataHolder.proofsGameObject[i];
                newProofNote.GetComponent<ProofNote>().myScale = dataHolder.proofsScaleRender[i];
                newProofNote.GetComponent<ProofNote>().myRotation = dataHolder.proofsRotationRender[i];
                proofNotes[i] = newProofNote;
            }
        }

        if (!Application.isEditor)
        {
            insSlideSpeed *= 16;
            scaleSpeed *= 6;
        }

        if (dataHolder.lastScene == dataHolder.levelCrimeScene)
        {
            feedbackEnterNotebook.Play();
        }
        else if (dataHolder.lastScene == dataHolder.levelNewsPaper)
        {
            feedbackSwitchPage.Play();
        }
    }

    private void Update()
    {
        if (!currentMode)
        {
            //Move Object during Inspection
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                Debug.Log(touch.deltaPosition.y);
                //Slide Right
                if (touch.deltaPosition.x > 10)
                {
                    gameObjectRender.transform.localPosition += Vector3.right * insSlideSpeed;
                }
                //Slide Left
                else if (touch.deltaPosition.x < -10)
                {
                    gameObjectRender.transform.localPosition += Vector3.left * insSlideSpeed;

                }

                if (touch.deltaPosition.y > 10)
                {
                    gameObjectRender.transform.localPosition += Vector3.up * insSlideSpeed;
                }
                else if (touch.deltaPosition.y < -10)
                {
                    gameObjectRender.transform.localPosition += Vector3.down * insSlideSpeed;
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

                ZoomIns(difference);
            }
        }
    }

    public void SwitchRender(GameObject newRender, Vector3 scaleNewRender, Vector3 rotationNewRender)
    {
        if (gameObjectRender != null && newRender != null)
        {
            Destroy(gameObjectRender);
            gameObjectRender = Instantiate(newRender, new Vector3(0, 0, 0), Quaternion.identity);
            gameObjectRender.transform.localScale = scaleNewRender;
            gameObjectRender.transform.eulerAngles = rotationNewRender;
        }
        else if (newRender != null)
        {
            gameObjectRender = Instantiate(newRender, new Vector3(0, 0, 0), Quaternion.identity);
            gameObjectRender.transform.localScale = scaleNewRender;
            gameObjectRender.transform.eulerAngles = rotationNewRender;
        }
    }

    public void InspectObject()
    {
        if (currentMode && gameObjectRender != null)
        {
            //Go Inspection Mode
            currentMode = false;
            noteBookCanvas.SetActive(false);
            backButton.SetActive(false);
            inspectionCanvas.SetActive(true);
            //insName.text = proofName.text;
            //insDesc.text = proofDescription.text;
        }
        else if (gameObjectRender != null)
        {
            //Go Notebook
            currentMode = true;
            noteBookCanvas.SetActive(true);
            backButton.SetActive(true);
            inspectionCanvas.SetActive(false);
            gameObjectRender.transform.localScale = baseScaleRender;
            gameObjectRender.transform.position = new Vector3 (0, 0, 0);
        }
    }

    public void ZoomIns(float differencePinching)
    {
        // Dezoom
        if (differencePinching < 0 && Vector3.Distance(gameObjectRender.transform.localScale, baseScaleRender) >= 50)
        {
            gameObjectRender.transform.localScale -= baseScaleRender * scaleSpeed;
        }

        // Zoom
        if (differencePinching > 0 && Vector3.Distance(gameObjectRender.transform.localScale, baseScaleRender) <= 1500)
        {
            gameObjectRender.transform.localScale += baseScaleRender * scaleSpeed;
        }
    }

    public void GoNewsPaper()
    {
        dataHolder.UpdateLastScene();
        SceneManager.LoadScene(dataHolder.levelNewsPaper);
    }
    public void GoBackScene()
    {
        dataHolder.UpdateLastScene();
        SceneManager.LoadScene(dataHolder.levelCrimeScene);
    }
}
