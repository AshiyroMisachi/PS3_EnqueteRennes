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
    public int[] scoreArray;

    //Navigation in Level
    public bool levelStarted;

    public bool[] proofsLevel;
    public string[] proofsName;
    public string[] proofsDescription;
    public TypeProof[] proofsType;
    public GameObject[] proofsGameObject;

    public GameObject[] textProofs;
    public Vector3[] textProofsCoords;

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
