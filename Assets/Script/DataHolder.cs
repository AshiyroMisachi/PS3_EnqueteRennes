using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataHolder : MonoBehaviour
{
    public string currentScene;
    public int[] scoreArray;
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
