using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class WeaponButtonEvent : MonoBehaviour
{
    public void OnButtonClick()
    {
        ShopManager.instance.RequestPurchase(GetComponent<Weapon>().weaponCardReference.ID);
    }
}
