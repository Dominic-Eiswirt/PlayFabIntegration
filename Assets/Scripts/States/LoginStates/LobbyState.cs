using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class LobbyState : UIState
{
    private string myPath = "LobbyStateView";
    public LobbyState()
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
