using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextCase : MonoBehaviour
{
    //DataHolder
    public DataHolder dataHolder;
    public FunctionHolderNewsPaper manager;

    //Var 
    public int myNumber;
    public string currentName;
    public string[] answers;
    public string correction;
    public TextMeshProUGUI myText;

    void Start()
    {
        //Find 
        dataHolder = FindObjectOfType<DataHolder>();
        manager = FindObjectOfType<FunctionHolderNewsPaper>();

        //Setup
        if (dataHolder.actualAnswers[myNumber] != null)
        {
            changeWord(dataHolder.actualAnswers[myNumber]);
        }
        else
        {
            changeWord("");
        }
    }

    public void BeSelected()
    {
        if (manager.currentSelected != null)
        {
            manager.currentSelected.GetComponent<Image>().color = new Color(1f,1f,1f,0.25f);
        }
        manager.currentSelected = gameObject.GetComponent<TextCase>();
        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

        manager.pannelWordPositionCount = 0;
        for (int i = 0; i < manager.pannelListWord.Count; i++)
        {
            manager.pannelListWord[i].GetComponent<TextProof>().gotMove = false;
        }

        for (int i = 0; i < answers.Length; i++)
        {
            for (int j = 0; j < manager.pannelListWord.Count; j++)
            {
                if (manager.pannelListWord[j].GetComponent<TextProof>().myName == answers[i])
                {
                    manager.pannelListWord[j].GetComponent<RectTransform>().localPosition = manager.pannelWordPosition[manager.pannelWordPositionCount];
                    manager.pannelListWord[j].GetComponent<TextProof>().gotMove = true;
                    manager.pannelWordPositionCount++;
                }
                else if (!manager.pannelListWord[j].GetComponent<TextProof>().gotMove)
                {
                    manager.pannelListWord[j].GetComponent<RectTransform>().localPosition = new Vector3(-4800f, -1.5f, 0f);
                }
            }
        }
    }

    public void changeWord(string word)
    {
        dataHolder.actualAnswers[myNumber] = word;
        myText.text = word;
        currentName = word;
    }
}