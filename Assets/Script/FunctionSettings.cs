using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionSettings : MonoBehaviour
{
    public DataHolder dataHolder;

    void Start()
    {
        dataHolder = FindObjectOfType<DataHolder>();
    }

    public void goBack()
    {
        SceneManager.LoadScene(dataHolder.currentScene);
    }

}
