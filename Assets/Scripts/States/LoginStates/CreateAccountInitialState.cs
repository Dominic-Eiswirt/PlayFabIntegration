using System;
using UnityEngine;
using System.Collections.Generic;
public class CreateAccountInitialState : UIState
{
    public GameObject createAccountInitialObj;
    public CreateAccountInitialState(UICenter center)
    {
        createAccountInitialObj = Resources.Load("Prefabs/CreateAccountInitialView") as GameObject;
        //this.center = center;
        //foreach (StateReferences r in this.center.References)
        //{
        //    myReferences = r as CreateAccountInitialReferences;
        //    if (myReferences != null)
        //    {
        //        break;
        //    }
        //}
    }


    public override void DisplayState()
    {
        createAccountInitialObj = GameObject.Instantiate(createAccountInitialObj, canvas.transform);
        //SetAllReferences(true);
    }
    public override void BeforeStateChange()
    {
        GameObject.Destroy(createAccountInitialObj);
        //SetAllReferences(false);
    }
    void SetAllReferences(bool condition)
    {   
        foreach (GameObject o in myReferences.GetReferencesOfState)
        {                
            o.SetActive(condition);
        }            
        
    }
}
