using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        manager.currentSelected = gameObject.GetComponent<TextCase>();
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
                    manager.pannelListWord[j].GetComponent<RectTransform>().localPosition = new Vector3(-650f, -1.5f, 0f);
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