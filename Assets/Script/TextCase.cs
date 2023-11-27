using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCase : MonoBehaviour
{
    //Var 
    public string currentName;
    public bool isOccuped;
    public MeshRenderer myColor;
    void Start()
    {
        isOccuped = false;
        myColor = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collision)
    {
        Debug.Log("Collision");
        TextProof textProof = collision.GetComponent<TextProof>();
        if (textProof != null)
        {
            if (!textProof.isPick && !isOccuped)
            {
                isOccuped = true;
                currentName = textProof.name;
                Transform collisionTransform = collision.gameObject.GetComponent<Transform>();
                collisionTransform.position = transform.position;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isOccuped = false;
    }
}
