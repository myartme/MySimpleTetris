using System;
using System.Linq;
using Combine;
using Spawn;
using UnityEngine;
using View.Screen;

namespace Engine
{
    public class GameGrid
    {
        public const int WIDTH = 10;
        public const int HEIGHT = 23;
        public const int VISIBLE_HEIGHT = 20;
        
        public static event Action<Block[][]> OnUpdateGrid;
        public static event Action<int> OnDeleteLines;
        public static event Action OnGetTetromino;

        private Block[][] _grid = new Block[HEIGHT][];
        
        public bool IssetTetromino => _activeTetromino != null;
        public delegate void GridAction();
        
        private Tetromino _activeTetromino;
        private Shadow _shadow;
        private bool _isTetrominoMakeComplete;
        private bool _isTetrominoStepDown;
        
        public GameGrid()
        {
            for (var i = 0; i < _grid.Length; i++)
            {
                _grid[i] = new Block[WIDTH];
            }
            Spawner.OnCurrentTetromino += GetActiveSpawnerTetromino;
        }

        private bool IsPossibleMovement(Tetromino tetromino, Vector3 position, Quaternion rotation)
        {
            foreach (var child in tetromino.Children)
            {
                var childPosWithRotation = rotation * child.transform.localPosition;
                var childPosition = Vector3Int.RoundToInt(childPosWithRotation + position);
                var x = childPosition.x;
                var y = childPosition.y;
                
                //if (_isTetrominoStepDown && (y <= 0 || (y < HEIGHT && !IsEmptyGridPosition(x, y))))
                if (_isTetrominoStepDown && (y <= 0 || !IsEmptyGridPosition(x, y)))
                {
                    _isTetrominoMakeComplete = true;
                }
                
                if (IsOutOfGrid(x, y) || (!IsOutOfGrid(x, y) && !IsEmptyGridPosition(x, y)))
                {
                    return false;
                }
            }
            
            return true;
        }
        
        public void StepLeft()
        {
            Step(new Vector3(_activeTetromino.Position.x - 1, _activeTetromino.Position.y));
        }
        
        public void StepRight()
        {
            Step(new Vector3(_activeTetromino.Position.x + 1, _activeTetromino.Position.y));
        }
        
        public void StepDown()
        {
            _isTetrominoStepDown = true;
            Step(new Vector3(_activeTetromino.Position.x, _activeTetromino.Position.y - 1));
            _isTetrominoStepDown = false;
        }
        
        public void ClockwiseAngleRotation()
        {
            Step(_activeTetromino.AngleRotation == 0 ? 3 : _activeTetromino.AngleRotation - 1);
        }
        
        public void AnticlockwiseAngleRotation()
        {
            Step(_activeTetromino.AngleRotation == 3 ? 0 : _activeTetromino.AngleRotation + 1);
        }
        
        private void Step(Vector3 position, int angleRotation)
        {
            if (IsPossibleMovement(_activeTetromino, position, Tetromino.GetRightAngleZRotation(angleRotation)))
            {
                if (_activeTetromino.Position != position)
                    _activeTetromino.Position = position;
                
                if (_activeTetromino.AngleRotation != angleRotation)
                    _activeTetromino.AngleRotation = angleRotation;
            }
            
            if (!_isTetrominoMakeComplete) return;
            
            UpdateTetrominoBlockPositionsOnGrid();
            CheckGameOver();
            DeleteLines();
            OnUpdateGrid?.Invoke(_grid);
            _activeTetromino.SetAsCompleted();
            _isTetrominoMakeComplete = false;
        }

        private void UpdateTetrominoBlockPositionsOnGrid()
        {
            _activeTetromino.Children.ForEach(item =>
            {
                _grid[(int)item.Position.y - 1][(int)item.Position.x - 1] = item;
            });
        }

        private void DeleteLines()
        {
            var linesDeleted = 0;
            for (var i = 0; i < _grid.Length; i++)
            {
                var rowBlockCount = _grid[i].Count(item => item != null);

                if (rowBlockCount == _grid[i].Length)
                {
                    foreach (var block in _grid[i])
                    {
                        block.DestroyMe();
                    }

                    linesDeleted++;
                }
                else if (linesDeleted > 0)
                {
                    for (var j = 0; j < _grid[i].Length; j++)
                    {
                        if (_grid[i][j])
                            _grid[i][j].DropMeDown(linesDeleted);
                        
                        _grid[i - linesDeleted][j] = _grid[i][j];
                    }
                }
            }
            
            OnDeleteLines?.Invoke(linesDeleted);
        }

        private void CheckGameOver()
        {
            foreach (var blocks in _grid[VISIBLE_HEIGHT])
            {
                if (blocks)
                    Game.IsGameOver = true;
            }
        }
        
        private void Step(Vector3 position) 
            => Step(position, _activeTetromino.AngleRotation);

        private void Step(int angleRotation)
            => Step(_activeTetromino.Position, angleRotation);
        
        private bool IsEmptyGridPosition(int x, int y)
        {
            return _grid[y - 1][x - 1] == null;
        }
        
        private bool IsOutOfGrid(int x, int y)
        {
            return x is <= 0 or > WIDTH || y is <= 0 or > HEIGHT;
        }
        
        private void GetActiveSpawnerTetromino(Tetromino tetromino)
        {
            _activeTetromino = tetromino;
            OnGetTetromino?.Invoke();
            OnUpdateGrid?.Invoke(_grid);
        }
    }
}