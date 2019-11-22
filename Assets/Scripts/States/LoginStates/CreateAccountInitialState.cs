using System;
using UnityEngine;
using System.Collections.Generic;
public class CreateAccountInitialState : UIState
{    
    private string myPath = "CreateAccountInitialView";
    public CreateAccountInitialState()
    {
        ResetReference(myPath);
    }

    public override void DisplayState()
    {
        referenceObj = GameObject.Instantiate(referenceObj, canvas.transform);
    }
    public override void BeforeStateChange()
    {
        GameObject.Destroy(referenceObj);
        ResetReference(myPath);
    }
}
