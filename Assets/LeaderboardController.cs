using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;
using System.Text;
public class LeaderboardController : MonoBehaviour
{
    [SerializeField] private GameObject sampleTextObject;
    [SerializeField] private GameObject toggleButton;
    [SerializeField] private GameObject loadingText;
    private StringBuilder sb = new StringBuilder();
    
    void Start()
    {
        loadingText.SetActive(true);
        toggleButton.SetActive(false);
        GetLeaderBoard();
    }

    private void GetLeaderBoard()
    {
        GetLeaderboardRequest request = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "TotalExp", MaxResultsCount = 10 };

        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardSuccess, OnLeaderboardFailure);
    }
    private void OnLeaderboardSuccess(GetLeaderboardResult result)
    {
        toggleButton.SetActive(true);
        loadingText.SetActive(false);
        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {            
            InstantiatePlayerText((player.Position+1).ToString(), player.PlayFabId, player.StatValue.ToString());
        }
    }
    private void OnLeaderboardFailure(PlayFabError error)
    {
        loadingText.SetActive(false);
        toggleButton.SetActive(true);
        Debug.Log(error.GenerateErrorReport());
    }

    private void InstantiatePlayerText(string positionInLeaderboard, string playerName, string playerExp)
    {
        Text playerLeaderboardText = Instantiate(sampleTextObject, this.transform).GetComponent<Text>();

        Debug.Log(PlayFabLogin.thisPlayerId);
        Debug.Log("LB " + playerName);
        sb.Append(positionInLeaderboard + ". ");
        if(playerName == PlayFabLogin.thisPlayerId)
        {
            playerLeaderboardText.color = Color.green;
            sb.Append("(You) ");
        }
        sb.Append(playerName + ": ");
        sb.Append(playerExp + " XP");                        
        
        
        playerLeaderboardText.text = sb.ToString();
        sb.Clear();
    }
}
