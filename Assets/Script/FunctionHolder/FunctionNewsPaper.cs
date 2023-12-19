using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FunctionHolderNewsPaper : MonoBehaviour
{
    public DataHolder dataHolder;
    public GameObject textproof;

    public TextCase[] textCases;
    public TextCase currentSelected;
    public string wordSelected;
    public GameObject pannelWord;
    public string[] wordList;
    public List<GameObject> pannelListWord;
    public Vector3[] pannelWordPosition;
    public int pannelWordPositionCount;

    public GameObject validateButton;
    public TextMeshProUGUI validateText;
    private bool canConfirm = false;

    //Warning Tab
    private bool inWarning = false;
    public GameObject warningTry;
    public TextMeshProUGUI warningTryText;
    public GameObject warningMistake;
    public TextMeshProUGUI warningMistakeText;

    public GameObject blackImage;

    private void Start()
    {
        //Find dataholder
        dataHolder = FindObjectOfType<DataHolder>();
        dataHolder.UpdateNoteBookScene();

        //Setup
        warningTry.SetActive(false);
        warningMistake.SetActive(false);
        blackImage.GetComponent<Animator>().SetTrigger("Clear");

        //Spawn TextProofs
        for (int j = 0; j < wordList.Length; j++)
        {
            GameObject newTextProof = Instantiate(textproof);
            newTextProof.gameObject.GetComponent<TextProof>().myName = wordList[j];
            newTextProof.GetComponent<RectTransform>().SetParent(pannelWord.GetComponent<RectTransform>(), false);
            newTextProof.GetComponent<RectTransform>().localPosition = new Vector3(-4800f, -1.5f, 0f);

            pannelListWord.Add(newTextProof);
        }

    }

    public void ApplyWord()
    {
        if (wordSelected != null)
        {
            currentSelected.changeWord(wordSelected);
            wordSelected = null;
        }

        var check = 0;
        for (int i = 0; i < textCases.Length; i++)
        {
            if (textCases[i].currentName != "")
            {
                check++;
            }
        }

        if (check == 6)
        { 
            validateButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            validateText.color = new Color(0f, 0f, 0f, 1f);
            canConfirm = true;
        }
    }

    public void GoNoteBook()
    {
        if (!inWarning)
        {
            SceneManager.LoadScene("NoteBook");
        }
    }

    public void goBackScene()
    {
        if (!inWarning)
        {
            SceneManager.LoadScene(dataHolder.lastScene);
        }
    }

    public void OpenWarningTry()
    {
        if (!inWarning && canConfirm)
        {
            inWarning = true;
            //CHECK MISTAKE
            dataHolder.mistake = 0;
            for (int i = 0; i < textCases.Length; i++)
            {
                if (textCases[i].correction != textCases[i].currentName)
                {
                    dataHolder.mistake++;
                }
            }
            if (dataHolder.mistake == 0 || dataHolder.difficulty || dataHolder.numberTry == 0)
            {
                continueVerification();
            }
            else
            {
                warningTry.SetActive(true);
                warningTryText.text = "Vous avez " + dataHolder.numberTry + " tentatives restantes";
            }
        }
    }

    public void CloseWarningTry()
    {
        inWarning = false;
        warningTry.SetActive(false);
    }

    public void OpenWarningMistake()
    {
        warningTry.SetActive(false);
        warningMistake.SetActive(true);
        warningMistakeText.text = "Vous avez " + dataHolder.mistake + " fautes";
    }

    public void CloseWarningMistake()
    {
        inWarning = false;
        dataHolder.numberTry--;
        warningMistake.SetActive(false);
    }

    public void continueVerification()
    {
        //Switch Score scene, save number of mistake
        StartCoroutine(LaunchScene("Scoring"));
    }

    public IEnumerator LaunchScene(string sceneName)
    {
        blackImage.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitUntil(() => blackImage.GetComponent<Image>().color.a == 1);
        SceneManager.LoadScene(sceneName);
    }
}
