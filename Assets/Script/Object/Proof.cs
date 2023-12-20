using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Proof : MonoBehaviour, ITouchable
{
    //Universsal Var
    public string[] myName;
    public string[] description;
    public int myNumber;
    public GameObject myGameObjectRender;
    public Vector3 myScaleRender;
    public Vector3 myRotationRender;

    public GameObject plot;
    public Vector3 distancePlot;

    private bool canPickUp = true;

    //Data Holder
    public DataHolder dataHolder;
    public Player player;

    void Start()
    {
        //Find dataholder
        dataHolder = FindObjectOfType<DataHolder>();
        player = FindObjectOfType<Player>();

        //If already pick
        if (dataHolder.proofsLevel[myNumber])
        {
            canPickUp = false;
            Instantiate(plot, distancePlot, Quaternion.Euler(-90, 0, 0));
        }
    }

    public void getPickUp()
    {
        if (canPickUp && player.currentMode)
        {
            //Block Pick Up        
            player.feedBackProof.Play();
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

            ShowInspection();

            //Pop up Text to show you pick up
            player.popUpText.text = player.feedBackProofFound[(int)dataHolder.language] + myName[(int)dataHolder.language];
            player.popUpText.alpha = 1f;
            player.timerText = 0f;
        }
        else if (player.currentMode)
        {
            ShowInspection();
        }
    }

    public void ShowInspection()
    {
        //Inspection Mode
        Camera.main.fieldOfView = 60;
        player.currentMode = false;
        player.insNameProof.text = myName[(int)dataHolder.language];
        player.insDescriptionProof.text = description[(int)dataHolder.language];
        player.insProof = Instantiate(myGameObjectRender, player.transform);
        player.insProof.transform.localPosition = new Vector3(0, 0.06f, 1);
        player.insProof.transform.localScale = myScaleRender / 5;
        player.insProof.transform.localEulerAngles = myRotationRender;
        player.blackImage.SetActive(false);
    }
    public void showFeedback()
    {
        Debug.Log("A proof is located at the Camera center");
    }

    public bool GetCanPickUp()
    {
        return canPickUp;
    }

    public void OnTouchedDown(Vector3 touchPosition)
    {
        if (MouseOverUILayerObject.IsPointerOverUIObject() == false)
        {
            getPickUp();
        }
    }

    public void OnTouchedStay(Vector3 touchPosition)
    {
    }

    public void OnTouchedUp()
    {
    }
}
