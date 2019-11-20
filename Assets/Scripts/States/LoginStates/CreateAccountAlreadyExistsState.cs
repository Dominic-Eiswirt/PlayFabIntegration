using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class CreateAccountAlreadyExistsState : UIState
{
    GameObject createAccountAlreadyExistsObj;
    public CreateAccountAlreadyExistsState(UICenter center)
    {
        createAccountAlreadyExistsObj = Resources.Load("Prefabs/CreateAccountAlreadyExistsView") as GameObject;
       // this.center = center;
       // foreach (StateReferences r in this.center.References)
       // {
       //     myReferences = r as CreateAccountResultSharedReferences;
       //     if (myReferences != null)
       //     {
       //         Debug.Log("Breaking account already exists, the references count is "+myReferences.GetReferencesOfState.Length);
       //         break;
       //     }
       // }
    }


    public override void DisplayState()
    {
        createAccountAlreadyExistsObj = GameObject.Instantiate(createAccountAlreadyExistsObj, canvas.transform);
        //SetAllReferences(true);
    }
    public override void BeforeStateChange()
    {
        GameObject.Destroy(createAccountAlreadyExistsObj);
        //SetAllReferences(false);
    }
    void SetAllReferences(bool condition)
    {        
        foreach (GameObject o in myReferences.GetReferencesOfState)
        {                
            o.SetActive(condition);
            if(o.GetComponent<Text>() != null)
            { 
                o.GetComponent<Text>().color = Color.red;
                o.GetComponent<Text>().text = "Account already exists! Create a new account or if you are the owner go back and login.";
            }
        }            
        
    }
}
