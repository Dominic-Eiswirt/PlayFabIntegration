using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class LeaderboardState : UIState
{
    private string myPath = "LeaderboardStateView";
    public LeaderboardState()
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
