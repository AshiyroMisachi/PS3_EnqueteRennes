using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    //Var of Level
    public string myName;
    public string myDescription;
    public string myMoreInfo;
    public string myScene;
    public string myNewsPaper;
    public int myNumber;
    public float myScore;
    public int myScoreProof;
    public int maxProof;
    public Sprite myImage;

    public RectTransform myTransform;

    public bool unlock;
    public GameObject objectLock;
    public Canvas canvas;
    //Get DataHolder
    public DataHolder dataHolder;

    public Sprite noSelected, selected, notUnlock;
    void Start()
    {
        //Get Transform
        myTransform = GetComponent<RectTransform>();

        //Get DataHolder 
        dataHolder = FindObjectOfType<DataHolder>();

        //GetScore
        myScore = dataHolder.scoreArray[myNumber];
        myScoreProof = dataHolder.scoreProofArray[myNumber];

        //Get Canvas to spawn Lock on Canvas
        canvas = FindObjectOfType<Canvas>();

        //is Unlock
        if (!unlock)
        {
            gameObject.GetComponent<Image>().sprite = notUnlock;
        }
    }

    public void Update()
    {
        //is Unlock
        if (!unlock)
        {
            gameObject.GetComponent<Image>().sprite = notUnlock;
        }
        

        if (gameObject.GetComponent<ButtonButBetter>().isSelected && unlock)
        {
            gameObject.GetComponent<Image>().sprite = selected;
        }
        else if (unlock)
        {
            gameObject.GetComponent<Image>().sprite = noSelected;
        }
    }
}
