using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TraductionWord : MonoBehaviour
{
    public DataHolder dataHolder;


    public string french, english;


    void Start()
    {
        dataHolder = FindObjectOfType<DataHolder>();

        switch (dataHolder.language)
        {
            case Language.Français:
                gameObject.GetComponent<TextMeshProUGUI>().text = french;
                break;
            case Language.English:
                gameObject.GetComponent<TextMeshProUGUI>().text = english;
                break;
        }
    }
}
