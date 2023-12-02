using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionHolderNewsPaper : MonoBehaviour
{
    public DataHolder dataHolder;
    public GameObject textproof;

    public TextCase[] textCases;
    public TextCase currentSelected;
    public string wordSelected;
    public GameObject pannelWord;
    public List<GameObject> pannelListWord;
    public Vector3[] pannelWordPosition;
    public int pannelWordPositionCount;

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
            string[] currentWordList = (string[])dataHolder.proofsWordList[i];
            if (currentWordList != null)
            {
                dataHolder.proofsCount++;
                for (int j = 0; j < currentWordList.Length; j++)
                {
                    GameObject newTextProof = Instantiate(textproof);
                    newTextProof.gameObject.GetComponent<TextProof>().myName = currentWordList[j];
                    newTextProof.GetComponent<RectTransform>().SetParent(pannelWord.GetComponent<RectTransform>(), false);
                    newTextProof.GetComponent<RectTransform>().localPosition = new Vector3 (-650f, -1.5f, 0f);
                    
                    pannelListWord.Add(newTextProof);
                }
            }
        }
    }


    public void ApplyWord()
    {
        if (wordSelected != null)
        {
            currentSelected.changeWord(wordSelected);
            wordSelected = null;
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

        if (dataHolder.mistake == 0 || dataHolder.numberTry == 0)
        {
            //Launch Score Scene
            SceneManager.LoadScene("Scoring");
        }
        else
        {
            //Open Warning Tab
            warningTab.SetActive(true);
            warningText_Error.text = "Vous avez " + dataHolder.mistake + " fautes, il vous reste " + dataHolder.numberTry + " tentatives";
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
