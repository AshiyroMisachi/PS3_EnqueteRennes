using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Update the Text on the screen depending on the raw position of the touch
            // NOTE: rawPosition doesn't change when the touch contact is dragged
            Debug.Log("Raw Position : " + touch.position);

            // début de test de Raycat - ne fonctionne pas
            if (Physics.Raycast(touch.position, Vector3.forward, 1000))
            {
                Debug.Log("CONTACT");
            }
        }
        else
        {
            Debug.Log("No touch contacts");
        }
    }
}
