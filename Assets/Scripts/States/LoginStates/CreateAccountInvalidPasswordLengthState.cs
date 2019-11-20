using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class CreateAccountInvalidPasswordLengthState : UIState
{
    public CreateAccountInvalidPasswordLengthState()
    {
        referenceObj = Resources.Load("Prefabs/CreateAccountInvalidPasswordView") as GameObject;
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
