using System;
using Engine;
using GameFigures.Shape;
using Service;
using UnityEngine;

namespace Spawn
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Previewer previewer;
        public event Action<Tetromino> OnCurrentTetromino;
        private Tetromino _currentTetromino;

        private void Start()
        {
            SetAsCurrentTetromino(previewer.GetTetromino());
        }

        private void SetAsCurrentTetromino(Tetromino tetromino)
        {
            _currentTetromino = tetromino;
            _currentTetromino.OnChangeStatus += OnCurrentTetromino;
            _currentTetromino.Position = transform.position;
            _currentTetromino.SetAsReady();
        }

        private void TetrominoComplete(Tetromino tetromino)
        {
            if (tetromino.Status != ObjectStatus.Completed) return;
            
            _currentTetromino.OnChangeStatus -= OnCurrentTetromino;
            if (!Options.IsGameOver)
            {
                SetAsCurrentTetromino(previewer.GetTetromino());
            }
        }
        
        private void OnEnable()
        {
            OnCurrentTetromino += TetrominoComplete;
        }
        
        private void OnDisable()
        {
            OnCurrentTetromino -= TetrominoComplete;
        }
    }
}