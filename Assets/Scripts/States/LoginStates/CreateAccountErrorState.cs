using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class CreateAccountErrorState : UIState
{
    string myPath = "CreateAccountErrorView";
    public CreateAccountErrorState()
    {
        ResetReference();
    }


    public override void DisplayState()
    {
        referenceObj = GameObject.Instantiate(referenceObj, canvas.transform);   
    }
    public override void BeforeStateChange()
    {
        GameObject.Destroy(referenceObj);
        ResetReference();
    }
    void ResetReference()
    {
        referenceObj = Resources.Load(resourcesPath + myPath) as GameObject;
    }
}
