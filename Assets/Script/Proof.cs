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

    public GameObject plot;
    public Vector3 distancePlot;

    public bool canPickUp;

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
            //Spawn Object to show you can't pickup anymore
            Instantiate(plot, transform.position + distancePlot, transform.rotation);
        }
    }

    public void getPickUp(Player player)
    {
        if (canPickUp)
        {
            //Block Pick Up        
            canPickUp = false;

            //Spawn Object to show you can't pickup anymore
            Instantiate(plot, transform.position + distancePlot, transform.rotation);

            /*//Add Proof to player list
            player.proofs.Add(this);
            dataHolder.proofsLevel.Add(this);*/
            player.proofsList[myNumber] = true;

            //Store proof data 
            dataHolder.proofsLevel[myNumber] = true;
            dataHolder.proofsName[myNumber] = myName;
            dataHolder.proofsDescription[myNumber] = description;
            dataHolder.proofsType[myNumber] = type;
            dataHolder.proofsGameObject[myNumber] = gameObject;

            //Pop up Text to show you pick up
            player.popUpText.text = "You found " + myName;
            player.popUpText.alpha = 1f;
            player.timerText = 0f;
        }
    }

}
