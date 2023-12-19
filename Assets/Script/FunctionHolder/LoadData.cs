using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadData : MonoBehaviour
{
    public DataHolder dataHolder;
    void Start()
    {
        dataHolder = FindObjectOfType<DataHolder>();

        dataHolder.UpdateLastScene();
        SceneManager.LoadScene(1);
    }
}
