using System;
using UnityEngine;
using View.Screen;

namespace Engine
{
    public class Game : MonoBehaviour
    {
        public GameStart gameStartScreen;
        public GameOver gameOverScreen;
        public float timeToNextStepDecreasePerLevel = 0.11f;
        public static bool IsGameOver;

        private Mover _mover;
        private int _totalPoints;
        private int _level = 1;
        private int _linesDeleted;
        private int _tetrominoCompletedCount = -1;
        
        public static event Action<int> OnLevelChange;
        public static event Action<int> OnTotalPoints;
        public static event Action<int> OnLinesDeleted;
        public static event Action<int> OnTetrominoCompleted;
        
        private void Start()
        {
            gameStartScreen.ShowScreen(true);
            _mover = GetComponent<Mover>();
            GameGrid.OnGetTetromino += IncreaseTetrominoCompletedCount;
            GameGrid.OnDeleteLines += UpdateStatistics;
        }

        private void Update()
        {
            if (IsGameOver)
            {
                gameOverScreen.ShowScreen(true);
            }
        }

        public static void PauseGame()
        {
            Time.timeScale = 0;
        }

        public static void ResumeGame()
        {
            Time.timeScale = 1;
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
            OnTotalPoints?.Invoke(_totalPoints);
        }

        private void IncreaseDeleteLines(int linesDeleted)
        {
            _linesDeleted += linesDeleted;
            OnLinesDeleted?.Invoke(_linesDeleted);
        }

        private void IncreaseTetrominoCompletedCount()
        {
            OnTetrominoCompleted?.Invoke(++_tetrominoCompletedCount);
        }

        private void IncreaseLevel()
        {
            if (_linesDeleted / 10 <= _level) return;
            _mover.timeToNextStep -= timeToNextStepDecreasePerLevel;
            OnLevelChange?.Invoke(++_level);
        }
        
        private void ResetLevel()
        {
            _level = 1;
            OnLevelChange?.Invoke(_level);
        }
    }
}