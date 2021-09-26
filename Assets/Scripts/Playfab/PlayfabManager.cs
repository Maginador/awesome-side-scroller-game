using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{


    public Text debug;
    public static PlayfabManager Instance;

    public void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }   
    }

    public void PlayerLogin()
    {
        Player.PlayerData.CreateNewPlayer();
        var request = new LoginWithCustomIDRequest
        {
            CustomId = Profile.GetID(),
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnCustomLoginSuccess, OnError);

    }

    public void LoginWithGooglePlay()
    {
        //TODO:: Develop Google Play login
        //var request = new LoginWithGoogleAccountRequest();
        //PlayFabClientAPI.LoginWithGoogleAccount(request, OnGoogleLogin, OnError);
    }

    private static void OnCustomLoginSuccess(LoginResult loginResult)
    {
        Debug.Log("Logged as " + loginResult.PlayFabId);
        Instance.debug.text = "Session Ticket : " + loginResult.SessionTicket;
        Instance.debug.text += "\nSession EntityToken : " + loginResult.EntityToken.EntityToken;
        Instance.debug.text += "\nSession Id : " + loginResult.PlayFabId;
    }
    private static void OnGoogleLogin(LoginResult loginResult)
    {
        //TODO:: Develop Google Play login
    }

    private static void OnError(PlayFabError playFabError)
    {
        Debug.LogError("Request got error : " + playFabError.Error);
    }
}
