using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public static class WeaponIds
{
    //these ids are the same as from playfab
    public const string pistolID = "Pistol";
    public const string shotgunID = "Shotgun";
    public static List<string> allIds = new List<string>() { pistolID, shotgunID };
    
    //returns true as soon as we have a match in our ids
    public static int? CompareIdsAndGetIndex(string storeId)
    {        
        for(int i = 0; i < allIds.Count; i++)
        {            
            if(storeId == allIds[i])
            {
                return i;
            }            
        }
        return null;
    }
}
