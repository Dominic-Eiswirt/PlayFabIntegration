using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class CreateAccountSuccessState : UIState
{
    private GameObject createAccountSuccessObj;
    public CreateAccountSuccessState(UICenter center)
    {
        createAccountSuccessObj = Resources.Load("Prefabs/CreateAccountSuccessView") as GameObject; 
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
        createAccountSuccessObj = GameObject.Instantiate(createAccountSuccessObj, canvas.transform);
        //SetAllReferences(true);
    }
    public override void BeforeStateChange()
    {     
        GameObject.Destroy(createAccountSuccessObj);
        //SetAllReferences(false);
    }
    void SetAllReferences(bool condition)
    {        
        foreach (GameObject o in myReferences.GetReferencesOfState)
        {                
            o.SetActive(condition);
            if(o.GetComponent<Text>() != null)
            { 
                o.GetComponent<Text>().color = Color.green;
                o.GetComponent<Text>().text = "Account created successfully! You can now go back and login.";
            }
        }            
        
    }
}
