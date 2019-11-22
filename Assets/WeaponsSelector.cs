using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public enum WeaponSelectedEnum { Pistol, Shotgun }
public class WeaponsSelector : MonoBehaviour
{
    public static WeaponsSelector instance;
    public WeaponSelectedEnum selectedWeapon = WeaponSelectedEnum.Pistol;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void OnWeaponSelection()
    {
        
    }
}
