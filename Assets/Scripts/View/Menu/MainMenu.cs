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
            if (Options.CurrentDevice == Options.Device.Desktop)
            {
                SceneManager.LoadScene("GameDesktop");
            }
            else if (Options.CurrentDevice == Options.Device.Mobile)
            {
                SceneManager.LoadScene("GameMobile");
            }
            
        }
        
        public void OpenOptions()
        {
            //SceneManager.LoadScene("MainMenu");
        }
    }
}