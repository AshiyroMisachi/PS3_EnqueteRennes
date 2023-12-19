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

    public AudioSource feedbackTextCase, feedbackEnterNotebook, feedbackSwitchpage;
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

        if (dataHolder.lastScene == dataHolder.levelCrimeScene)
        {
            feedbackEnterNotebook.Play();
        }
        else if (dataHolder.lastScene == "NoteBook")
        {
            feedbackSwitchpage.Play();
        }
    }

    private void Update()
    {
        UnlockValidateButton();
    }

    public void ApplyWord()
    {
        feedbackTextCase.Play();
        if (wordSelected != null)
        {
            currentSelected.changeWord(wordSelected);
            wordSelected = null;
        }

        UnlockValidateButton();
    }

    public void GoNoteBook()
    {
        if (!inWarning)
        {
            dataHolder.UpdateLastScene();
            SceneManager.LoadScene("NoteBook");
        }
    }

    public void GoBackScene()
    {
        if (!inWarning)
        {
            dataHolder.UpdateLastScene();
            SceneManager.LoadScene(dataHolder.levelCrimeScene);
        }
    }

    public void UnlockValidateButton()
    {
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
                ContinueVerification();
            }
            else
            {
                warningTry.SetActive(true);
                switch (dataHolder.language)
                {
                    case Language.Français:
                        warningTryText.text = "Il vous restera " + dataHolder.numberTry + " tentatives.";
                        break;
                    case Language.English:
                        warningTryText.text = "There will be " + dataHolder.numberTry + " tries left.";
                        break;
                }
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

        switch (dataHolder.language)
        {
            case Language.Français:
                warningMistakeText.text = "Vous avez " + dataHolder.mistake + " erreurs.";
                break;
            case Language.English:
                warningMistakeText.text = "You made " + dataHolder.mistake + " mistakes.";
                break;
        }
    }

    public void CloseWarningMistake()
    {
        inWarning = false;
        dataHolder.numberTry--;
        warningMistake.SetActive(false);
    }

    public void ContinueVerification()
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
