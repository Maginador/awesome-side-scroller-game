using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.CloudScriptModels;
using PlayFab.DataModels;
using UnityEngine;
using EntityKey = PlayFab.DataModels.EntityKey;
using ExecuteCloudScriptResult = PlayFab.ClientModels.ExecuteCloudScriptResult;

namespace Playfab
{
    
    public class PlayfabManager : MonoBehaviour
    {
        public static PlayfabManager Instance;
        private LoginResult _login;
        private Queue<Action> _playFabEventsQueue;
        private bool _lockDispatcher = false;

        private Action<Dictionary<string, ObjectResult>> _upgradeResultListener;
        private Action<ExecuteCloudScriptResult> _timerResultListener;
        private Action<Dictionary<string, ObjectResult>> _progressResultListener;
        private Action<Dictionary<string, ObjectResult>> _storeResultListener;
        private Action<Dictionary<string, ObjectResult>> _leaderboardsResultListener;

        public ExecuteCloudScriptResult result;
        public void AddUpgradeResultListener(Action<Dictionary<string, ObjectResult>> listener)
        {
            _upgradeResultListener += listener;
        } public void AddProgressResultListener(Action<Dictionary<string, ObjectResult>> listener)
        {
            _progressResultListener += listener;
        }
        public void AddTimerResultListener(Action<ExecuteCloudScriptResult> listener)
        {
            _timerResultListener += listener;
        }
        public void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }

            _playFabEventsQueue = new Queue<Action>();
        }

        private void OnCloudscriptResult(ExecuteCloudScriptResult obj)
        {

            if (obj.FunctionName == "ValidateUpgrade")
            { 
                if(obj.FunctionResult != null) StartTimerForUpgrade(obj.FunctionResult.ToString());
            }
            if (obj.FunctionName == "StartTimerForUpgrade" || obj.FunctionName == "GetTimerForUpgrade")
            {
                _timerResultListener.Invoke(obj);
            }
            Debug.Log("Cloud Script Worked");
            Debug.Log(obj.FunctionResult);
            result = obj;
        }

        private void Update()
        {
            StartCoroutine(RunPlayfabDispatcher());
        }

        private IEnumerator RunPlayfabDispatcher()
        {
        
            //TODO Wait for login
            while (true)
            {
                if (_playFabEventsQueue.Count > 0 && !_lockDispatcher)
                {
                    _lockDispatcher = true;
                    _playFabEventsQueue.Dequeue().Invoke();

                }

                yield return null;
            }
        }
        public void PlayerLogin()
        {
        
            _playFabEventsQueue.Enqueue(OnPlayerLogin);

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
            _playFabEventsQueue.Enqueue(OnGetCurrency);

        }

        public void GetPlayerProgress()
        {
            if (PlayerPrefs.HasKey("ProgressInitialize"))
            {
                var request = new GetObjectsRequest {Entity = new EntityKey {Id = _login.EntityToken.Entity.Id, Type = _login.EntityToken.Entity.Type}};
                PlayFabDataAPI.GetObjects(request, OnGetProgress, OnError);
            }
            else
            {
                SetProgressValue(0);
            }
        }

        private void SetProgressValue(int level)
        {
            var request = new SetObjectsRequest();
            var data = new Dictionary<string, object>()
            {
                {"progress", level}
                
            };
            
            var dataList = new List<SetObject>()
            {
                new SetObject()
                {
                    ObjectName = "ProgressData",
                    DataObject = data
                },
            };
            PlayFabDataAPI.SetObjects(new SetObjectsRequest()
            {
                Entity = new EntityKey {Id = _login.EntityToken.Entity.Id, Type = _login.EntityToken.Entity.Type},
                Objects = dataList,
            }, (setResult) => {
                PlayerPrefs.SetInt("ProgressInitialize", 1);
                GetPlayerProgress();
            }, OnError);    }

        private void OnGetProgress(GetObjectsResponse obj)
        {
            _progressResultListener.Invoke(obj.Objects);
        }

        public void GetPlayerUpgrades()
        {
            if (PlayerPrefs.HasKey("UpgradesInitialized"))
            {
                var request = new GetObjectsRequest {Entity = new EntityKey {Id = _login.EntityToken.Entity.Id, Type = _login.EntityToken.Entity.Type}};
                PlayFabDataAPI.GetObjects(request, OnGetUpgrades, OnError);
            }
            else
            {
                var request = new SetObjectsRequest();
                var data = new Dictionary<string, object>()
                {
                    {"HealthPoints", 0},
                    {"Energy", 0},
                    {"ShootPower", 0},
                    {"FireRate",0},
                    {"Armor",0},
                    {"Bullets",0},
                
                };
            
                var dataList = new List<SetObject>()
                {
                    new SetObject()
                    {
                        ObjectName = "UpgradeData",
                        DataObject = data
                    },
                };
                PlayFabDataAPI.SetObjects(new SetObjectsRequest()
                {
                    Entity = new EntityKey {Id = _login.EntityToken.Entity.Id, Type = _login.EntityToken.Entity.Type},
                    Objects = dataList,
                }, (setResult) => {
                    Debug.Log(setResult.ProfileVersion);
                    PlayerPrefs.SetInt("UpgradesInitialized", 1);
                    GetPlayerUpgrades();
                }, OnError);
            }
        }

        private void OnGetUpgrades(GetObjectsResponse obj)
        {
            _upgradeResultListener(obj.Objects);
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
            _login = loginResult;
        
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

        public void SetProgress(int level)
        {
            SetProgressValue(level);
        }

        public bool IsLogged()
        {
            return _login != null;
        }

        public void ValidateUpgrade(string upgrade, float price)
        {
            var request = new ExecuteCloudScriptRequest()
            {
                FunctionName = "ValidateUpgrade", FunctionParameter = new {Upgrade = upgrade, Price = price}
            };
            PlayFabClientAPI.ExecuteCloudScript(request,OnCloudscriptResult,OnError);
        }

        public void StartTimerForUpgrade(string upgrade)
        {
            var request = new ExecuteCloudScriptRequest()
            {
                FunctionName = "StartTimerForUpgrade", FunctionParameter = new {Upgrade = upgrade}
            };
            PlayFabClientAPI.ExecuteCloudScript(request,OnCloudscriptResult,OnError);
        }

    }
}
