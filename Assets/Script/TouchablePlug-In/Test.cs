using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour, ITouchable
{

    public void OnTouchedDown(Vector3 touchPosition)
    {
    }


    public void OnTouchedStay(Vector3 touchPosition)
    {
        transform.position = touchPosition;
    }

    public void OnTouchedUp()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
