using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameFigures.Combine;
using GameFigures.Shape;
using Service;
using UnityEngine;

namespace Engine
{
    public class GameGrid : MonoBehaviour
    {
        public const int WIDTH = 10;
        public const int HEIGHT = 23;
        public const int VISIBLE_HEIGHT = 20;
        public static event Action<int> OnDeleteLinesCount;
        public static event Action OnGetTetromino;

        private static Block[][] _grid = new Block[HEIGHT][];
        
        public bool IssetTetromino => _activeTetromino != null;
        public delegate void GridAction();
        
        private Tetromino _activeTetromino;
        private Game _game;
        private Shadow _shadow;
        private bool _isTetrominoMakeComplete;
        private bool _isTetrominoStepDown;

        private void Awake()
        {
            _game = GetComponent<Game>();
            for (var i = 0; i < _grid.Length; i++)
            {
                _grid[i] = new Block[WIDTH];
            }
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
            Step(_activeTetromino.PrevRotation);
        }
        
        public void AnticlockwiseAngleRotation()
        {
            Step(_activeTetromino.NextRotation);
        }
        
        private void Step(Vector3 position, int rotation)
        {
            if (IsPossibleMovement(_activeTetromino, position, rotation))
            {
                if (_activeTetromino.Position != position)
                    _activeTetromino.Position = position;
                
                if (_activeTetromino.Rotation != rotation)
                    _activeTetromino.Rotation = rotation;
            }
            
            if (!_isTetrominoMakeComplete) return;
            
            UpdateTetrominoBlockPositionsOnGrid();
            _activeTetromino.SetAsMakeComplete();
            CheckGameOver();
            StartCoroutine(DeleteLines());
        }

        private void UpdateTetrominoBlockPositionsOnGrid()
        {
            _activeTetromino.Children.ForEach(item =>
            {
                _grid[(int)item.Position.y - 1][(int)item.Position.x - 1] = item;
            });
        }

        private IEnumerator DeleteLines()
        {
            var linesDeleted = 0;
            var deleteLineCoroutines = new List<Coroutine>();
            
            foreach (var cell in _grid)
            {
                var rowBlockCount = cell.Count(item => item != null);

                if (rowBlockCount != cell.Length) continue;
                
                linesDeleted++;
                var coroutine = StartCoroutine(AnimationDeleteLine(cell));
                deleteLineCoroutines.Add(coroutine);
            }
            
            if (linesDeleted > 0)
            {
                linesDeleted = 0;
                yield return StartCoroutine(WaitForCoroutines(deleteLineCoroutines));
                deleteLineCoroutines.Clear();
                
                for (var i = 0; i < _grid.Length; i++)
                {
                    var rowBlockCount = _grid[i].Count(item => item != null);
                    
                    if (rowBlockCount == _grid[i].Length)
                    {
                        foreach (var block in _grid[i])
                        {
                            block.DestroyMe();
                            yield return null;
                        }

                        linesDeleted++;
                    }
                    else
                    {
                        DeleteLineFromGrid(i, linesDeleted);
                    }
                }
            }
            
            yield return new WaitUntil(() => deleteLineCoroutines.Count == 0);
            OnDeleteLinesCount?.Invoke(linesDeleted);
            _activeTetromino.SetAsCompleted();
            _isTetrominoMakeComplete = false;
        }
        
        private IEnumerator WaitForCoroutines(List<Coroutine> coroutines)
        {
            return coroutines.GetEnumerator();
        }

        private IEnumerator AnimationDeleteLine(Block[] blocks)
        {
            for (int j = 4, k = 5; j >= 0 && k < blocks.Length; j--, k++)
            {
                blocks[j].VanishMe();
                blocks[k].VanishMe();
                yield return new WaitForSeconds(0.05f);
            }
        }

        private void DeleteLineFromGrid(int line, int offset)
        {
            var gridLine = _grid[line];
            
            for (var i = 0; i < gridLine.Length; i++)
            {
                if (gridLine[i])
                    gridLine[i].DropMeDown(offset);
                        
                _grid[line - offset][i] = gridLine[i];
            }
        }

        private void CheckGameOver()
        {
            foreach (var blocks in _grid[VISIBLE_HEIGHT])
            {
                if (blocks)
                {
                    Options.IsGameOver = true;
                }
            }
        }
        
        private void Step(Vector3 position) 
            => Step(position, _activeTetromino.Rotation);

        private void Step(int rotation)
            => Step(_activeTetromino.Position, rotation);
        
        public static bool IsEmptyGridPosition(int x, int y)
        {
            return _grid[y - 1][x - 1] == null;
        }
        
        private bool IsOutOfGrid(int x, int y)
        {
            return x is <= 0 or > WIDTH || y is <= 0 or > HEIGHT;
        }
        
        private void GetActiveSpawnerTetromino(Tetromino tetromino)
        {
            if (tetromino.Status == ObjectStatus.Active)
            {
                _activeTetromino = tetromino;
                OnGetTetromino?.Invoke();
            }
        }
        
        private void OnEnable()
        {
            _game.spawner.OnCurrentTetromino += GetActiveSpawnerTetromino;
        }
        
        private void OnDisable()
        {
            _game.spawner.OnCurrentTetromino -= GetActiveSpawnerTetromino;
        }
    }
}