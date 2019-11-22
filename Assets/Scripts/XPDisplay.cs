using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;

public class XPDisplay : MonoBehaviour
{   
    public GameObject[] buttons;
    private void Start()
    {
        foreach (GameObject o in buttons)
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
             GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream

         }, OnCloudUpdatePlayerStats, 
         CloudExecuteError => 
         {
             GetComponent<Text>().text = "";
             Debug.Log("Error executing cloud script");
         });
    }

    private void OnCloudUpdatePlayerStats(ExecuteCloudScriptResult result)
    {
        // Cloud Script returns abitrary results, so you have to evaluate them one step and one parameter at a time                
        Debug.Log("The result of serialization from json is " + PlayFab.PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer).SerializeObject(result.FunctionResult));
        JsonObject jsonResult = (JsonObject)result.FunctionResult;
        CoreGameData data = JsonUtility.FromJson<CoreGameData>(((JsonObject)result.FunctionResult).ToString());        
        object messageValue;
        jsonResult.TryGetValue("messageValue", out messageValue); // note how "messageValue" directly corresponds to the JSON values set in Cloud Script        
        Debug.Log(data);
        Debug.Log(data.score);
        if (data.score >= 0)
        {
            GetComponent<Text>().text = "Player XP: " + data.score.ToString();
        }   
        foreach(GameObject o in buttons)
        {             
            o.SetActive(true);
        }
    }

}
