using System;
using UnityEngine;
using System.Collections.Generic;
public abstract class UIState
{
    protected const string resourcesPath = "Prefabs/StateView/";
    protected UIState()
    {        
        if (canvas == null)
        {
            canvas = UICenter.instance.canvas;
        }
    }
    protected static Canvas canvas;
    protected GameObject referenceObj;    
    public abstract void DisplayState();

    //Disable state here before change
    public abstract void BeforeStateChange();
}
