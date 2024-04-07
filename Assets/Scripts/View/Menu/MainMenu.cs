using Engine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace View.Menu
{
    [RequireComponent(typeof(GUIManager))]
    public class MainMenu : MonoBehaviour
    {
        private GUIManager _guiManager;
        private static bool _gameIsRestarted;

        private void Awake()
        {
            _guiManager = GetComponent<GUIManager>();
        }

        private void Start()
        {
            _guiManager.ShowMainMenu();
        }

        public void StartGame()
        {
            if (ScreenDetector.CurrentDevice == ScreenDetector.Device.Desktop)
            {
                SceneManager.LoadScene("Desktop");
            }
            else if (ScreenDetector.CurrentDevice == ScreenDetector.Device.Mobile)
            {
                SceneManager.LoadScene("Mobile");
            }
            
        }
        
        public void OpenOptions()
        {
            //SceneManager.LoadScene("MainMenu");
        }
    }
}