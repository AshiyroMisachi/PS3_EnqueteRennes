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
    public string[] wordList;

    public GameObject plot;
    public Vector3 distancePlot;

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
            Instantiate(plot, transform.position + distancePlot, Quaternion.Euler(0, 0, 0));
        }
    }

    public void getPickUp(Player player)
    {
        if (canPickUp)
        {
            //Block Pick Up        
            canPickUp = false;

            //Spawn Object to show you can't pickup anymore
            Instantiate(plot, transform.position + distancePlot, Quaternion.Euler(0,0,0));

            //Store proof data 
            dataHolder.proofsLevel[myNumber] = true;
            dataHolder.proofsName[myNumber] = myName;
            dataHolder.proofsDescription[myNumber] = description;
            dataHolder.proofsGameObject[myNumber] = gameObject;
            dataHolder.proofsWordList[myNumber] = wordList;

            //Pop up Text to show you pick up
            player.popUpText.text = "Vous avez trouvé " + myName;
            player.popUpText.alpha = 1f;
            player.timerText = 0f;
        }
    }

}
