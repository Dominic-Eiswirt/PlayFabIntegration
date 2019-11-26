using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
public class PlayerInventory : MonoBehaviour
{
    [Serializable]
    public struct Weapons
    { 
        public GameObject PistolPrefab;
        public GameObject ShotgunPrefab;
        public GameObject ChaingunPrefab;
    }
    public Weapons weapons = new Weapons();    
    public List<GameObject> objectsInSpawnOrder = new List<GameObject>();

    public List<GameObject> GetSpawnList()
    {
        return objectsInSpawnOrder;
    }

    public void SetInventoryPlayFab(GetUserInventoryResult result)
    {
        foreach (ItemInstance item in result.Inventory)
        {
            if(item.ItemId == WeaponEnum.Pistol.ToString())
            {
                for(int i = 0; i < item.RemainingUses; i++)
                { 
                    objectsInSpawnOrder.Add(weapons.PistolPrefab);
                    objectsInSpawnOrder[objectsInSpawnOrder.Count-1].GetComponent<Weapon>().instanceId = item.ItemInstanceId;
                    
                }
            }
            if(item.ItemId == WeaponEnum.Shotgun.ToString())
            {
                for (int i = 0; i < item.RemainingUses; i++)
                {
                    objectsInSpawnOrder.Add(weapons.ShotgunPrefab);
                    objectsInSpawnOrder[objectsInSpawnOrder.Count - 1].GetComponent<Weapon>().instanceId = item.ItemInstanceId;
                }
            }
            if(item.ItemId == WeaponEnum.Chaingun.ToString())
            {
                for (int i = 0; i < item.RemainingUses; i++)
                {                    
                    objectsInSpawnOrder.Add(weapons.ChaingunPrefab);
                    objectsInSpawnOrder[objectsInSpawnOrder.Count - 1].GetComponent<Weapon>().instanceId = item.ItemInstanceId;
                }
            }
        }
        GetComponentInChildren<InventorySpawner>().InstantiateCells();
    }

    

}
