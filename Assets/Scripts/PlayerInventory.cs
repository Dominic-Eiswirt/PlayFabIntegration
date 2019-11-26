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
        public int chainGunCount;
        public GameObject Pistol;
        public GameObject Shotgun;
        public GameObject Chaingun;
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
        int chainGunCount = 0;
        foreach (ItemInstance item in result.Inventory)
        {
            if(item.ItemId == WeaponSelectedEnum.Pistol.ToString())
            {
                for(int i = 0; i < item.RemainingUses; i++)
                { 
                    pistolCount++;
                    objectsInSpawnOrder.Add(weapons.Pistol);
                    objectsInSpawnOrder[objectsInSpawnOrder.Count-1].GetComponent<Weapon>().instanceId = item.ItemInstanceId;
                    
                }
            }
            if(item.ItemId == WeaponSelectedEnum.Shotgun.ToString())
            {
                for (int i = 0; i < item.RemainingUses; i++)
                {
                    shotgunCount++;
                    objectsInSpawnOrder.Add(weapons.Shotgun);
                    objectsInSpawnOrder[objectsInSpawnOrder.Count - 1].GetComponent<Weapon>().instanceId = item.ItemInstanceId;
                }
            }
            if(item.ItemId == WeaponSelectedEnum.Chaingun.ToString())
            {
                for (int i = 0; i < item.RemainingUses; i++)
                {
                    Debug.Log("in chain");
                    chainGunCount++;
                    objectsInSpawnOrder.Add(weapons.Chaingun);
                    objectsInSpawnOrder[objectsInSpawnOrder.Count - 1].GetComponent<Weapon>().instanceId = item.ItemInstanceId;
                }
            }
        }

        weapons.chainGunCount = chainGunCount;
        weapons.shotgunCount = shotgunCount;
        weapons.pistolCount = pistolCount;
        GetComponentInChildren<InventorySpawner>().InstantiateCells();
    }

    

}
