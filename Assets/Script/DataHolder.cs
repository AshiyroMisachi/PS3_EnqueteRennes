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
    public TypeProof[] proofsType;
    public GameObject[] proofsGameObject;

    public int proofsCount;

    public GameObject[] textProofs;
    public Vector3[] textProofsCoords;

    public int numberTry = 3;
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
}
