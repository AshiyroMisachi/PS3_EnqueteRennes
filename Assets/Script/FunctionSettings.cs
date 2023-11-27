using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionSettings : MonoBehaviour
{
    //Data Holder
    public DataHolder dataHolder;

    //Parameters
    public bool cameraMode;
    public TextMeshProUGUI textCameraMode;

    void Start()
    {
        //Find DataHolder
        dataHolder = FindObjectOfType<DataHolder>();

        //CameraMode
        cameraMode = dataHolder.cameraMode;
        if (cameraMode)
        {
            textCameraMode.text = "Gyroscope";
        }
        else
        {
            textCameraMode.text = "Flèche";
        }
    }

    //Navigation
    public void goBack()
    {
        SceneManager.LoadScene(dataHolder.currentScene);
    }

    //Change CameraMode
    public void changeCameraMode()
    {
        if (cameraMode)
        {
            cameraMode = false;
            textCameraMode.text = "Flèche";
        }
        else
        {
            cameraMode = true;
            textCameraMode.text = "Gyroscope";
        }
        dataHolder.cameraMode = cameraMode;
    }

}
