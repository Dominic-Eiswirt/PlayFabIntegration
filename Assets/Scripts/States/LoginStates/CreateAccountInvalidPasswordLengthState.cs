using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class CreateAccountInvalidPasswordLengthState : UIState
{
    private string myPath = "CreateAccountInvalidPasswordView";
    public CreateAccountInvalidPasswordLengthState()
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
