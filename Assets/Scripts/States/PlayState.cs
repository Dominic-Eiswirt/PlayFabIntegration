using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayState : UIState
{
    private string myPath = "PlayStateView";
    public PlayState()
    {
        ResetReference(myPath);
    }
    public override void DisplayState()
    {
        referenceObj = GameObject.Instantiate(referenceObj);        
    }
    public override void BeforeStateChange()
    {
        GameObject.Destroy(referenceObj);
        ResetReference(myPath);
    }
}
