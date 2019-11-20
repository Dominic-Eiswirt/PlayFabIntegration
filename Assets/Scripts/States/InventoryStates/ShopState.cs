using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShopState : UIState
{
    string myPath = "ShopStateView";
    public ShopState()
    {
        ResetReference();
    }

    public override void BeforeStateChange()
    {
        GameObject.Destroy(referenceObj);
    }
    public override void DisplayState()
    {
        referenceObj = GameObject.Instantiate(referenceObj, canvas.transform);
    }
    void ResetReference()
    {
        referenceObj = Resources.Load(resourcesPath + myPath) as GameObject;
    }
}
