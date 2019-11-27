using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;

public class PlayfabLobbyXPDisplay : MonoBehaviour
{   
    [Header("Disables/Enables content when the state is ready loaded or destroyed")]
    public GameObject[] contentToDisable;
    

    private void Start()
    {
        foreach (GameObject o in contentToDisable)
        {
            o.SetActive(false);
        }
        GetComponent<Text>().text = "";
        GetCloudExp();
    }

    private void GetCloudExp()
    { 
         PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
         {
             FunctionName = "updatePlayerExp",
             FunctionParameter = new { Kills = UICenter.instance.data.score },
             GeneratePlayStreamEvent = true, 

         }, OnCloudUpdatePlayerStats, 
         CloudExecuteError => 
         {
             GetComponent<Text>().text = "";
             Debug.LogError("Error executing cloud script");
            foreach (GameObject o in contentToDisable)
            {
                o.SetActive(true);
            }
             transform.GetChild(0).GetComponent<Text>().text = "Error.";
             transform.GetChild(0).GetComponent<Text>().color = Color.red;
         });        
    }

    private void OnCloudUpdatePlayerStats(ExecuteCloudScriptResult result)
    {
        // Cloud Script returns abitrary results, so you have to evaluate them one step and one parameter at a time                
        Debug.Log("The result of serialization from json is " + PlayFab.PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer).SerializeObject(result.FunctionResult));
        JsonObject jsonResult = (JsonObject)result.FunctionResult;
        CoreGameData data = JsonUtility.FromJson<CoreGameData>(((JsonObject)result.FunctionResult).ToString());        
        object messageValue;
        jsonResult.TryGetValue("messageValue", out messageValue);        
        if (data.score >= 0)
        {
            GetComponent<Text>().text = "Player XP: " + data.score.ToString();
        }   
        foreach(GameObject o in contentToDisable)
        {             
            o.SetActive(true);
        }
        transform.GetChild(0).gameObject.SetActive(false);
    }

}
