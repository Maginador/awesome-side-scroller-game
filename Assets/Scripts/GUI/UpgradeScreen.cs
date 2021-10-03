using System;
using System.Collections;
using System.Collections.Generic;
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
            PlayfabManager.Instance.AddUpgradeResultListener(UpgradeDataResult);
        
            PlayfabManager.Instance.GetPlayerUpgrades();
        }

        private void UpgradeDataResult(Dictionary<string, ObjectResult> obj)
        {
            foreach (var upgrade in obj)
            {
                if (upgrade.Key == "UpgradeData")
                {
                    var upgradeInfo = UpgradeInfo.GetUpgradeFromJson(upgrade.Value.DataObject.ToString());
                    Debug.Log(upgradeInfo);
                    foreach (var handler in upgradeHandlers)
                    {
                        if (handler.fieldName == "healthpoints") handler.SetCurrent(upgradeInfo.healthpoints);
                        if (handler.fieldName == "energy") handler.SetCurrent(upgradeInfo.energy);
                        if (handler.fieldName == "shootpower") handler.SetCurrent(upgradeInfo.shootpower);
                        if (handler.fieldName == "firerate") handler.SetCurrent(upgradeInfo.firerate);
                        if (handler.fieldName == "armor") handler.SetCurrent(upgradeInfo.armor);
                        if (handler.fieldName == "bullets") handler.SetCurrent(upgradeInfo.bullets);
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}