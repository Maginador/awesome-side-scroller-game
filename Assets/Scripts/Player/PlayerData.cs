using System;
using System.Collections;
using System.Collections.Generic;
using Info;
using Playfab;
using PlayFab.DataModels;
using UnityEngine;

namespace Player
{
    
    public class PlayerData : MonoBehaviour
    {
        private static UpgradeInfo _currentPlayerUpgradeInfo;
        private static Profile _currentProfile;

        public static void CreateNewPlayer()
        {
            _currentProfile = new Profile(SystemInfo.deviceUniqueIdentifier);
        }
        public static UpgradeInfo GetPlayerUpgradeData()
        {
            return _currentPlayerUpgradeInfo;
        }

        public static Profile GetPlayerProfile()
        {
            return _currentProfile;
        }

        public static void RequestUpgradeData()
        {
            PlayfabManager.Instance.AddUpgradeResultListener(UpgradeDataResult);
        
            PlayfabManager.Instance.GetPlayerUpgrades();
        }

        private static void UpgradeDataResult(Dictionary<string, ObjectResult> obj)
        {
            foreach (var upgrade in obj)
            {
                if (upgrade.Key == "UpgradeData")
                {
                    _currentPlayerUpgradeInfo = UpgradeInfo.GetUpgradeFromJson(upgrade.Value.DataObject.ToString());
                    Debug.Log(_currentPlayerUpgradeInfo);
                    
                }
            }
        }
    }
}