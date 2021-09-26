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
    public static PlayfabManager Instance;
    private Queue<Action> _playfabEventsQueue;
    private bool _lockDispatcher = false;

    public void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

        _playfabEventsQueue = new Queue<Action>();
    }

    private void Update()
    {
        StartCoroutine(RunPlayfabDispatcher());
    }

    private IEnumerator RunPlayfabDispatcher()
    {
        while (true)
        {
            if (_playfabEventsQueue.Count > 0 && !_lockDispatcher)
            {
                _lockDispatcher = true;
                _playfabEventsQueue.Dequeue().Invoke();

            }

            yield return null;
        }
    }
    public void PlayerLogin()
    {
        
        _playfabEventsQueue.Enqueue(OnPlayerLogin);

    }

    private void OnPlayerLogin()
    {
        //TODO Check if the player already exists 
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

    public void GetCurrency()
    {
        _playfabEventsQueue.Enqueue(OnGetCurrency);

    }

    private void OnGetCurrency()
    {
        var request = new GetUserInventoryRequest();
        
        PlayFabClientAPI.GetUserInventory(request, GetCurrencyResult, OnError);
    }
    private void GetCurrencyResult(GetUserInventoryResult obj)
    {
        foreach (var currency in obj.VirtualCurrency)
        {
            Game.GameController.Instance.SetCurrency(currency.Key, currency.Value);
        }

        _lockDispatcher = false;
    }

    private void OnCustomLoginSuccess(LoginResult loginResult)
    {
        Debug.Log("Logged as " + loginResult.PlayFabId);

        _lockDispatcher = false;

        
    }
    private void OnGoogleLogin(LoginResult loginResult)
    {
        //TODO:: Develop Google Play login
        _lockDispatcher = false;

    }

    private void OnError(PlayFabError playFabError)
    {
        Debug.LogError("Request got error : " + playFabError.Error);
        _lockDispatcher = false;

    }
}
