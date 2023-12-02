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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeSelected()
    {
        manager.currentSelected = gameObject.GetComponent<TextCase>();
        manager.pannelWordPositionCount = 0;
        for (int i = 0; i < answers.Length; i++)
        {
            for (int j = 0; j < manager.pannelListWord.Count; j++)
            {
                if (manager.pannelListWord[i].GetComponent<TextProof>().myName == answers[i])
                {
                    manager.pannelListWord[i].GetComponent<RectTransform>().localPosition = manager.pannelWordPosition[manager.pannelWordPositionCount];
                    manager.pannelWordPositionCount++;
                }
                else
                {
                    manager.pannelListWord[i].GetComponent<RectTransform>().localPosition = new Vector3(-650f, -1.5f, 0f);
                }
            }
        }
    }
}
