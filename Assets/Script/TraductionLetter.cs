using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TraductionLetter : MonoBehaviour
{
    public DataHolder dataHolder;


    public Material french, english;


    void Start()
    {
        dataHolder = FindObjectOfType<DataHolder>();

        switch (dataHolder.language)
        {
            case Language.Français:
                gameObject.GetComponent<MeshRenderer>().material = french;
                break;
            case Language.English:
                gameObject.GetComponent<MeshRenderer>().material = english;
                break;
        }
    }
}
