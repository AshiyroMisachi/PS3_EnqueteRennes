using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionHolderNoteBook : MonoBehaviour
{
    //Reference
    public DataHolder dataHolder;
    public GameObject proofShelf;

    //Prefab
    public GameObject proofNote;

    //Var
    public GameObject[] proofNotes;
    public TextMeshProUGUI proofName;
    public TextMeshProUGUI proofDescription;
    public GameObject gameObjectRender;

    private void Start()
    {
        dataHolder = FindObjectOfType<DataHolder>();
        proofNotes = new GameObject[dataHolder.proofsLevel.Length];

        //Spawn Proof Note
        for (int i = 0; i < dataHolder.proofsLevel.Length; i++)
        {
            if (dataHolder.proofsLevel[i])
            {
                GameObject newProofNote = Instantiate(proofNote);
                newProofNote.GetComponent<RectTransform>().SetParent(proofShelf.GetComponent<RectTransform>(), false);

                newProofNote.GetComponent<ProofNote>().myName = dataHolder.proofsName[i];
                newProofNote.GetComponent<TextMeshProUGUI>().text = dataHolder.proofsName[i];
                newProofNote.GetComponent<ProofNote>().myDescription = dataHolder.proofsDescription[i];
                newProofNote.GetComponent<ProofNote>().myGameObject = dataHolder.proofsGameObject[i];
                newProofNote.GetComponent<ProofNote>().myScale = dataHolder.proofsScaleRender[i];
                newProofNote.GetComponent<ProofNote>().myRotation = dataHolder.proofsRotationRender[i];
                proofNotes[i] = newProofNote;
            }
        }

        //proofNotes[0].GetComponent<ProofNote>().ShowProof();   
    }

    public void switchRender(GameObject newRender, Vector3 scaleNewRender, Vector3 rotationNewRender)
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


    public void goBackScene()
    {
        SceneManager.LoadScene(dataHolder.lastScene);
    }
}
