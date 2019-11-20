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
        public int shotgunCount;
        public int pistolCount;
        public GameObject Pistol;
        public GameObject Shotgun;        
    }
    public Weapons weapons = new Weapons();    
    public List<GameObject> objectsInSpawnOrder = new List<GameObject>();

    public List<GameObject> GetSpawnList()
    {
        return objectsInSpawnOrder;
    }

    public void SetInventoryPlayFab(GetUserInventoryResult result)
    {
        int pistolCount = 0;
        int shotgunCount = 0;
        foreach (ItemInstance item in result.Inventory)
        {
            if(item.ItemId == WeaponIds.pistolID)
            {
                for(int i = 0; i < item.RemainingUses; i++)
                { 
                    pistolCount++;
                    objectsInSpawnOrder.Add(weapons.Pistol);
                }
            }
            if(item.ItemId == WeaponIds.shotgunID)
            {
                for (int i = 0; i < item.RemainingUses; i++)
                {
                    shotgunCount++;
                    objectsInSpawnOrder.Add(weapons.Shotgun);
                }
            }
        }

        weapons.shotgunCount = shotgunCount;
        weapons.pistolCount = pistolCount;
        GetComponentInChildren<InventorySpawner>().InstantiateCells();
    }

    

}
