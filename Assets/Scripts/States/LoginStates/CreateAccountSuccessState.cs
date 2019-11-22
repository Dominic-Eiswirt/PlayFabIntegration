using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class CreateAccountSuccessState : UIState
{    
    private string myPath = "CreateAccountSuccessView";
    public CreateAccountSuccessState()
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
