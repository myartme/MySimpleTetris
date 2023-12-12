using System;
using GameFigures;
using GameFigures.Shape;
using Service;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawn
{
    public class Previewer : MonoBehaviour
    {
        [SerializeField] private Sprite blockSprite;
        [SerializeField] private RectTransform positionOnUI;
        
        private Tetromino _tetromino;

        public Tetromino GetTetromino()
        {
            if (_tetromino == null)
                CreateAndSetPositionTetromino();
            var tetromino = _tetromino;
            CreateAndSetPositionTetromino();
            return tetromino;
        }

        private void CreateAndSetPositionTetromino()
        {
            _tetromino = new Tetromino(blockSprite, GetGeneratedBlockType());
            _tetromino.Position = positionOnUI.position - _tetromino.Bounds.center;
            _tetromino.SetAsPreview();
        }

        private static BlockType GetGeneratedBlockType()
        {
            return (BlockType) Random.Range(0, Enum.GetValues(typeof(BlockType)).Length);
        }
    }
}