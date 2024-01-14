using UnityEngine;

namespace Engine
{
    public class Logic : MonoBehaviour
    {
        [SerializeField] public float timeToNextStep = 2f;
        [SerializeField] public float timeStepDecreasePerLevel = 0.5f;
        
        private GUIManager _guiManager;
        private int _totalPoints;
        private int _level = 1;
        private int _linesDeleted;
        private int _tetrominoCompletedCount = -1;
        
        private void Awake()
        {
            _guiManager = GetComponent<GUIManager>();
        }

        private void Start()
        {
            _guiManager.UpdateLevel(_level);
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
            _guiManager.UpdateScore(_totalPoints);
        }

        private void IncreaseDeleteLines(int linesDeleted)
        {
            _linesDeleted += linesDeleted;
            _guiManager.UpdateLinesDeleted(_linesDeleted);
        }

        private void IncreaseTetrominoCompletedCount()
        {
            _guiManager.UpdateTetrominoCount(++_tetrominoCompletedCount);
        }

        private void IncreaseLevel()
        {
            if ((float)_linesDeleted / 10 <= _level) return;

            UpdateTimeToNextStep();
            _guiManager.UpdateLevel(++_level);
        }

        private void UpdateTimeToNextStep()
        {
            timeToNextStep -= timeStepDecreasePerLevel;
            
            if (timeToNextStep <= 0)
            {
                timeToNextStep = 0.1f;
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