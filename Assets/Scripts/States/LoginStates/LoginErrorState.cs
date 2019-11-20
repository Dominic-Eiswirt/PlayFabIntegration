using System;
using UnityEngine;
using System.Collections.Generic;
public class LoginErrorState : UIState
{
    [SerializeField] private GameObject loginErrorObj;
    public LoginErrorState(UICenter center)
    {
        loginErrorObj = Resources.Load("Prefabs/LoginErrorView") as GameObject;
        //this.center = center;
        //foreach (StateReferences r in this.center.References)
        //{
        //    myReferences = r as LoginErrorReferences;
        //    if (myReferences != null)
        //    {
        //        break;
        //    }
        //}
    }
    public override void DisplayState()
    {
        loginErrorObj = GameObject.Instantiate(loginErrorObj, canvas.transform);
        //SetAllReferences(true);

    }
    public override void BeforeStateChange()
    {
        GameObject.Destroy(loginErrorObj);
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

