using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;
public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public GameObject gridChild;
    public GameObject toggleButton;
    public GameObject loadingText;    
    private const string catalogueName = "Weapons";
    private const string storeName = "GunShop";
    [SerializeField] private Text purchaseResultDisplay;
    [SerializeField] private Text playerCashDisplay;
    [SerializeField] private GameObject[] allWeapons;
    private Weapon[] allWeaponsScript;
    private List<int> weaponPrices = new List<int>();
    private int? indexOfWeapon;
    int currentMoney;
    void OnEnable()
    {
        if(instance == null)
        {
            loadingText.SetActive(true);
            toggleButton.SetActive(false);
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        GetWeaponScripts();
        SortArray();
        GetStore();
        GetPlayerCash();
    }

    private void GetWeaponScripts()
    {
        //create another array to avoid calling GetComponent multiple times on the same game objects in the sort array
        allWeaponsScript = new Weapon[allWeapons.Length];
        for(int i = 0; i < allWeapons.Length; i++)
        {
            allWeaponsScript[i] = allWeapons[i].GetComponent<Weapon>();
        }
    }

    private void SortArray()
    {
        //Function dumb checks to make sure list is organized numerically. I need this for using indexes to instantiate weapons later.
        int i = 0;
        if(i < allWeapons.Length - 1)
        { 
            foreach(GameObject obj in allWeapons)
            { 
                while(i < allWeapons.Length - 1 && 
                        allWeaponsScript[i + 1].weaponCardReference.ID < allWeaponsScript[i].weaponCardReference.ID)
                {
                    //Swap game objects and weapon references
                    GameObject temp = allWeapons[i];
                    Weapon weaponTemp = allWeaponsScript[i];
                    allWeapons[i] = allWeapons[i + 1];
                    allWeaponsScript[i] = allWeaponsScript[i + 1];
                    allWeapons[i + 1] = temp;
                    allWeaponsScript[i + 1] = weaponTemp;
                    i++;
                }
            }

            i--; //Set index back by one for next check (I want to secondary check the last while loop to see if we prematurely stopped looping)
            if(i > 0 && i < allWeapons.Length && allWeaponsScript[i+1].weaponCardReference.ID == allWeaponsScript[i].weaponCardReference.ID)
            {
                Debug.LogError("Duplicates in weapon array detected, ID should not be the same");
            }
        }
    }
    private void GetStore()
    {        
        GetStoreItemsRequest request = new GetStoreItemsRequest
        {
            CatalogVersion = catalogueName,
            StoreId = storeName            
        };
        PlayFabClientAPI.GetStoreItems(request, OnGetShopSuccess, OnGetShopFail);
    }

    private void OnGetShopFail(PlayFabError error)
    {
        loadingText.SetActive(false);
        toggleButton.SetActive(true);
        Debug.LogWarning("Store failure");
        Debug.LogWarning(error.ErrorDetails);
    }

    private void OnGetShopSuccess(GetStoreItemsResult result)
    {        
        GameObject temp;
        Weapon tempWeapon;
        foreach(StoreItem item in result.Store)
        {
            indexOfWeapon = WeaponIds.CompareIdsAndGetIndex(item.ItemId);
            if(indexOfWeapon != null)
            {
                temp = Instantiate(allWeapons[(int)indexOfWeapon], gridChild.transform);
                tempWeapon = temp.GetComponent<Weapon>();
                weaponPrices.Add((int)item.VirtualCurrencyPrices["DC"]);
                tempWeapon.weaponPriceText.text = "Doominic-Cash: " + item.VirtualCurrencyPrices["DC"].ToString();
                tempWeapon.headText.gameObject.SetActive(true);
                tempWeapon.headText.text = item.ItemId;
                temp.GetComponent<Button>().enabled = true;
                indexOfWeapon = null;
            }
            else
            {
                Debug.LogError("The weapon catalogue has more gameobjects than what is available here, make sure you've added all gameobjects");
            }
        }
        toggleButton.SetActive(true);
        loadingText.SetActive(false);
    }

    public void RequestPurchase(int weaponID)
    {
        PrematureTextEdit(weaponID);
        //Gets called when a weapon button is clicked
        PurchaseItemRequest request = new PurchaseItemRequest
        {
            ItemId = WeaponIds.allIds[weaponID],
            Price = weaponPrices[weaponID],
            VirtualCurrency = "DC"
        };
        PlayFabClientAPI.PurchaseItem(request, resultCallback =>
        {
            Debug.Log("Success");
            StartCoroutine(DisplayTextWithColor("Purchase Successful", Color.green));            
        },
            error =>
        { 
            Debug.Log("Error");
            StartCoroutine(DisplayTextWithColor("Purchase Failed. Do you have enough money?", Color.red));
            ResetTextOnFailure();
        });
        
    }

    private IEnumerator DisplayTextWithColor(string s, Color col)
    {
        purchaseResultDisplay.text = s;
        purchaseResultDisplay.color = col;
        // Get cash only after the transaction result is in. Need to make sure that the value does not get changed while we are requesting
        GetPlayerCash(); 
        yield return new WaitForSeconds(1f);
        purchaseResultDisplay.text = "";
    }
    private void PrematureTextEdit(int weaponId)
    {
        playerCashDisplay.text = "Doominic-Cash: " + (currentMoney - weaponPrices[weaponId]).ToString();
    }
    private void ResetTextOnFailure()
    {
        playerCashDisplay.text = "Doominic-Cash: " + currentMoney.ToString();
    }
    private void GetPlayerCash()
    {
        GetUserInventoryRequest request = new GetUserInventoryRequest();

        PlayFabClientAPI.GetUserInventory(request, resultCallback =>
        {
            currentMoney = resultCallback.VirtualCurrency["DC"];
            playerCashDisplay.text = "Doominic-Cash: " + currentMoney.ToString();
        },
        errorCallback =>
        {
            Debug.Log("Error Message: " + errorCallback.ErrorDetails);
        });
    }
}
