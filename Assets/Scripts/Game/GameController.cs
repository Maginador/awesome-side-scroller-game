using System.Collections;
using GUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        // Start is called before the first frame update
        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene(sceneBuildIndex: 1);
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
    }
}
