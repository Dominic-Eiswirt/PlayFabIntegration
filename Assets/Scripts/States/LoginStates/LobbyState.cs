using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class LobbyState : UIState
{    
    public LobbyState()
    {
        referenceObj = Resources.Load("Prefabs/LobbyStateView") as GameObject;
    }
    public override void BeforeStateChange()
    {
        GameObject.Destroy(referenceObj);
    }

    public override void DisplayState()
    {
        referenceObj = GameObject.Instantiate(referenceObj, canvas.transform);
    }
}
