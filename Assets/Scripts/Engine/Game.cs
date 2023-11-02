using UnityEngine;
using UnityEngine.SceneManagement;
using View.Screen;
using View.TextField;

namespace Engine
{
    public class Game : MonoBehaviour
    {
        public float timeStepDecreasePerLevel = 0.5f;
        public static bool IsGameOver;
        public static Transform BoardTransform { get; private set; }

        private Mover _mover;
        private int _totalPoints;
        private int _level = 1;
        private int _linesDeleted;
        private int _tetrominoCompletedCount;
        
        private void Start()
        {
            _mover = GetComponent<Mover>();
            GameGrid.OnGetTetromino += IncreaseTetrominoCompletedCount;
            GameGrid.OnDeleteLines += UpdateStatistics;
            BoardTransform = gameObject.transform;
            if (!IsGameOver)
            {
                GameStart.ClassInstance.ShowScreen(true);
            }
            InitializeUI();
        }

        private void Update()
        {
            if (IsGameOver)
            {
                GameOver.ClassInstance.ShowScreen(true);
                Score.ClassInstance.UpdateCountText(_totalPoints);
            }
        }

        private void UpdateStatistics(int linesDeleted)
        {
            var points = linesDeleted switch
            {
                1 => 100,
                2 => 300,
                3 => 600,
                4 => 1300,
                _ => 0
            };

            IncreaseDeleteLines(linesDeleted);
            IncreaseTotalPoints(points);
            IncreaseLevel();
        }
        
        private void IncreaseTotalPoints(int totalPoints)
        {
            _totalPoints += totalPoints;
            Score.ClassInstance.UpdateCountText(_totalPoints);
        }

        private void IncreaseDeleteLines(int linesDeleted)
        {
            _linesDeleted += linesDeleted;
            LinesDeleted.ClassInstance.UpdateCountText(_linesDeleted);
        }

        private void IncreaseTetrominoCompletedCount()
        {
            TetrominoCount.ClassInstance.UpdateCountText(++_tetrominoCompletedCount);
        }

        private void IncreaseLevel()
        {
            if (_linesDeleted / 10 < _level) return;
            _mover.timeToNextStep -= timeStepDecreasePerLevel;
            Level.ClassInstance.UpdateCountText(++_level);
        }

        private void InitializeUI()
        {
            Score.ClassInstance.UpdateCountText(_totalPoints);
            LinesDeleted.ClassInstance.UpdateCountText(_linesDeleted);
            TetrominoCount.ClassInstance.UpdateCountText(_tetrominoCompletedCount);
            Level.ClassInstance.UpdateCountText(_level);
            IsGameOver = false;
        }
        
        public void StartGame()
        {
            GameStart.ClassInstance.ShowScreen(false);
        }

        public void RestartGame()
        {
            GameOver.ClassInstance.ShowScreen(false);
            SceneManager.LoadScene(0);
        }
        
        public static void PauseGame()
        {
            Time.timeScale = 0;
        }

        public static void ResumeGame()
        {
            Time.timeScale = 1;
        }

        private void OnDestroy()
        {
            GameGrid.OnGetTetromino -= IncreaseTetrominoCompletedCount;
            GameGrid.OnDeleteLines -= UpdateStatistics;
        }
    }
}