using System;
using Engine.Grid;
using GameFigures.Combine;
using UnityEngine;
using View.Scene;
using Object = UnityEngine.Object;

namespace GameFigures.Shape
{
    public class Shadow: GeometricShape
    {
        private bool _isActive;
        
        public bool IsActive
        {
            get => _isActive;
            private set
            {
                _isActive = value;
                gameObject.SetActive(_isActive);
            }
        }
        
        public Shadow(Sprite blocksSprite, BlockType blockType) : base(blocksSprite, blockType)
        {
            Name = $"Shadow {gameObject.name}";
            Color = ColorScheme.Shadow;
            IsActive = false;
        }
        
        public void UpdatePositionAndRotation(Vector3 position, int rotation, Combine<Block> children)
        {
            var offsetY = 0;
            while (!GoDownYToBarrier(ref offsetY, children))
            {
                if (offsetY <= 0)
                    throw new ArgumentException();
            }
            
            Rotation = rotation;
            Position = new Vector3(position.x, 
                position.y - offsetY, 
                5f);
        }
        
        public void UpdatePositionAndRotation(Tetromino tetromino)
        {
            UpdatePositionAndRotation(tetromino.Position, tetromino.Rotation, tetromino.Children);
        }
        
        public void SetupAsActive(Tetromino tetromino)
        {
            UpdatePositionAndRotation(tetromino);
            IsActive = true;
        }

        private bool GoDownYToBarrier(ref int offsetY, Combine<Block> children)
        {
            foreach (var block in children)
            {
                var x = (int)block.Position.x;
                if (x <= 0) 
                    throw new ArgumentException();
                var y = (int)block.Position.y - offsetY;
                
                if (y <= 0 || !GameGrid.IsEmptyGridPosition(x, y))
                {
                    --offsetY;
                    return true;
                }
            }

            ++offsetY;
            return false;
        }

        public void DestroyMe()
        {
            Object.Destroy(gameObject);
        }
    }
}