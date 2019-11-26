using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class FailedToStartGameMessage : MonoBehaviour
{
    private void OnEnable()
    {
        CurrentWeaponLoadout.instance.OnStartGameFail += OnFailureWeaponSelection;    
    }
    private void OnDisable()
    {
        CurrentWeaponLoadout.instance.OnStartGameFail -= OnFailureWeaponSelection;

    }
    public void OnFailureWeaponSelection()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
