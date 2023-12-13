using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Proof : MonoBehaviour
{
    //Universsal Var
    public TypeProof type;
    public string myName;
    public string description;
    public int myNumber;
    public GameObject myGameObjectRender;
    public Vector3 myScaleRender;
    public Vector3 myRotationRender;

    public GameObject plot;
    public Vector3 distancePlot;
    public Vector3 clueLightPosition;

    private bool canPickUp = true;

    //Data Holder
    public DataHolder dataHolder;

    void Start()
    {
        //Find dataholder
        dataHolder = FindObjectOfType<DataHolder>();

        //If already pick
        if (dataHolder.proofsLevel[myNumber])
        {
            canPickUp = false;
            Instantiate(plot, distancePlot, Quaternion.Euler(-90, 0, 0));
        }
    }

    public void getPickUp(Player player)
    {
        if (canPickUp)
        {
            //Block Pick Up        
            canPickUp = false;

            //Spawn Object to show you can't pickup anymore
            Instantiate(plot, distancePlot, Quaternion.Euler(-90, 0, 0));

            //Store proof data 
            dataHolder.proofsLevel[myNumber] = true;
            dataHolder.proofsName[myNumber] = myName;
            dataHolder.proofsDescription[myNumber] = description;
            dataHolder.proofsCount++;

            dataHolder.proofsGameObject[myNumber] = myGameObjectRender;
            dataHolder.proofsScaleRender[myNumber] = myScaleRender;
            dataHolder.proofsRotationRender[myNumber] = myRotationRender;

            //Inspection Mode
            Camera.main.fieldOfView = 60;
            player.currentMode = false;
            player.insNameProof.text = myName;
            player.insDescriptionProof.text = description;
            player.insProof = Instantiate(myGameObjectRender, player.transform);
            player.insProof.transform.localPosition = new Vector3(0, 0, 1);
            player.insProof.transform.localScale = myScaleRender / 5;
            player.insProof.transform.localEulerAngles = myRotationRender;

            //Pop up Text to show you pick up
            player.popUpText.text = "Vous avez trouvé " + myName;
            player.popUpText.alpha = 1f;
            player.timerText = 0f;
        }
    }

    public void showFeedback()
    {
        Debug.Log("A proof is located at the Camera center");
    }

}
