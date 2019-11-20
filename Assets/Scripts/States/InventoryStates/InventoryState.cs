using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryState : UIState
{
    public InventoryState()
    {
        
        referenceObj = Resources.Load("Prefabs/InventoryStateView") as GameObject;
        
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
