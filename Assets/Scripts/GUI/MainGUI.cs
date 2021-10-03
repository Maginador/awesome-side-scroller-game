using System;
using Playfab;
using UnityEngine;

namespace Info
{
    public class MainGUI : MonoBehaviour
    {
        public static MainGUI Instance;
        //GUI Screens
        [SerializeField] private GameObject mainScreen;
        [SerializeField] private GameObject progressScreen;
        [SerializeField] private GameObject endlessScreen;
        [SerializeField] private GameObject storeScreen;
        [SerializeField] private GameObject upgradeScreen;

        [SerializeField] private GameObject soundDisabled;
        public enum Screens { Load, Main, Progress, Endless, Store, Upgrade}

        public Screens currentScreen;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            currentScreen = Screens.Load;
        }

        public void Update()
        {
            if (PlayfabManager.Instance == null) return;
            if (currentScreen == Screens.Load)
            {
                if (PlayfabManager.Instance.IsLogged())
                {
                    OpenMainScreen();
                }
            }
        }

        private void OpenMainScreen()
        {
            mainScreen.SetActive(true);
        }

        public void BackToMainScreen()
        {
            mainScreen.SetActive(true);
            switch (currentScreen)
            {
                case Screens.Progress:
                    progressScreen.SetActive(false);
                    break;
                case Screens.Endless:
                    endlessScreen.SetActive(false);
                    break;
                case Screens.Store:
                    storeScreen.SetActive(false);
                    break;
                case Screens.Upgrade:
                    upgradeScreen.SetActive(false);
                    break;
            }
        }
        public void OpenProgressScreen()
        {
            mainScreen.SetActive(false);
            progressScreen.SetActive(true);
            currentScreen = Screens.Progress;

        }

        public void OpenEndlessScreen()
        {
            mainScreen.SetActive(false);
            endlessScreen.SetActive(true);
            currentScreen = Screens.Endless;

        }

        public void ToggleSound()
        {
            soundDisabled.SetActive(!soundDisabled.activeSelf);
        }

        public void Quit()
        {
            //TODO: create safe quit screen (verify if user really wants to quit)
            Application.Quit();
        }

        public void OpenStore()
        {
            mainScreen.SetActive(false);
            storeScreen.SetActive(true);
            currentScreen = Screens.Store;
        }

        public void OpenUpgrade()
        {
            mainScreen.SetActive(false);
            upgradeScreen.SetActive(true);
            currentScreen = Screens.Upgrade;
        }

        public static void OpenMenu(Screens screen)
        {
            switch (screen)
            {
                case Screens.Progress :
                    Instance.OpenProgressScreen();
                    break;
                case Screens.Endless:
                    Instance.OpenEndlessScreen();
                    break;
                case Screens.Store:
                    Instance.OpenStore();
                    break;
                case Screens.Upgrade:
                    Instance.OpenUpgrade();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(screen), screen, null);
            }
        }
    }
}
