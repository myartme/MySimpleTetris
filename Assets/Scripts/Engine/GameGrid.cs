using System;
using System.Collections.Generic;
using System.Linq;
using Combine;
using Spawn;
using UnityEngine;

namespace Engine
{
    public class GameGrid
    {
        public const int WIDTH = 10;
        public const int HEIGHT = 20;
        public event Action<int> OnDeleteLines;
        public static event Action<Block[][]> OnUpdateGrid;

        private Block[][] _grid = new Block[HEIGHT][];
        
        public bool IssetTetromino => _activeTetromino != null;
        public delegate void GridAction();
        
        private Spawner _spawner;
        private Tetromino _activeTetromino;
        private Shadow _shadow;
        private bool _isTetrominoMakeComplete;
        private bool _isTetrominoStepDown;
        private EqualityComparer<Block> _comparer = EqualityComparer<Block>.Default;
        
        public GameGrid(Spawner spawner)
        {
            for (int i = 0; i < _grid.Length; i++)
            {
                _grid[i] = new Block[WIDTH];
            }
            _spawner = spawner;
            _spawner.OnCurrentTetromino += GetActiveSpawnerTetromino;
        }

        private bool IsPossibleMovement(Tetromino tetromino, Vector3 position, Quaternion rotation)
        {
            foreach (var child in tetromino.Children)
            {
                var childPosWithRotation = rotation * child.transform.localPosition;
                var childPosition = Vector3Int.RoundToInt(childPosWithRotation + position);
                var x = childPosition.x;
                var y = childPosition.y;
                
                if (_isTetrominoStepDown && (
                        childPosition.y == 0 || (
                            childPosition.y < HEIGHT && !IsEmptyGridPosition(x, y))))
                {
                    _isTetrominoMakeComplete = true;
                }
                
                if (IsOutOfGrid(x, y) || (
                        !IsOutOfGrid(x, y) && !IsEmptyGridPosition(x, y))
                    )
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
        
        public void StepUp()
        {
            Step(new Vector3(_activeTetromino.Position.x, _activeTetromino.Position.y + 1));
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
            CheckLinesForDeleting();
            _activeTetromino.SetAsCompleted();
            _isTetrominoMakeComplete = false;
        }

        private void UpdateTetrominoBlockPositionsOnGrid()
        {
            _activeTetromino.Children.ForEach(item =>
            {
                _grid[(int)item.Position.y - 1][(int)item.Position.x - 1] = item;
            });
            OnUpdateGrid?.Invoke(_grid);
        }

        private void CheckLinesForDeleting()
        {
            var linesNeedDelete = new List<int>();
            var index = 0;
            for (var i = 0; i < _grid.Length; i++)
            {
                var count = 0;
                for (var j = 0; j < _grid[i].Length; j++)
                {
                    if (!_comparer.Equals(_grid[i][j], default))
                    {
                        count++;
                    }
                }

                if (count == _grid[i].Length && index < 4)
                {
                    linesNeedDelete.Add(i);
                    index++;
                }
            }
            if(index > 0)
                DeleteLines(linesNeedDelete);
        }

        private void DeleteLines(List<int> lines)
        {
            var offset = 0;
            for (var i = 0; i < _grid.Length; i++)
            {
                var countLines = lines.Count(line => i == line);
                
                if (countLines > 0)
                {
                    offset++;
                    foreach (var block in _grid[i])
                    {
                        block.DestroyMe();
                    }
                }
                else
                {
                    foreach (var block in _grid[i])
                    {
                        if (block)
                            block.DropMeDown(offset);
                    }
                    _grid[i - offset] = _grid[i];
                }
            }
            
            OnDeleteLines?.Invoke(lines.Count);
        }
        
        private void Step(Vector3 position) 
            => Step(position, _activeTetromino.AngleRotation);

        private void Step(int angleRotation)
            => Step(_activeTetromino.Position, angleRotation);
        
        private bool IsEmptyGridPosition(int x, int y)
        {
            return _comparer.Equals(_grid[y - 1][x - 1], default);
        }
        
        private bool IsOutOfGrid(int x, int y)
        {
            return x is <= 0 or > WIDTH || y is <= 0 or > HEIGHT;
        }
        
        private void GetActiveSpawnerTetromino(Tetromino tetromino)
        {
            _activeTetromino = tetromino;
            OnUpdateGrid?.Invoke(_grid);
        }
    }
}