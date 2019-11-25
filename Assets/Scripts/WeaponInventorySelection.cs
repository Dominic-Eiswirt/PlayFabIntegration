using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;

public class WeaponInventorySelection : MonoBehaviour
{
    public WeaponSelectedEnum myType;
    public void OnButtonClick()
    {           
        CurrentWeaponLoadout.instance.CloneWeapon(GetComponentInParent<Weapon>());
    }
}
