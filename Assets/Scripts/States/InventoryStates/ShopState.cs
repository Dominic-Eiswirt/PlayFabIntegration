using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShopState : UIState
{
    private string myPath = "ShopStateView";
    public ShopState()
    {
        ResetReference(myPath);
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
