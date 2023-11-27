using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataHolder : MonoBehaviour
{
    //Navigation in menu
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

    //Parameters
    public bool cameraMode;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("MainScreen");
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Option")
        {
            currentScene = SceneManager.GetActiveScene().name;
        }
    }
}
