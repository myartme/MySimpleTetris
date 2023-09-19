using System;
using Engine;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Combine
{
    public class Shadow : Tetromino
    {
        private Tetromino _originalTetromino;
        private Block[][] _grid;
        
        public Shadow(Tetromino originalTetromino, Sprite blocksSprite, BlockType blockType) : base(blocksSprite, blockType)
        {
            _originalTetromino = originalTetromino;
            OnChangeStatus += DisableShadow;
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
        
        private void DisableShadow(Tetromino tetromino)
        {
            if (tetromino == _originalTetromino && _originalTetromino.Status == ObjectStatus.Active)
            {
                SetupPositionAndRotation();
                GameObject.SetActive(true);
            }
            if (_originalTetromino.Status == ObjectStatus.Completed)
            {
                Object.Destroy(GameObject);
            }
        }

        private void SetupPositionAndRotation()
        {
            Position = _originalTetromino.Position;
            Rotation = _originalTetromino.Rotation;
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
            
            var positionX = _originalTetromino.Position.x;
            var positionY = _originalTetromino.Position.y - offsetY;
            var positionZ = 5f;
            Rotation = _originalTetromino.Rotation;
            Position = new Vector3(positionX, positionY, positionZ);
            
            //UpdateChildrenPosition();
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
    }
}