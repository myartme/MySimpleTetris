using Sounds;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Engine
{
    public class Game : MonoBehaviour
    {
        [SerializeField] public GameObject TetrominoParent;
        [SerializeField] public SoundsEffects SoundsEffects;
        [SerializeField] private GUIManager _guiManager;
        
        public static Transform BoardTransform { get; private set; }
        public static bool IsGameOver;

        private void Awake()
        {
            BoardTransform = TetrominoParent.transform;
        }

        private void Update()
        {
            if (IsGameOver)
            {
                _guiManager.ShowGameOverScreen();
            }
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
            IsGameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}