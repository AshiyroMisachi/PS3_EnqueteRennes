using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proof : MonoBehaviour
{
    //Universsal Var
    public TypeProof type;
    public string myName;
    public string description;
    public GameObject plot;
    public Vector3 distancePlot;

    public bool canPickUp;

    public void getPickUp(Player player)
    {
        if (canPickUp)
        {
            //Block Pick Up        
            Debug.Log("GetPickUp");
            canPickUp = false;

            //Spawn Object to show you can't pickup anymore
            Instantiate(plot, transform.position + distancePlot, transform.rotation);

            //Add Proof to player list
            player.proofs.Add(this);

            //Pop up Text to show you pick up
            player.popUpText.text = "You found " + myName;
            player.popUpText.alpha = 1f;
            player.timerText = 0f;
        }
    }

}
