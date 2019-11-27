using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using PlayFab.Json;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.DataModels;
using System;
using UnityEngine.UI;
public class LastWeaponLoadout : MonoBehaviour
{    
    private LastGameInfo deserializedLastGameInfo;    

    [Header("References in this gameobject hierarchy")]
    [SerializeField] private GameObject weaponImageSpawnPoint;
    [SerializeField] private Text killsText;
    [Space(10)]
    [SerializeField] private GameObject[] weaponPrefabs;

    void Start()
    {
        weaponImageSpawnPoint.SetActive(false);
        PlayfabEntity.instance.OnReceivedLastWeapon += SetEnumAndImage;
        PlayfabEntity.instance.RequestLastWeaponInfo();
    }

    void OnDisable()
    {
        PlayfabEntity.instance.OnReceivedLastWeapon -= SetEnumAndImage;
    }

    void SetEnumAndImage(ObjectResult result)
    {

        deserializedLastGameInfo = JsonUtility.FromJson<LastGameInfo>(result.DataObject.ToString());
        int i = 0;
        bool lastGameWeaponExists = false;
        foreach(GameObject go in weaponPrefabs)
        {
            if(deserializedLastGameInfo.CompareToEnum(go.GetComponent<Weapon>().myType))
            {
                lastGameWeaponExists = true;
                break;
            }
            i++;
        }
        if (lastGameWeaponExists)
        {
            weaponImageSpawnPoint.SetActive(true);
            Instantiate(weaponPrefabs[i], this.weaponImageSpawnPoint.transform.position, Quaternion.identity, this.weaponImageSpawnPoint.transform);
            killsText.text = "Kills last game: " + deserializedLastGameInfo.killCount;
        }
        //WeaponEnum lastUsedWeapon = (WeaponEnum)Enum.Parse(typeof(WeaponEnum), result.DataObject.ToString());
    }
}
