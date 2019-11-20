using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryState : UIState
{
    private GameObject inventoryGameObject;    
    
    public InventoryState (UICenter center)
    {
        
        inventoryGameObject = Resources.Load("Prefabs/InventoryStateView") as GameObject;
        //this.center = center;
        //foreach (StateReferences r in this.center.References)
        //{
        //    myReferences = r as InventoryReferences;
        //    if (myReferences != null)
        //    {                
        //        break;
        //    }
        //}
        
    }
    public override void BeforeStateChange()
    {
        GameObject.Destroy(inventoryGameObject); 
        //inventorySpawner.gameObject.SetActive(false);
    }

    public override void DisplayState()
    {
        inventoryGameObject = GameObject.Instantiate(inventoryGameObject, canvas.transform);
        
        ////Turn all on the objects, and also grab the script we want to send a command back to a monobehaviour to instantiate the grid.
        ////Instantiation happens in the spawner. Spawner will check if childs exist before instantiating.
        //foreach (GameObject o in myReferences.GetReferencesOfState)
        //{            
        //    o.gameObject.SetActive(true);
        //    if(o.GetComponent<InventorySpawner>() != null)
        //    {   
        //        inventoryGameObject = o;                
        //        inventorySpawner = o.GetComponent<InventorySpawner>();                
        //    }
        //}
        //inventoryGameObject.SetActive(true);
        //inventorySpawner.InstantiateCells();
    }
}
