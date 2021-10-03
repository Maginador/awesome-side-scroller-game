using System;
using System.Collections;
using System.Collections.Generic;
using GUI;
using Player;
using Playfab;
using PlayFab.DataModels;
using UnityEngine;


namespace Info
{
    public class UpgradeScreen : MonoBehaviour
    {
        [SerializeField] private List<UpgradeHandler> upgradeHandlers;
        // Start is called before the first frame update
        void Awake()
        {
            if (PlayfabManager.Instance == null) return;

           var upgradeInfo  = PlayerData.GetPlayerUpgradeData();
           foreach (var handler in upgradeHandlers)
           {
               if (handler.fieldName == "HealthPoints") handler.SetCurrent(upgradeInfo.HealthPoints);
               if (handler.fieldName == "Energy") handler.SetCurrent(upgradeInfo.Energy);
               if (handler.fieldName == "ShootPower") handler.SetCurrent(upgradeInfo.ShootPower);
               if (handler.fieldName == "FireRate") handler.SetCurrent(upgradeInfo.FireRate);
               if (handler.fieldName == "Armor") handler.SetCurrent(upgradeInfo.Armor);
               if (handler.fieldName == "Bullets") handler.SetCurrent(upgradeInfo.Bullets);
           }
        }
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}