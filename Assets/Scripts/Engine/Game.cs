using Sounds;
using Spawn;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Engine
{
    [RequireComponent(typeof(GUIManager))]
    public class Game : MonoBehaviour
    {
        [SerializeField] public Spawner spawner;
        [SerializeField] private Transform tetrominoParent;
        [SerializeField] public SoundsEffects soundsEffects;
        public static Transform BoardTransform { get; private set; }
        
        private GUIManager _guiManager;

        private void Awake()
        {
            _guiManager = GetComponent<GUIManager>();
            BoardTransform = tetrominoParent;
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
            Destroy(tetrominoParent);
            _guiManager.HideGameOverScreen();
            Options.IsGameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}