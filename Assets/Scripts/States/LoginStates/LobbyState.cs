using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class LobbyState : UIState
{
    string myPath = "LobbyStateView";
    public LobbyState()
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
