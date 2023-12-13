using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FunctionSettings : MonoBehaviour
{
    //Data Holder
    public DataHolder dataHolder;

    //Parameters
    public bool cameraMode;
    public TextMeshProUGUI textCameraMode, textLanguage, textDifficulty;
    public Slider volumeSlider, sonSlider;


    void Start()
    {
        //Find DataHolder
        dataHolder = FindObjectOfType<DataHolder>();

        //Volume Son Slider
        volumeSlider.value = dataHolder.volume;
        sonSlider.value = dataHolder.son;

        //Language
        textLanguage.text = dataHolder.language.ToString();

        //Difficulty
        if (dataHolder.difficulty)
        {
            textDifficulty.text = "Normal";
        }
        else
        {
            textDifficulty.text = "Facile";
        }

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

    public void ChangeDiffuculty()
    {
        if (dataHolder.difficulty)
        {
            dataHolder.difficulty = false;
            textDifficulty.text = "Facile";
        }
        else
        {
            dataHolder.difficulty = true;
            textDifficulty.text = "Normal";
        }
    }

    public void ChangeVolume()
    {
        dataHolder.volume = volumeSlider.value;
    }

    public void ChangeSon()
    {
        dataHolder.son = sonSlider.value;
    }

    public void ChangeLanguage()
    {
        switch (dataHolder.language)
        {
            case Language.Français:
                dataHolder.language = Language.English;
                break;
            case Language.English:
                dataHolder.language = Language.Français;
                break;
        }
        textLanguage.text = dataHolder.language.ToString();
    }

}
