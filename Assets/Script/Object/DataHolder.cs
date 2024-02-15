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
    public string[] levelName;
    public string[] levelMoreInfo;
    public string levelCrimeScene;
    public string levelNewsPaper;
    public string levelLastNotebook;

    //Navigation in Level
    public Vector3 cameraRotation;
    public bool levelStarted;

    public bool[] proofsLevel;
    public Array[] proofsName;
    public Array[] proofsDescription;
    public GameObject[] proofsGameObject;
    public Vector3[] proofsScaleRender;
    public Vector3[] proofsRotationRender;
    public int[] actualAnswers;

    public int proofsCount;
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

    public void UpdateLastScene()
    {
        lastScene = currentScene;
    }

    public void UpdateNoteBookScene()
    {
        levelLastNotebook = SceneManager.GetActiveScene().name;
    }

    public void setupArray(int numberElement)
    {
        proofsName = new Array[numberElement];
        proofsDescription = new Array[numberElement];
        proofsGameObject = new GameObject[numberElement];
        proofsScaleRender = new Vector3[numberElement];
        proofsRotationRender = new Vector3[numberElement];
    }

    public void ResetLevelVAR()
    {
        //Level Crime Scene
        cameraRotation = Vector3.zero;
        levelLastNotebook = "NoteBook";
        levelStarted = false;
        for (int i = 0; i < proofsLevel.Length; i++)
        {
            proofsLevel[i] = false;
        }
        //for (int i = 0; i < proofsName.Length; i++)
        //{
        //    proofsName[i] = new Array[1];
        //}
        //for (int i = 0; i < proofsDescription.Length; i++)
        //{
        //    proofsDescription[i] = new Array[1];
        //}
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
            actualAnswers[i] = -1;
        }
        proofsCount = 0;
        mistake = 0;
        numberTry = 2;
    }
}