using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class CreateAccountAlreadyExistsState : UIState
{
    string myPath = "CreateAccountAlreadyExistsView";
    public CreateAccountAlreadyExistsState()
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
