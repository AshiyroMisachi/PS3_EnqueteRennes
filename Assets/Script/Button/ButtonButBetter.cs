using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonButBetter : Button, ISelectHandler, IDeselectHandler
{
    public bool isPressed => IsPressed();
    public bool isSelected;

    public void OnSelect(BaseEventData date)
    {
        isSelected = true;
    }

    public void OnDeselect(BaseEventData date)
    {
        isSelected = false;
    }

}
