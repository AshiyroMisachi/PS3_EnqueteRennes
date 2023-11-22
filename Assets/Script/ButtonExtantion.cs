using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ButtonButBetter))]
public class ButtonExtantion : MonoBehaviour
{

    private ButtonButBetter better;
    public UnityEvent onStay;
    // Start is called before the first frame update
    void Awake()
    {
        better = GetComponent<ButtonButBetter>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(better.isPressed)
        {
            onStay.Invoke();    
        }
    }
}
