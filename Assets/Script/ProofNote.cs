using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProofNote : MonoBehaviour
{
    public DataHolder dataHolder;
    public FunctionNoteBook manager;

    //Var
    public string myName;
    public TextMeshProUGUI myText;
    public string myDescription;
    public GameObject myGameObject;
    public Vector3 myScale;
    public Vector3 myRotation;

    private void Start()
    {
        dataHolder = FindObjectOfType<DataHolder>();
        manager = FindObjectOfType<FunctionNoteBook>();
    }

    public void ShowProof()
    {
        manager.proofName.text = myName;
        manager.proofDescription.text = myDescription;
        manager.switchRender(myGameObject, myScale, myRotation);
        manager.baseScaleRender = myScale;
    }
}
