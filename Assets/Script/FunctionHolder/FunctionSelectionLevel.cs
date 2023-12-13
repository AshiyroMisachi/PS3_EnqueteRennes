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
    public Image levelImage;
    public TextMeshProUGUI levelScoreProof;
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
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i].isSelected)
            {
                SetupLevelDisplay(i);
            }
        }

        timerDeselection -= Time.deltaTime;
        if (timerDeselection < 0f)
        {
            levelDisplay.anchoredPosition = new Vector3(900f, 0f, 0f);
        }
    }

    public void SetupLevelDisplay(int numberLevel)
    {
        levelName.text = levels[numberLevel].GetComponent<Level>().myName;
        levelDescription.text = levels[numberLevel].GetComponent<Level>().myDescription;
        levelImage.sprite = levels[numberLevel].GetComponent<Level>().myImage;
        levelScore.fillAmount = levels[numberLevel].GetComponent<Level>().myScore;
        levelScoreProof.text = levels[numberLevel].GetComponent<Level>().myScoreProof + "/" + levels[numberLevel].GetComponent<Level>().maxProof;
        levelDisplay.anchoredPosition = new Vector3(-275f, 0f, 0f);
        levelSelected = levels[numberLevel].GetComponent<Level>().myScene;
        timerDeselection = timerDeselectionTime;

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
        Debug.Log("Launch");
        SceneManager.LoadScene(levelSelected);
    }
}
