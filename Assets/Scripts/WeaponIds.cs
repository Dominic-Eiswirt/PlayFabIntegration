using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public static class WeaponIds
{
    //these ids are the same as from playfab
    public const string pistolID = "Pistol";
    public const string shotgunID = "Shotgun";
    public const string chaingunID = "Chaingun";

    public static List<string> allIds = new List<string>() { pistolID, shotgunID, chaingunID };
    
    //returns true as soon as we have a match in our ids
    public static int? CompareIdsAndGetIndex(string storeId)
    {        
        int i = 0;
        foreach(WeaponEnum e in Enum.GetValues(typeof(WeaponEnum)))
        {
            if(storeId == e.ToString())
            {
                return i;
            }
            i++;
        }
        return null;
    }
}
