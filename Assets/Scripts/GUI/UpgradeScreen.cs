using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab.DataModels;
using UnityEngine;

public class UpgradeScreen : MonoBehaviour
{
    [SerializeField] private List<UpgradeHandler> upgradeHandlers;
    // Start is called before the first frame update
    void Awake()
    {
        PlayfabManager.Instance.AddUpgradeResultListener(UpgradeDataResult);
        
        PlayfabManager.Instance.GetPlayerUpgrades();
    }

    private void UpgradeDataResult(Dictionary<string, ObjectResult> obj)
    {
        foreach (var upgrade in obj)
        {
            foreach (var handler in upgradeHandlers)
            {
                if (handler.fieldName == upgrade.Key)
                {
                    handler.SetCurrent((int)(upgrade.Value.DataObject));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
