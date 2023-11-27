using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchable 
{
    public void OnTouchedDown(Vector3 touchPosition);
    public void OnTouchedStay(Vector3 touchPosition);
    public void OnTouchedUp();

}
