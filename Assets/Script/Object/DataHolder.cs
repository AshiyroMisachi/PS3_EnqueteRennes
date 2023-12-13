using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataHolder : MonoBehaviour
{
    //Navigation in menu
    public string lastScene;
    public string currentScene;

    //Selection Level
    public float[] scoreArray;
    public int[] scoreProofArray;
    public int levelSelectedNumber;
    public string levelName;
    public string levelNewsPaper;
    public string levelLastNotebook;

    //Navigation in Level
    public bool levelStarted;

    public bool[] proofsLevel;
    public string[] proofsName;
    public string[] proofsDescription;
    public GameObject[] proofsGameObject;
    public Vector3[] proofsScaleRender;
    public Vector3[] proofsRotationRender;

    public int proofsCount;

    public string[] actualAnswers;
    public int numberTry = 2;
    public int mistake;

    //Parameters
    public float volume, son;
    public bool cameraMode, difficulty;
    public Language language;
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

    public void UpdateNoteBookScene()
    {
        levelLastNotebook = SceneManager.GetActiveScene().name;
    }

    public void setupArray(int numberElement)
    {
        proofsLevel = new bool[numberElement];
        proofsName = new string[numberElement];
        proofsDescription = new string[numberElement];
        proofsGameObject = new GameObject[numberElement];
        proofsScaleRender = new Vector3[numberElement];
        proofsRotationRender = new Vector3[numberElement];
    }

    public void resetLevelVAR()
    {
        //Level Crime Scene
        levelLastNotebook = "NoteBook";
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
        for (int i = 0; i < proofsScaleRender.Length; i++)
        {
            proofsScaleRender[i] = new Vector3(1, 1, 1);
        }
        for (int i = 0; i < proofsRotationRender.Length; i++)
        {
            proofsRotationRender[i] = new Vector3(1, 1, 1);
        }

        //Level NewsPaper
        for (int i = 0; i < actualAnswers.Length; i++)
        {
            actualAnswers[i] = null;
        }
        proofsCount = 0;
        mistake = 0;
        numberTry = 2;
    }
}
