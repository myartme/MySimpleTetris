using Engine.Grid;
using UnityEngine;

namespace Engine
{
    public class Logic : MonoBehaviour
    {
        [SerializeField] public float TimeToNextStep = 2f;
        [SerializeField] public float TimeStepDecreasePerLevel = 0.5f;
        [SerializeField] private GUIManager guiManager;
        
        public int TotalPoints => _totalPoints;
        public int Level => _level;
        public int LinesDeleted => _linesDeleted;
        public int TetrominoCompleted => _tetrominoCompletedCount;
        
        private int _totalPoints;
        private int _level = 1;
        private int _linesDeleted;
        private int _tetrominoCompletedCount = -1;

        private void Start()
        {
            guiManager.UpdateLevel(_level);
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
            guiManager.UpdateScore(_totalPoints);
        }

        private void IncreaseDeleteLines(int linesDeleted)
        {
            _linesDeleted += linesDeleted;
            guiManager.UpdateLinesDeleted(_linesDeleted);
        }

        private void IncreaseTetrominoCompletedCount()
        {
            guiManager.UpdateTetrominoCount(++_tetrominoCompletedCount);
        }

        private void IncreaseLevel()
        {
            if ((float)_linesDeleted / 10 <= _level) return;

            UpdateTimeToNextStep();
            guiManager.UpdateLevel(++_level);
        }

        private void UpdateTimeToNextStep()
        {
            TimeToNextStep -= TimeStepDecreasePerLevel;
            
            if (TimeToNextStep <= 0)
            {
                TimeToNextStep = 0.1f;
            }
        }
        
        private void OnEnable()
        {
            TetrominoOrder.OnGetTetromino += IncreaseTetrominoCompletedCount;
            GameGrid.OnDeleteLinesCount += UpdateStatistics;
        }
        
        private void OnDisable()
        {
            TetrominoOrder.OnGetTetromino -= IncreaseTetrominoCompletedCount;
            GameGrid.OnDeleteLinesCount -= UpdateStatistics;
        }
    }
}