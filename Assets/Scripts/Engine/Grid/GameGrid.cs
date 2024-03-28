using System;
using System.Collections.Generic;
using System.Linq;
using GameFigures.Combine;
using GameFigures.Shape;
using Service;
using UnityEngine;

namespace Engine.Grid
{
    public class GameGrid : MonoBehaviour
    {
        private Tetromino Tetromino => _tetrominoOrder.CurrentTetromino;
        
        public const int WIDTH = 10;
        public const int HEIGHT = 23;
        public static event Action<int> OnDeleteLinesCount;

        private static Block[][] _grid = new Block[HEIGHT][];
        public delegate void GridAction();
        
        private Game _game;
        private TetrominoOrder _tetrominoOrder;
        private Shadow _shadow;
        private bool _isTetrominoStepDown;
        private List<int> _linesDeleted = new();

        private void Awake()
        {
            _game = GetComponent<Game>();
            _tetrominoOrder = GetComponent<TetrominoOrder>();
            for (var i = 0; i < _grid.Length; i++)
            {
                _grid[i] = new Block[WIDTH];
            }
        }

        private void Update()
        {
            if (_linesDeleted.Count <= 0 || GetDestroyedLines() != _linesDeleted.Count) return;

            var linesDeleted = 0;
            for (var i = 0; i < _grid.Length; i++)
            {
                var allCompleted = _grid[i].All(item => item != null);
                if (allCompleted)
                {
                    foreach (var block in _grid[i])
                    {
                        block.DestroyMe();
                    }

                    linesDeleted++;
                }
                else
                {
                    for (var j = 0; j < _grid[i].Length; j++)
                    {
                        if (_grid[i][j])
                            _grid[i][j].DropMeDown(linesDeleted);
                        
                        _grid[i - linesDeleted][j] = _grid[i][j];
                    }
                }
            }

            OnDeleteLinesCount?.Invoke(_linesDeleted.Count);
            _linesDeleted.Clear();
            Tetromino.SetAsCompleted();
        }

        private bool IsPossibleMovement(Tetromino tetromino, Vector3 position, int rotation)
        {
            var localPositions = tetromino.Children.GetChildrenLocalPositionsByRotation(rotation);
            foreach (var blockLocalPos in localPositions)
            {
                var childPosition = Vector3Int.RoundToInt(position + blockLocalPos);
                var x = childPosition.x;
                var y = childPosition.y;
                
                if (_isTetrominoStepDown && (y <= 0 || !IsEmptyGridPosition(x, y)))
                {
                    Tetromino.SetAsMakeComplete();
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
            Step(new Vector3(Tetromino.Position.x - 1, Tetromino.Position.y));
        }
        
        public void StepRight()
        {
            Step(new Vector3(Tetromino.Position.x + 1, Tetromino.Position.y));
        }
        
        public void StepDown()
        {
            _isTetrominoStepDown = true;
            Step(new Vector3(Tetromino.Position.x, Tetromino.Position.y - 1));
            _isTetrominoStepDown = false;
        }
        
        public void ClockwiseAngleRotation()
        {
            Step(Tetromino.PrevRotation);
        }
        
        public void AnticlockwiseAngleRotation()
        {
            Step(Tetromino.NextRotation);
        }
        
        private void Step(Vector3 position, int rotation)
        {
            if (IsPossibleMovement(Tetromino, position, rotation))
            {
                if (Tetromino.Position != position)
                    Tetromino.Position = position;
                
                if (Tetromino.Rotation != rotation)
                    Tetromino.Rotation = rotation;
            }
            
            if (Tetromino.Status != ObjectStatus.MakeComplete) return;
            
            _game.SoundsEffects.PlayComplete();
            UpdateTetrominoBlockPositionsOnGrid();
            DeleteLines();
        }

        private void UpdateTetrominoBlockPositionsOnGrid()
        {
            Tetromino.Children.ForEach(item =>
            {
                _grid[(int)item.Position.y - 1][(int)item.Position.x - 1] = item;
            });
        }

        private void DeleteLines()
        {
            for (var i = 0; i < _grid.Length; i++)
            {
                var allCompleted = _grid[i].All(item => item != null);
                
                if (allCompleted)
                {
                    for (int j = 4, k = 5; j >= 0 && k < _grid[i].Length; j--, k++)
                    {
                        _grid[i][j].VanishMe(4-j);
                        _grid[i][k].VanishMe(4-j);
                    }
                    _linesDeleted.Add(i);
                }
            }

            if (_linesDeleted.Count == 0)
            {
                Tetromino.SetAsCompleted();
            }
            else
            {
                _game.SoundsEffects.PlayDeleteLine();
            }
        }

        private int GetDestroyedLines()
        {
            var counter = 0;
            foreach (var line in _linesDeleted)
            {
                for (int i = 0, count = 0; i < _grid[line].Length; i++)
                {
                    if (_grid[line][i].Status == ObjectStatus.Destroyed)
                    {
                        count++;
                    }
                        
                    if (count == _grid[line].Length)
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }
        
        private void Step(Vector3 position) 
            => Step(position, Tetromino.Rotation);

        private void Step(int rotation)
            => Step(Tetromino.Position, rotation);
        
        public static bool IsEmptyGridPosition(int x, int y)
        {
            return _grid[y - 1][x - 1] is null;
        }
        
        private bool IsOutOfGrid(int x, int y)
        {
            return x is <= 0 or > WIDTH || y is <= 0 or > HEIGHT;
        }
    }
}