using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWeaponSelectedGameplay : MonoBehaviour
{
    public WeaponSelectedEnum myType;
    public void OnButtonClick()
    {        
        WeaponsSelector.instance.selectedWeapon = myType;        
    }
}
