using System;
using UnityEngine;

namespace GUI
{
    public class MainGUI : MonoBehaviour
    {
        //GUI Screens
        [SerializeField] private GameObject mainScreen;
        [SerializeField] private GameObject progressScreen;
        [SerializeField] private GameObject endlessScreen;
        [SerializeField] private GameObject storeScreen;
        [SerializeField] private GameObject upgradeScreen;

        [SerializeField] private GameObject soundDisabled;
        public enum Screens { Main, Progress, Endless, Store, Upgrade}

        public Screens currentScreen;

        public void Awake()
        {
            currentScreen = Screens.Main;
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
        }

        public void OpenEndlessScreen()
        {
            mainScreen.SetActive(false);
            endlessScreen.SetActive(true);
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
        }

        public void OpenUpgrade()
        {
            mainScreen.SetActive(false);
            upgradeScreen.SetActive(true);
        }
    }
}
