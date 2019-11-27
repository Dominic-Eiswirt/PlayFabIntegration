using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
public class LastWeaponLoadoutUsingUserData : MonoBehaviour
{
    public GameObject[] weaponPrefabs;
    public GameObject weaponSpawnLocation;
    public Text killsText;
    
    void Start()
    {
        PlayfabUserData.instance.OnDataReceived += BuildView;
        PlayfabUserData.instance.OnPlayerIsNewAndHasNoInfo += DisableSelf;
        PlayfabUserData.instance.GetUserData();
    }
    private void OnDisable()
    {
        PlayfabUserData.instance.OnDataReceived -= BuildView;
        PlayfabUserData.instance.OnPlayerIsNewAndHasNoInfo -= DisableSelf;
    }

    void BuildView(Dictionary<string, string> dict)
    {
        killsText.text = "Kills last game: " + dict["Kills"];
        WeaponEnum dataWeapon = (WeaponEnum)Enum.Parse(typeof(WeaponEnum), dict["Weapon"]);

        foreach(GameObject go in weaponPrefabs)
        {
            if (go.GetComponent<Weapon>().myType == dataWeapon)
            {
                Instantiate(go, weaponSpawnLocation.transform.position, Quaternion.identity, weaponSpawnLocation.transform);
                break;
            }
        }
    }
    void DisableSelf()
    {
        this.gameObject.SetActive(false);
    }

}
