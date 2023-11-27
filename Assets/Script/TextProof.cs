using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextProof : MonoBehaviour, ITouchable
{
    //Var Text Proof
    public bool isPick;
    public string myName;

    public void OnTouchedDown(Vector3 touchPosition)
    {
        isPick = true;
    }

    public void OnTouchedStay(Vector3 touchPosition)
    {
        transform.position = touchPosition;
    }

    public void OnTouchedUp()
    {
        isPick = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     //Reset Position
     if (!isPick)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 4f);
        }
    }
}
