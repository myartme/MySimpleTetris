using System;
using Combine;
using Engine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawn
{
    public class Previewer : MonoBehaviour
    {
        [SerializeField] private Sprite blockSprite;
        private Tetromino _tetromino;
        
        private void Start()
        {
            Tetromino.OnChangeStatus += CheckTetrominoStatus;
            CreateAndSetPositionTetromino();
        }

        private void CreateAndSetPositionTetromino()
        {
            _tetromino = new Tetromino(blockSprite, GetGeneratedBlockType());
            _tetromino.SetToPreviewPosition(transform.position);
        }

        private void CheckTetrominoStatus(Tetromino tetromino)
        {
            if (tetromino.Equals(_tetromino) && tetromino.Status == ObjectStatus.Active)
            {
                CreateAndSetPositionTetromino();
            }
        }

        private static BlockType GetGeneratedBlockType()
        {
            return (BlockType) Random.Range(0, Enum.GetValues(typeof(BlockType)).Length);
        }
    }
}