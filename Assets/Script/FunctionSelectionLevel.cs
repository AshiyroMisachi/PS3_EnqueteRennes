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
    public TextMeshProUGUI levelScore;
    public string levelSelected;

    //Make Launch Button Work
    public bool noSelection;
    private float timerDeselection;
    private float timerDeselectionTime = 1f;

    //Var Get Level Selected Info
    public ButtonButBetter[] levels;


    void Update()
    {
        //Find dataHolder
        dataHolder = FindObjectOfType<DataHolder>();

        //Reset Dataholder info
        dataHolder.levelStarted = false;
        for (int i = 0; i < dataHolder.proofsLevel.Length; i++)
        {
            dataHolder.proofsLevel[i] = false;
        }
        for (int i = 0; i < dataHolder.proofsName.Length; i++)
        {
            dataHolder.proofsName[i] = "";
        }
        for (int i = 0; i < dataHolder.proofsDescription.Length; i++)
        {
            dataHolder.proofsDescription[i] = "";
        }
        for (int i = 0; i < dataHolder.proofsType.Length; i++)
        {
            dataHolder.proofsType[i] = TypeProof.Null;
        }

        //Show Display
        if (levels[0].isSelected)
        {
            levelName.text = levels[0].GetComponent<Level>().myName;
            levelDescription.text = levels[0].GetComponent<Level>().myDescription;
            levelScore.text = "Score: " + levels[0].GetComponent<Level>().myScore;
            levelDisplay.anchoredPosition = new Vector3(315f, 0f, 0f);
            levelSelected = levels[0].GetComponent<Level>().myScene;
            timerDeselection = timerDeselectionTime;
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
