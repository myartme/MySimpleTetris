using System;
using Combine;
using Engine;
using UnityEngine;

namespace Spawn
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Previewer previewer;
        public static event Action<Tetromino> OnCurrentTetromino;
        
        private Tetromino _currentTetromino;

        private void Start()
        {
            SetAsCurrentTetromino(previewer.GetTetromino());
        }
        
        private void SetAsCurrentTetromino(Tetromino tetromino)
        {
            _currentTetromino = tetromino;
            _currentTetromino.OnChangeStatus += TetrominoComplete;
            _currentTetromino.SetToSpawnPosition(transform.position);
            OnCurrentTetromino?.Invoke(_currentTetromino);
        }
        
        private void TetrominoComplete(Tetromino tetromino)
        {
            if (tetromino.Status != ObjectStatus.Completed) return;
            _currentTetromino.OnChangeStatus -= TetrominoComplete;
            SetAsCurrentTetromino(previewer.GetTetromino());
        }
    }
}