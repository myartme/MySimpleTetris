using Engine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace View.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GUIManager guiManager;
        [SerializeField] private Localization localization;
        [SerializeField] private GameObject desktopCanvasScreens;
        [SerializeField] private GameObject mobileCanvasScreens;
        private static bool _gameIsRestarted;
        private string _scene;

        private void Awake()
        {
            if (ScreenDetector.CurrentDevice == ScreenDetector.Device.Desktop)
            {
                _scene = "Desktop";
                mobileCanvasScreens.SetActive(false);
            }
            else if (ScreenDetector.CurrentDevice == ScreenDetector.Device.Mobile)
            {
                _scene = "Mobile";
                desktopCanvasScreens.SetActive(false);
            }
        }

        public void StartGame()
        {
            SceneManager.LoadScene(_scene);
        }
    }
}