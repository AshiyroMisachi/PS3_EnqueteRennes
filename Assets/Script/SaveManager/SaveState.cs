using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveState
{
    //Options
    public float volume = 0.5f, son = 0.5f;
    public bool cameraMode = true, difficulty = false;
    public Language language = Language.English;

    //Levels
    public float[] scoreArray = new float[3];
    public int[] scoreProofArray = new int[3];

    //Current Level Info
    public string[] lastLevel = new string[2];

    public bool levelStarted;
    public bool[] proofsLevel = new bool[20];
    public int proofsCount;
    public int[] actualAnswers = new int[20];
    public int numberTry;
}