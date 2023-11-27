using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionHolderNewsPaper : MonoBehaviour
{
    public DataHolder dataHolder;

    public GameObject textproof;

    private void Start()
    {
        //Find dataholder
        dataHolder = FindObjectOfType<DataHolder>();

        //Spawn TextProofs
        for (int i = 0; i < dataHolder.proofsLevel.Length; i++)
        {
            if (dataHolder.proofsLevel[i])
            {
                GameObject newObject = Instantiate(textproof, dataHolder.textProofsCoords[i], transform.rotation);
                TextProof newTextProof = newObject.gameObject.GetComponent<TextProof>();
                newTextProof.myName = dataHolder.proofsName[i];
                newTextProof.myNumber = i;
            }
        }
    }

    public void goBackScene()
    {
        SceneManager.LoadScene(dataHolder.lastScene);
    }
}
