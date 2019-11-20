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
    const string catalogueName = "Weapons";
    const string storeName = "GunShop";
    [SerializeField] GameObject[] allWeapons;
    List<int> weaponPrices = new List<int>();
    public GameObject gridChild;
    int? indexOfWeapon;
    void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        SortArray();
        GetStore();
    }

    void SortArray()
    {
        //Function dumb checks to make sure list is organized numerically. I need this for using indexes to instantiate weapons later.
        int i = 0;
        if(i < allWeapons.Length - 1)
        { 
            foreach(GameObject obj in allWeapons)
            { 
                while(i < allWeapons.Length - 1 && 
                        allWeapons[i + 1].GetComponent<Weapon>().weaponCardReference.ID < allWeapons[i].GetComponent<Weapon>().weaponCardReference.ID)

                {
                    GameObject temp = allWeapons[i];
                    allWeapons[i] = allWeapons[i + 1];
                    allWeapons[i + 1] = temp;
                    i++;
                }
            }

            i--; //Set index back by one for next check (I want to secondary check the last while loop to see if we prematurely stopped looping)
            if(i > 0 && i < allWeapons.Length && allWeapons[i+1].GetComponent<Weapon>().weaponCardReference.ID == allWeapons[i].GetComponent<Weapon>().weaponCardReference.ID)
            {
                Debug.LogError("Duplicates in weapon array detected");
            }
        }
    }
    void GetStore()
    {        
        GetStoreItemsRequest request = new GetStoreItemsRequest
        {
            CatalogVersion = catalogueName,
            StoreId = storeName            
        };
        PlayFabClientAPI.GetStoreItems(request, OnShopResultSuccess, OnShopResultFail);
    }
    void OnShopResultSuccess(GetStoreItemsResult result)
    {
        GameObject temp;
        foreach(StoreItem item in result.Store)
        {
            indexOfWeapon = WeaponIds.CompareIdsAndGetIndex(item.ItemId);
            if(indexOfWeapon != null)
            {
                temp = Instantiate(allWeapons[(int)indexOfWeapon], gridChild.transform);
                weaponPrices.Add((int)item.VirtualCurrencyPrices["DC"]);
                temp.GetComponent<Weapon>().progressBar.text = "$ " + item.VirtualCurrencyPrices["DC"].ToString();
                temp.GetComponent<Button>().enabled = true;
                indexOfWeapon = null;
            }
            else
            {
                Debug.LogError("The weapon catalogue has more gameobjects than what is available here, make sure you've added all gameobjects");
            }
        }
    }

    public void RequestPurchase(int weaponID)
    {
        PurchaseItemRequest request = new PurchaseItemRequest
        {
            ItemId = WeaponIds.allIds[weaponID],
            Price = weaponPrices[weaponID],
            VirtualCurrency = "DC"
        };
        PlayFabClientAPI.PurchaseItem(request, resultCallback => Debug.Log("Success?"), error => Debug.Log("Errorr?"));
        //StartPurchaseRequest request = new StartPurchaseRequest
        //{
        //    CatalogVersion = catalogueName,
        //    StoreId = storeName,
        //    Items = new List<ItemPurchaseRequest> 
        //    { 
        //        new ItemPurchaseRequest 
        //        { 
        //            ItemId = WeaponIds.allIds[weaponID], //string from the collection 
        //            Quantity = 1, 
        //        } 
        //    }
        //};
        //
        //PlayFabClientAPI.StartPurchase(request,  DefinePaymentCurrency, OnShopResultFail);
    }
    void DefinePaymentCurrency(StartPurchaseResult result)
    {
        var request = new PayForPurchaseRequest
        {
            OrderId = result.OrderId, // orderId comes from StartPurchase above
            Currency = "DC" // User defines which currency they wish to use to pay for this purchase (all items must have a defined/non-zero cost in this currency)
        };
        PlayFabClientAPI.PayForPurchase(request, FinishPurchase, LogFailure);

    }
    
    void FinishPurchase(PayForPurchaseResult result )//string orderId)
    {
        
        var request = new ConfirmPurchaseRequest { OrderId = result.OrderId };
        PlayFabClientAPI.ConfirmPurchase(request, LogSuccess, LogFailure);
    }
    void LogSuccess(PayForPurchaseResult result)
    {
        Debug.Log("Purchase paid");
    }
    void LogSuccess(ConfirmPurchaseResult result)
    {
        Debug.Log("Purchase complete");
    }
    void LogFailure(PlayFabError error)
    {
        Debug.Log("Purchase Failed");
        Debug.Log(error.ErrorMessage);
        Debug.Log(error.Error);
    }
    void OnShopResultFail(PlayFabError error)
    {
        Debug.LogWarning("Store failure");
        Debug.LogWarning(error.ErrorDetails);
    }
}
