using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextProof : MonoBehaviour
{
    public DataHolder dataHolder;
    public FunctionHolderNewsPaper manager;

    //Var Text Proof
    public string myName;
    public TextMeshProUGUI textName;

    // Start is called before the first frame update
    void Start()
    {
        dataHolder = FindObjectOfType<DataHolder>();
        manager = FindObjectOfType<FunctionHolderNewsPaper>();
        textName.text = myName;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void selectWord()
    {
        manager.wordSelected = myName;
        manager.ApplyWord();
    }
}
