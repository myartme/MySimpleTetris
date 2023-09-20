using System;
using Engine;
using Service;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Combine
{
    public class Shadow : Tetromino
    {
        private Tetromino _originalTetromino;
        private Block[][] _grid;
        
        public Shadow(Tetromino originalTetromino, Sprite blocksSprite, BlockType blockType) 
            : base(blocksSprite, blockType)
        {
            _originalTetromino = originalTetromino;
            _originalTetromino.OnChangeStatus += ActiveOrDestroy;
            _originalTetromino.OnTransform += UpdatePositionAndRotation;
            GameGrid.OnUpdateGrid += UpdateGrid;
            GameObject.name = $"Shadow {GameObject.name}";
            GameObject.SetActive(false);
            Color = BlockColors.Shadow;
        }

        private void UpdateGrid(Block[][] grid)
        {
            _grid = grid;
            UpdatePositionAndRotation();
        }
        
        private void ActiveOrDestroy(Tetromino tetromino)
        {
            if (_originalTetromino.Status == ObjectStatus.Active)
            {
                SetupAsActive();
            }
            if (_originalTetromino.Status == ObjectStatus.Completed)
            {
                DestroyMe();
            }
        }

        private void SetupAsActive()
        {
            Position = _originalTetromino.Position;
            Rotation = _originalTetromino.Rotation;
            GameObject.SetActive(true);
        }
        
        private void UpdatePositionAndRotation()
        {
            if (_originalTetromino.Status != ObjectStatus.Active || _grid == null) return;
            
            var offsetY = 0;
            while (!GoDownYToBarrier(ref offsetY))
            {
                if (offsetY <= 0)
                    throw new ArgumentException();
            }
            
            Rotation = _originalTetromino.Rotation;
            Position = new Vector3(_originalTetromino.Position.x, 
                _originalTetromino.Position.y - offsetY, 
                5f);
        }

        private bool GoDownYToBarrier(ref int offsetY)
        {
            foreach (var block in _originalTetromino.Children)
            {
                var x = (int)block.Position.x;
                if (x <= 0) 
                    throw new ArgumentException();
                var y = (int)block.Position.y - offsetY;
                
                if (y <= 0 || _grid[y - 1][x - 1] != null)
                {
                    --offsetY;
                    return true;
                }
            }

            ++offsetY;
            return false;
        }

        private void DestroyMe()
        {
            _originalTetromino.OnChangeStatus -= ActiveOrDestroy;
            _originalTetromino.OnTransform -= UpdatePositionAndRotation;
            GameGrid.OnUpdateGrid -= UpdateGrid;
            Object.Destroy(GameObject);
        }
    }
}