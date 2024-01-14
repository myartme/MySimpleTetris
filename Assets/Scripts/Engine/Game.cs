using Sounds;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Engine
{
    [RequireComponent(typeof(GUIManager))]
    public class Game : MonoBehaviour
    {
        [SerializeField] public GameObject TetrominoParent;
        [SerializeField] public SoundsEffects SoundsEffects;
        public static Transform BoardTransform { get; private set; }
        
        private GUIManager _guiManager;

        private void Awake()
        {
            _guiManager = GetComponent<GUIManager>();
            BoardTransform = TetrominoParent.transform;
        }

        private void Update()
        {
            if (Options.IsGameOver)
            {
                _guiManager.ShowGameOverScreen();
            }
        }
        
        public void ShowOption()
        {
            _guiManager.ShowOptionScreen();
        }
        
        public void HideOption()
        {
            _guiManager.HideOptionScreen();
        }
        
        public void PauseGame()
        {
            _guiManager.ShowPauseScreen();
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            _guiManager.HidePauseScreen();
            Time.timeScale = 1;
        }

        public void RestartGame()
        {
            Destroy(TetrominoParent);
            _guiManager.HideGameOverScreen();
            Options.IsGameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}