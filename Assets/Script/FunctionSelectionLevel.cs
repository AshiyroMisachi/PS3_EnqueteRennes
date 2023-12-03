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
    public TextMeshProUGUI levelName;
    public TextMeshProUGUI levelDescription;
    public Image levelScore;
    public string levelSelected;

    //Make Launch Button Work
    public bool noSelection;
    private float timerDeselection;
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
        if (levels[0].isSelected)
        {
            SetupLevelDisplay(0);
        }
        else
        {
                timerDeselection -= Time.deltaTime;
        }

        if (timerDeselection < 0f)
        {
            levelDisplay.anchoredPosition = new Vector3(500f, 0f, 0f);
        }
    }

    public void SetupLevelDisplay(int numberLevel)
    {
        levelName.text = levels[numberLevel].GetComponent<Level>().myName;
        levelDescription.text = levels[numberLevel].GetComponent<Level>().myDescription;
        levelScore.fillAmount = levels[numberLevel].GetComponent<Level>().myScore;
        levelDisplay.anchoredPosition = new Vector3(315f, 0f, 0f);
        levelSelected = levels[numberLevel].GetComponent<Level>().myScene;
        timerDeselection = timerDeselectionTime;

        dataHolder.levelSelectedNumber = levels[numberLevel].GetComponent<Level>().myNumber;
    }
    public void launchSettings()
    {
        //Launch Settings Scene
        SceneManager.LoadScene("Option");
    }

    public void launchLevel()
    {
        Debug.Log("Launch");
        SceneManager.LoadScene(levelSelected);
    }
}
