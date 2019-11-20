using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class CreateAccountSuccessState : UIState
{    
    public CreateAccountSuccessState()
    {
        referenceObj = Resources.Load("Prefabs/CreateAccountSuccessView") as GameObject; 
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
