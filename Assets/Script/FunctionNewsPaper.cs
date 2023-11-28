using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionHolderNewsPaper : MonoBehaviour
{
    public DataHolder dataHolder;

    public GameObject textproof;

    //Verification
    public TextCase[] textCases;

    //Warning Tab
    public GameObject warningTab;
    public TextMeshProUGUI warningText_Error;

    private void Start()
    {
        //Find dataholder
        dataHolder = FindObjectOfType<DataHolder>();
        
        //Setup
        warningTab.SetActive(false);

        //Spawn TextProofs
        dataHolder.proofsCount = 0;
        for (int i = 0; i < dataHolder.proofsLevel.Length; i++)
        {
            if (dataHolder.proofsLevel[i])
            {
                GameObject newObject = Instantiate(textproof, dataHolder.textProofsCoords[i], transform.rotation);
                TextProof newTextProof = newObject.gameObject.GetComponent<TextProof>();
                newTextProof.myName = dataHolder.proofsName[i];
                newTextProof.myNumber = i;
                dataHolder.proofsCount++;
            }
        }
    }

    public void goBackScene()
    {
        SceneManager.LoadScene(dataHolder.lastScene);
    }

    public void verification()
    {
        for (int i = 0; i < textCases.Length; i++)
        {
            if (textCases[i].currentName != textCases[i].correction)
            {
                dataHolder.mistake++;
            }
        }

        if (dataHolder.mistake == 0)
        {
            //Launch Score Scene
            SceneManager.LoadScene("Scoring");
        }
        else
        {
            //Open Warning Tab
            warningTab.SetActive(true);
            warningText_Error.text = "Vous avez " + dataHolder.mistake + " fautes, il vous reste " + dataHolder.numberTry + " tentatives";

            //if try = 0 and mistake > 0, launch Score Scene
        }
    }

    public void closeVerification()
    {
        dataHolder.mistake = 0;
        dataHolder.numberTry--;
        warningTab.SetActive(false);
    }

    public void continueVerification()
    {
        //Switch Score scene, save number of mistake
        SceneManager.LoadScene("Scoring");
    }
}
