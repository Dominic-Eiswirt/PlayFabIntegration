using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class CreateAccountErrorState : UIState
{
    public CreateAccountErrorState()
    {
        referenceObj = Resources.Load("Prefabs/CreateAccountErrorView") as GameObject;
    }


    public override void DisplayState()
    {
        referenceObj = GameObject.Instantiate(referenceObj, canvas.transform);   
    }
    public override void BeforeStateChange()
    {
        GameObject.Destroy(referenceObj);     
    }
}
