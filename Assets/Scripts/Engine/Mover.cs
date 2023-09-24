using Combine;
using Service;
using Spawn;
using UnityEngine;

namespace Engine
{
    public class Mover : MonoBehaviour
    {
        public float timeToNextStep = 2f;
        public float maxSpeedDelay = 1f;
        
        private GameGrid _gameGrid;
        private bool _isAccelerating;
        private float _moveTimer;
        private float _maxSpeedTimer;
        
        private void Start()
        {
            _gameGrid = new GameGrid();
            Spawner.OnCurrentTetromino += CheckTetrominoStatus;
        }

        private void Update()
        {
            if (!_gameGrid.IssetTetromino || Time.timeScale == 0) return;
            _moveTimer += Time.deltaTime;
            if(_moveTimer > timeToNextStep)
            {
                _moveTimer = 0;
                _gameGrid.StepDown();
            }
            
            RotationByKey(KeyCode.Z, _gameGrid.AnticlockwiseAngleRotation);
            RotationByKey(KeyCode.UpArrow, _gameGrid.AnticlockwiseAngleRotation);
            
            RotationByKey(KeyCode.X, _gameGrid.ClockwiseAngleRotation);

            StepByKey(KeyCode.LeftArrow, _gameGrid.StepLeft);
            StepByKey(KeyCode.RightArrow, _gameGrid.StepRight);
            StepByKey(KeyCode.DownArrow, _gameGrid.StepDown);
        }

        private bool IsMaxSpeedReady()
        {
            if (_isAccelerating)
            {
                _maxSpeedTimer += Time.deltaTime;
            }
            
            return _maxSpeedTimer > maxSpeedDelay;
        }

        private void ResetMaxSpeedTimer()
        {
            _maxSpeedTimer = 0;
            _isAccelerating = false;
        }

        private void CheckTetrominoStatus(Tetromino tetromino)
        {
            if (tetromino.Status != ObjectStatus.Active) return;
            ResetMaxSpeedTimer();
        }

        private void StepByKey(KeyCode keyCode, GameGrid.GridAction gridAction)
        {
            if (Input.GetKeyDown(keyCode))
            {
                ResetMaxSpeedTimer();
                _isAccelerating = true;
                gridAction();
            }
            
            if (Input.GetKey(keyCode) && IsMaxSpeedReady())
            {
                gridAction();
            }
        }
        
        private void RotationByKey(KeyCode keyCode, GameGrid.GridAction gridAction)
        {
            if (Input.GetKeyDown(keyCode))
            {
                gridAction();
            }
        }
    }
}