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
    public Sprite gyroscope;
    public Sprite arrows;
    public Image renderImageCameraMode;
    public TextMeshProUGUI textLanguage, textDifficulty;
    public Image difficultyButton;
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
            switch (dataHolder.language)
            {
                case Language.Français:
                    textDifficulty.text = "Normal";
                    difficultyButton.color = new Color(0.5f, 0.6f, 0.2f);
                    break;
                case Language.English:
                    textDifficulty.text = "Normal";
                    difficultyButton.color = new Color(0.5f, 0.6f, 0.2f);
                    break;
            }
        }
        else
        {
            switch (dataHolder.language)
            {
                case Language.Français:
                    textDifficulty.text = "Facile";
                    difficultyButton.color = new Color(0.2f, 0.6f, 0.3f);
                    break;
                case Language.English:
                    textDifficulty.text = "Easy";
                    difficultyButton.color = new Color(0.2f, 0.6f, 0.3f);
                    break;
            }
        }

        //CameraMode
        cameraMode = dataHolder.cameraMode;
        if (cameraMode)
        {
            renderImageCameraMode.sprite = gyroscope;
        }
        else
        {
            renderImageCameraMode.sprite = arrows;
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
            renderImageCameraMode.sprite = arrows;
        }
        else
        {
            cameraMode = true;
            renderImageCameraMode.sprite = gyroscope;
        }
        dataHolder.cameraMode = cameraMode;
    }

    public void ChangeDiffuculty()
    {
        if (dataHolder.difficulty)
        {
            dataHolder.difficulty = false;
            switch (dataHolder.language)
            {
                case Language.Français:
                    textDifficulty.text = "Facile";
                    difficultyButton.color = new Color(0.2f, 0.6f, 0.3f);
                    break;
                case Language.English:
                    textDifficulty.text = "Easy";
                    difficultyButton.color = new Color(0.2f, 0.6f, 0.3f);
                    break;
            }
        }
        else
        {
            dataHolder.difficulty = true;
            switch (dataHolder.language)
            {
                case Language.Français:
                    textDifficulty.text = "Normal";
                    difficultyButton.color = new Color(0.5f, 0.6f, 0.2f);
                    break;
                case Language.English:
                    textDifficulty.text = "Normal";
                    difficultyButton.color = new Color(0.5f, 0.6f, 0.2f);
                    break;
            }
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
