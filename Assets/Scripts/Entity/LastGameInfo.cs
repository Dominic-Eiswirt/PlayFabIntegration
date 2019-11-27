using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class LastGameInfo
{
    public WeaponEnum lastUsedWeaponType;
    public int killCount = 0;
    public bool CompareToEnum(WeaponEnum itemToCompare)
    {
        if(itemToCompare == lastUsedWeaponType)
        {
            return true;
        }
        return false;
    }
}
