using System;
using Combine;
using Engine;
using UnityEngine;

namespace Spawn
{
    public class Spawner : MonoBehaviour
    {
        public event Action<Tetromino> OnCurrentTetromino;
        
        private Tetromino _currentTetromino;
        private Tetromino _nextTetromino;

        private void Start()
        {
            Tetromino.OnChangeStatus += GetTetromino;
        }

        private void GetTetromino(Tetromino tetromino)
        {
            if (tetromino.Status == ObjectStatus.Preview)
            {
                if (_currentTetromino == null)
                {
                    SetAsCurrentTetromino(tetromino);
                } else if (_nextTetromino == null)
                {
                    _nextTetromino = tetromino;
                }
            } else if (tetromino.Status == ObjectStatus.Completed)
            {
                SetAsCurrentTetromino(_nextTetromino);
            }
        }

        private void SetAsCurrentTetromino(Tetromino tetromino)
        {
            _currentTetromino = tetromino;
            _nextTetromino = null;
            _currentTetromino.SetToSpawnPosition(transform.position);
            OnCurrentTetromino?.Invoke(tetromino);
        }
    }
}