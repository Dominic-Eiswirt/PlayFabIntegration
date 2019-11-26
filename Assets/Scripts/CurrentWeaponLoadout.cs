using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public enum WeaponSelectedEnum { Pistol, Shotgun, Chaingun }

/// <summary>
/// This is just a container for storing the weapon that was selected in the inventory. Used because the inventory state will be destroyed 
/// and the info would be lost.
/// </summary>
public class CurrentWeaponLoadout : MonoBehaviour
{
    public static CurrentWeaponLoadout instance;    
    public Weapon selectedWeapon;
    public GameObject selectedWeaponGameObj;

    public delegate void StartGameFail();
    public event StartGameFail OnStartGameFail;
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

    public void AdjustPlayfabInventory()
    {
        if (selectedWeapon != null)
        {
            Debug.Log(selectedWeapon.myType.ToString());
            var request = new ConsumeItemRequest()
            {
                ConsumeCount = 1,
                ItemInstanceId = selectedWeapon.instanceId
            };

            PlayFabClientAPI.ConsumeItem(request, OnSuccess, OnFailure);
            UICenter.instance.ChangeState(new PlayState());
        }
        else
        {
            OnStartGameFail.Invoke();
        }

    }

    private void OnSuccess(ConsumeItemResult result)
    {
        Debug.Log("Used item");
    }
    private void OnFailure(PlayFabError error)
    {
        Debug.Log("Failed to use item, " + error.ErrorMessage);
    }

    public void CloneWeapon(Weapon parentWeapon)
    {
        if(selectedWeaponGameObj != null)
        {
            Destroy(selectedWeaponGameObj);
            selectedWeapon = null;
        }
        Debug.Log("Cloning");
        selectedWeaponGameObj = new GameObject();
        selectedWeaponGameObj.AddComponent<Weapon>();
        selectedWeaponGameObj.name = parentWeapon.name;
        Weapon weaponClone = selectedWeaponGameObj.GetComponent<Weapon>();
        weaponClone.instanceId = parentWeapon.instanceId;
        weaponClone.headText = parentWeapon.headText;
        weaponClone.myType = parentWeapon.myType;
        weaponClone.weaponPriceText = parentWeapon.weaponPriceText;
        weaponClone.weaponCardReference = parentWeapon.weaponCardReference;
        //Debug.Log("Selected weapon is  "+ weaponClone);
        selectedWeapon = weaponClone;
    }

    public IWeapon GetCurrentWeaponFromLoadout()
    {
        if (selectedWeapon.myType == WeaponSelectedEnum.Pistol)
        {
            return new Pistol();
        }
        else if (selectedWeapon.myType == WeaponSelectedEnum.Shotgun)
        {
            return new Shotgun();
        }
        else if (selectedWeapon.myType == WeaponSelectedEnum.Chaingun)
        {
            return new Chaingun();
        }
        else
        {
            Debug.LogError("Error with equipping weapon, either enum or IWeapon class doesn't exist");
            return null;
        }
    }
}
