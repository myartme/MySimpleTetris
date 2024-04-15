using System.Collections;
using Engine;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using View.GUI.Screen;

namespace View.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GUIManager _guiManager;
        [SerializeField] private Localization _localization;
        private static bool _gameIsRestarted;

        private IEnumerator Start()
        {
            yield return GameStartScreen.ClassInstance;
            yield return LocalizationSettings.InitializationOperation;
            _guiManager.ShowMainMenu();
            yield return null;
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
    }
}