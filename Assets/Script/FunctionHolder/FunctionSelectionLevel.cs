using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FunctionSelectionLevel : MonoBehaviour
{
    //DataHolder
    public DataHolder dataHolder;

    //Var LevelDisplay
    public RectTransform levelDisplay;
    private float levelDisplayLerp;
    private float levelDisplayMultiplier = 300;
    public TextMeshProUGUI levelName;
    public TextMeshProUGUI levelDescription;
    public Image levelScore;
    public Image levelImage;
    public TextMeshProUGUI levelScoreProof;
    public string levelSelected;

    //Make Launch Button Work
    public bool noSelection;
    private float timerDeselection;
    private bool deselection;
    private float timerDeselectionTime = 0.5f;

    //Var Get Level Selected Info
    public ButtonButBetter[] levels;

    public GameObject blackImage;
    public GameObject musicCrimeScene;
    public TextMeshProUGUI startButton;

    private void Start()
    {
        //Find dataHolder
        dataHolder = FindObjectOfType<DataHolder>();

        //Reset Dataholder info
        //dataHolder.ResetLevelVAR();

        if (!Application.isEditor)
        {
            levelDisplayMultiplier /= 15f;
        }

        if (dataHolder.lastScene == "MainScreen")
        {
            blackImage.GetComponent<Animator>().SetTrigger("FadeOut");
        }
        else
        {
            blackImage.GetComponent<Animator>().SetTrigger("Clear");
        }
    }
    void Update()
    {
        //Show Display
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i].isSelected && levels[i].gameObject.GetComponent<Level>().unlock)
            {
                if (i != 0)
                {
                    SetupLevelDisplay(i);
                    if (FindObjectOfType<SaveManager>().currentSave.lastLevel[(int)dataHolder.language] == dataHolder.levelName[(int)dataHolder.language])
                    {
                        //Change Start Button to Continue
                        switch (dataHolder.language)
                        {
                            case Language.Français:
                                startButton.text = "Continuer";
                                break;
                            case Language.English:
                                startButton.text = "Continue";
                                break;
                        }
                    }
                }
                if (deselection)
                {
                    levelDisplayLerp = 0f;
                }
                levelDisplay.anchoredPosition = Vector3.Lerp(levelDisplay.anchoredPosition, new Vector3(-725f, 0f, 0f), levelDisplayLerp);
                levelDisplayLerp += Time.deltaTime / levelDisplayMultiplier;
                levelDisplayLerp = (levelDisplayLerp > 1f) ? 1f : levelDisplayLerp;
                timerDeselection = timerDeselectionTime;
                deselection = false;
            }

        }

        if (timerDeselection < 0)
        {
            if (!deselection)
            {
                levelDisplayLerp = 0f;
            }

            levelDisplay.anchoredPosition = Vector3.Lerp(levelDisplay.anchoredPosition, new Vector3(-2000f, 0f, 0f), levelDisplayLerp);
            levelDisplayLerp += Time.deltaTime / levelDisplayMultiplier;
            levelDisplayLerp = (levelDisplayLerp < 0f) ? 0f : levelDisplayLerp;
            deselection = true;
        }
        timerDeselection -= Time.deltaTime;
    }

    public void SetupLevelDisplay(int numberLevel)
    {
        levelName.text = levels[numberLevel].GetComponent<Level>().myName[(int)dataHolder.language];
        levelDescription.text = levels[numberLevel].GetComponent<Level>().myDescription[(int)dataHolder.language];
        levelImage.sprite = levels[numberLevel].GetComponent<Level>().myImage;
        levelScore.fillAmount = levels[numberLevel].GetComponent<Level>().myScore;
        levelScoreProof.text = levels[numberLevel].GetComponent<Level>().myScoreProof + "/" + levels[numberLevel].GetComponent<Level>().maxProof;
        levelSelected = levels[numberLevel].GetComponent<Level>().myScene;

        dataHolder.levelName = levels[numberLevel].GetComponent<Level>().myName;
        dataHolder.levelMoreInfo = levels[numberLevel].GetComponent<Level>().myMoreInfo;
        dataHolder.levelSelectedNumber = levels[numberLevel].GetComponent<Level>().myNumber;
        dataHolder.levelCrimeScene = levels[numberLevel].GetComponent<Level>().myScene;
        dataHolder.levelNewsPaper = levels[numberLevel].GetComponent<Level>().myNewsPaper;
    }
    public void launchSettings()
    {
        //Launch Settings Scene
        dataHolder.UpdateLastScene();
        SceneManager.LoadScene("Option");
    }

    public void launchLevel()
    {
        if (FindObjectOfType<SaveManager>().currentSave.lastLevel[(int)dataHolder.language] != dataHolder.levelName[(int)dataHolder.language])
        {
            dataHolder.ResetLevelVAR();
        }
        else
        {
            //Change Start Button to Continue
            switch (dataHolder.language)
            {
                case Language.Français:
                    startButton.text = "Continuer";
                    break;
                case Language.English:
                    startButton.text = "Continue";
                    break;
            }
        }

        dataHolder.levelLastNotebook = "NoteBook";
        StartCoroutine(LaunchScene(levelSelected));
    }

    public IEnumerator LaunchScene(string sceneName)
    {
        blackImage.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitUntil(() => blackImage.GetComponent<Image>().color.a == 1);
        Instantiate(musicCrimeScene, gameObject.transform.position, Quaternion.identity);
        SceneManager.LoadScene(sceneName);
    }
}
