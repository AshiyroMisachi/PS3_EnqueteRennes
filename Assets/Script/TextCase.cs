using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCase : MonoBehaviour
{
    //DataHolder
    public DataHolder dataHolder;

    //Var 
    public string currentName;
    public string correction;
    public bool isOccuped;
    public MeshRenderer myColor;
    void Start()
    {
        //Find DataHolder
        dataHolder = FindObjectOfType<DataHolder>();

        //Setup
        isOccuped = false;
        myColor = gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collision)
    {
        TextProof textProof = collision.GetComponent<TextProof>();
        if (textProof != null)
        {
            if (!textProof.isPick && !isOccuped)
            {
                myColor.material.color = new Color(0.49f, 0.8f, 0.82f, 0f);
                isOccuped = true;
                currentName = textProof.myName;
                Transform collisionTransform = collision.gameObject.GetComponent<Transform>();
                collisionTransform.position = transform.position;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        myColor.material.color = new Color(0.49f, 0.8f, 0.82f, 1f);
        isOccuped = false;
        currentName = "";
    }
}
