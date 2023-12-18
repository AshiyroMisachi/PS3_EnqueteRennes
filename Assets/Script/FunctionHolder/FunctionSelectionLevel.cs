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
    public float levelDisplayLerp;
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

    private void Start()
    {
        //Find dataHolder
        dataHolder = FindObjectOfType<DataHolder>();

        //Reset Dataholder info
        dataHolder.resetLevelVAR();
    }
    void Update()
    {
        //Show Display
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i].isSelected)
            {
                if (i != 0)
                {
                    SetupLevelDisplay(i);
                }
                levelDisplay.anchoredPosition = Vector3.Lerp(levelDisplay.anchoredPosition, new Vector3(-725f, 0f, 0f), levelDisplayLerp);
                levelDisplayLerp += Time.deltaTime / 500;
                levelDisplayLerp = (levelDisplayLerp > 1f) ? 1f : levelDisplayLerp;
                timerDeselection = timerDeselectionTime;
                deselection = false;
            }

        }

        
        if (timerDeselection < 0)
        {
            levelDisplay.anchoredPosition = Vector3.Lerp(levelDisplay.anchoredPosition, new Vector3(-1600f, 0f, 0f), levelDisplayLerp);
            levelDisplayLerp -= Time.deltaTime / 500;
            levelDisplayLerp = (levelDisplayLerp < 0f) ? 0f : levelDisplayLerp;
        }
        timerDeselection -= Time.deltaTime;
        deselection = true;
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
        dataHolder.levelSelectedNumber = levels[numberLevel].GetComponent<Level>().myNumber;
        dataHolder.levelNewsPaper = levels[numberLevel].GetComponent<Level>().myNewsPaper;
    }
    public void launchSettings()
    {
        //Launch Settings Scene
        SceneManager.LoadScene("Option");
    }

    public void launchLevel()
    {
        SceneManager.LoadScene(levelSelected);
    }
}
