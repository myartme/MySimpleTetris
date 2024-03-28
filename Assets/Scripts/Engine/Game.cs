using Save;
using Save.Data;
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
        public static bool IsGameOver;
        public static IStorable Store;
        public static SaveData SaveData;
        
        private GUIManager _guiManager;
        private ColorTheme _colorTheme;

        private void Awake()
        {
            Store = new JsonSaveSystem();
            SaveData = new SaveData();
            _guiManager = GetComponent<GUIManager>();
            BoardTransform = TetrominoParent.transform;
            if (!Store.IsExists)
            {
                Store.Create();
            }
            else
            {
                SaveData = Store.Load();
            }

            InitSaves();
        }

        private void Update()
        {
            if (IsGameOver)
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
            IsGameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void InitSaves()
        {
            _colorTheme = GetComponent<ColorTheme>();
            _colorTheme.InitializeSaveData();
            _colorTheme.LoadSaveData();
        }
    }
}