using System;
using UnityEngine;
using System.Collections.Generic;
public abstract class UIState
{
    protected static Canvas canvas;
    protected UICenter center;    
    protected StateReferences myReferences;
    public abstract void DisplayState();

    //Disable state here before change
    public abstract void BeforeStateChange();
}
