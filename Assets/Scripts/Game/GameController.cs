using System;
using System.Collections;
using GUI;
using Playfab;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        private const string SoftCurrencyKey = "SC";
        private const string HardCurrencyKey = "HC";
        private Action _currencyChangedListener;
        private int _softCurrency, _hardCurrency;

        // Start is called before the first frame update
        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene(sceneBuildIndex: 1);
            PlayfabManager.Instance.PlayerLogin();
            GetCurrency();
        }

        public void AddCurrencyChangedListener(Action action)
        {
            _currencyChangedListener += action;
        }
        public void DeepLinkMainGUI(MainGUI.Screens screen)
        {
            StartCoroutine(Deeplink(screen));
        }

        private IEnumerator Deeplink(MainGUI.Screens screen)
        {
            while (SceneManager.GetActiveScene().buildIndex != 1)
            {
                yield return null;
            }

            MainGUI.OpenMenu(screen);
            
        }

        public void GetCurrency()
        {
            PlayfabManager.Instance.GetCurrency();
        }

        public int GetCurrency(string currency)
        {
            switch (currency)
            {
                case SoftCurrencyKey:
                    return _softCurrency;
                case HardCurrencyKey:
                    return _hardCurrency;
            }

            return 0;
        }

        public void SetCurrency(string currencyKey, int currencyValue)
        {
            Debug.Log("Currency Key : " + currencyKey);
            if (currencyKey == SoftCurrencyKey)
            {
                _softCurrency = currencyValue;
            }else if (currencyKey == HardCurrencyKey)
            {
                _hardCurrency = currencyValue;
            }
            _currencyChangedListener.Invoke();
        }

        public void RemoveCurrencyChangedListener(Action currencyUpdate)
        {
            _currencyChangedListener -= currencyUpdate;
        }
    }
}
