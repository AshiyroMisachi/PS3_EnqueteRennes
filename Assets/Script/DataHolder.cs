using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataHolder : MonoBehaviour
{
    //Navigation in menu
    public string lastScene;
    public string currentScene;

    //Selection Level
    public float[] scoreArray;
    public int levelSelectedNumber;

    //Navigation in Level
    public bool levelStarted;

    public bool[] proofsLevel;
    public string[] proofsName;
    public string[] proofsDescription;
    public GameObject[] proofsGameObject;
    public Array[] proofsWordList;

    public int proofsCount;

    public string[] actualAnswers;
    public int numberTry = 2;
    public int mistake;

    //Parameters
    public bool cameraMode;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Option")
        {
            currentScene = SceneManager.GetActiveScene().name;
        }
    }

    public void updateLastScene()
    {
        lastScene = currentScene;
    }


    public void setupArray(int numberElement)
    {
        proofsLevel = new bool[numberElement];
        proofsName = new string[numberElement];
        proofsDescription = new string[numberElement];
        proofsWordList = new Array[numberElement];
    }

    public void resetLevelVAR()
    {
        //Level Crime Scene
        levelStarted = false;
        for (int i = 0; i < proofsLevel.Length; i++)
        {
            proofsLevel[i] = false;
        }
        for (int i = 0; i < proofsName.Length; i++)
        {
            proofsName[i] = "";
        }
        for (int i = 0; i < proofsDescription.Length; i++)
        {
            proofsDescription[i] = "";
        }
        for (int i = 0; i < proofsGameObject.Length; i++)
        {
            proofsGameObject[i] = null;
        }

        //Level NewsPaper
        for (int i = 0; i < actualAnswers.Length; i++)
        {
            actualAnswers[i] = null;
        }
        mistake = 0;
        numberTry = 2;
    }
}
