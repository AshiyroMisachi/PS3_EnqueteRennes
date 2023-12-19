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

    private void Start()
    {
        //Find dataHolder
        dataHolder = FindObjectOfType<DataHolder>();

        //Reset Dataholder info
        dataHolder.resetLevelVAR();

        if (!Application.isEditor)
        {
            levelDisplayMultiplier /= 9;
        }

        blackImage.GetComponent<Animator>().SetTrigger("FadeOut");
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
        levelName.text = levels[numberLevel].GetComponent<Level>().myName;
        levelDescription.text = levels[numberLevel].GetComponent<Level>().myDescription;
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
        SceneManager.LoadScene("Option");
    }

    public void launchLevel()
    {
        StartCoroutine(LaunchScene(levelSelected));
    }

    public IEnumerator LaunchScene(string sceneName)
    {
        blackImage.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitUntil(() => blackImage.GetComponent<Image>().color.a == 1);
        SceneManager.LoadScene(sceneName);
    }
}
