using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Weapon : MonoBehaviour
{
    public WeaponCard weaponCardReference;
    public Text weaponPriceText;
    public Text headText;
    [SerializeField] private Image background;
    public WeaponEnum myType;
    public string instanceId;    

    private void Awake()
    {
        //If bg is null we do nothing because the item is only a reference for the game manager and is invisible anyway. It will just contain data about instanceid
        if (background != null)
        {
            background.sprite = weaponCardReference.cardImage;
            weaponPriceText.text = "";            
        }
    }

}

