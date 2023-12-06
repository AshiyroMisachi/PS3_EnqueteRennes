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
                proofNotes[i] = newProofNote;
            }
        }

        //proofNotes[0].GetComponent<ProofNote>().ShowProof();   
    }

    public void switchRender(GameObject newRender, Vector3 scaleNewRender)
    {
        if (gameObjectRender != null)
        {
            Destroy(gameObjectRender);
            gameObjectRender = Instantiate(newRender, new Vector3(0, 0, 10), Quaternion.identity);
            gameObjectRender.transform.localScale = scaleNewRender;
        }
    }


    public void goBackScene()
    {
        SceneManager.LoadScene(dataHolder.lastScene);
    }
}
