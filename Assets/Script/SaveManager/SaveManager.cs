using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public SaveState currentSave;
    public DataHolder dataHolder;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        currentSave = new SaveState();
        dataHolder = FindObjectOfType<DataHolder>();
        Load();
    }
    public void Save()
    {
        var t = JsonUtility.ToJson(currentSave);
        //Save to json file
        string saveFilePath = Application.persistentDataPath + "/Save.txt";
        var Writer = File.CreateText(saveFilePath);
        Writer.Close();
        File.WriteAllText(saveFilePath, t, System.Text.Encoding.UTF8);
    }

    public void SaveOptions()
    {
        currentSave.volume = dataHolder.volume;
        currentSave.son = dataHolder.son;
        currentSave.cameraMode = dataHolder.cameraMode;
        currentSave.difficulty = dataHolder.difficulty;
        currentSave.language = dataHolder.language;

        Save();
    }

    public void SaveLevelScore()
    {
        currentSave.scoreArray = dataHolder.scoreArray;
        currentSave.scoreProofArray = dataHolder.scoreProofArray;
    }

    public void SaveCurrentLevel()
    {
        currentSave.lastLevel = dataHolder.levelName;
        currentSave.levelStarted = dataHolder.levelStarted;
    }

    public void SaveProofInfo()
    {
        currentSave.proofsLevel = dataHolder.proofsLevel;
        currentSave.proofsCount = dataHolder.proofsCount;
    }

    public void SaveNotebookInfo()
    {
        currentSave.actualAnswers = dataHolder.actualAnswers;
        currentSave.numberTry = dataHolder.numberTry;
    }

    public void Load()
    {
        //Load savestate from Json File
        string saveFilePath = Application.persistentDataPath + "/Save.txt";
        if (File.Exists(saveFilePath))
        {
            var Writer = File.OpenText(saveFilePath);
            Writer.Close();
            var t = File.ReadAllText(saveFilePath);
            currentSave = JsonUtility.FromJson<SaveState>(t);

            //Attribute var from saveState
            //Options Value
            dataHolder.volume = currentSave.volume;
            dataHolder.son = currentSave.son;
            dataHolder.cameraMode = currentSave.cameraMode;
            dataHolder.difficulty = currentSave.difficulty;
            dataHolder.language = currentSave.language;

            //Levels
            dataHolder.scoreArray = currentSave.scoreArray;
            dataHolder.scoreProofArray = currentSave.scoreProofArray;

            //Current Level
            dataHolder.levelStarted = currentSave.levelStarted;
            dataHolder.proofsLevel = currentSave.proofsLevel;
            dataHolder.actualAnswers = currentSave.actualAnswers;
            dataHolder.proofsCount = currentSave.proofsCount;
            dataHolder.numberTry = currentSave.numberTry;
        }
    }
}