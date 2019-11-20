using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class CreateAccountErrorState : UIState
{
    public GameObject createAccountErrorObj;
    public CreateAccountErrorState(UICenter center)
    {
        createAccountErrorObj = Resources.Load("Prefabs/CreateAccountErrorView") as GameObject;
        //this.center = center;
        //foreach (StateReferences r in this.center.References)
        //{
        //    myReferences = r as CreateAccountResultSharedReferences;
        //    if (myReferences != null)
        //    {
        //        break;
        //    }
        //}

    }


    public override void DisplayState()
    {
        createAccountErrorObj = GameObject.Instantiate(createAccountErrorObj, canvas.transform);
        //SetAllReferences(true);
    }
    public override void BeforeStateChange()
    {
        GameObject.Destroy(createAccountErrorObj);
       // SetAllReferences(false);
    }
    void SetAllReferences(bool condition)
    {        
        foreach (GameObject o in myReferences.GetReferencesOfState)
        {                
            o.SetActive(condition);
            if(o.GetComponent<Text>() != null)
            { 
                o.GetComponent<Text>().color = Color.red;
                o.GetComponent<Text>().text = "Error creating account, please check that the email address is in a correct format xxx@yyy.zzz";
            }
        }            
        
    }
}
