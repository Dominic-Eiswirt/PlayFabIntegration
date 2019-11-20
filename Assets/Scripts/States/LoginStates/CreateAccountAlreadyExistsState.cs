﻿using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class CreateAccountAlreadyExistsState : UIState
{
    public CreateAccountAlreadyExistsState()
    {
        referenceObj = Resources.Load("Prefabs/CreateAccountAlreadyExistsView") as GameObject;
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
