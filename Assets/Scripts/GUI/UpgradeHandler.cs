using System;
using System.Collections.Generic;
using Playfab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GUI
{
    public class UpgradeHandler : MonoBehaviour
    {

    
        public string fieldName;
    
        [SerializeField] private GameObject upgradePrefab;
        [SerializeField] private float initialXPosition;
        [SerializeField] private int upgradeLimit;

        [SerializeField] private int currentUpgrade;
        private List<Image> _nodeImageList;
        [SerializeField] private Color upgradedColor;

        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private float priceInitial;
        [SerializeField] private float priceMultiplier;
        private int _timer;
        private bool _isUpgrading;
        private long _timestamp;

        private const int XVariation = 30;

        private void Update()
        {
            if (_timer > 0 && _isUpgrading)
            {
                buttonText.text = "Speed Up \n" + ConvertMiliSeconds(_timer);
                var newTimestamp = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                var diff = (int)(_timestamp - newTimestamp);
                _timer -= diff;
            }
            else if(_isUpgrading)
            {
                _isUpgrading = false;
                FinishUpgrade();
            }
        
        }

        void Start()
        {
            if(PlayfabManager.Instance)PlayfabManager.Instance.AddTimerResultListener(OnTimerResult);
            _nodeImageList = new List<Image>();
            GetUpgradeData();
            BuildUpgradeView();
        
        }

        private void OnTimerResult(ExecuteCloudScriptResult obj)
        {
            if (obj.FunctionResult != null)
            {
                var split = obj.FunctionResult.ToString().Split(',');
                if (split[0] == fieldName)
                {
                    SetTimer(int.Parse(split[1]));
                }
            }
        }

        private void SetTimer(int time)
        {
            _timer = time;
            _isUpgrading = true;
            _timestamp = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        private string ConvertMiliSeconds(int time)
        {
            var seconds = time / 1000;
            var minutes = seconds / 60;
            var hours = minutes / 60;
            return hours + ":" + minutes +":" + seconds;
        }


        public void DoUpgrade()
        {
            if (currentUpgrade == upgradeLimit) return;
            DoBackendValidation();
        
        }

        public void FinishUpgrade()
        {
            //TODO Finish should only be called from backend callback, this should only update the UI 
            currentUpgrade++;
            _nodeImageList[currentUpgrade].color = upgradedColor;
            priceText.text = (priceInitial * priceMultiplier * currentUpgrade).ToString();

        }

        private void DoBackendValidation()
        {
            PlayfabManager.Instance.ValidateUpgrade(fieldName, priceInitial * priceMultiplier * (currentUpgrade + 1));
        }
        private void BuildUpgradeView()
        {
            for (var i = 0; i <= upgradeLimit; i++)
            {
                var node = Instantiate(upgradePrefab, transform, true);
                var rectTransform = node.GetComponent<RectTransform>();
                rectTransform.anchoredPosition =
                    new Vector2(initialXPosition + XVariation * i, 0);
                var image = node.GetComponentInChildren<Image>();
                _nodeImageList.Add(image);
            
                if (i <= currentUpgrade)
                {
                    image.color = upgradedColor;
                }
            }

            priceText.text = (priceInitial * priceMultiplier * (currentUpgrade + 1)).ToString();
        }
        private void GetUpgradeData()
        {
            //TODO get upgrade data from backend (limit (global) and current (current user)/ initial price and price multiplier)
        }

        public void SetCurrent(int value)
        {
            currentUpgrade = value;
        }
    }
}
