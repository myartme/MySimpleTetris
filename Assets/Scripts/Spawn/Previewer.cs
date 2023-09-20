﻿using System;
using Combine;
using Service;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawn
{
    public class Previewer : MonoBehaviour
    {
        [SerializeField] private Sprite blockSprite;
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
            _tetromino.SetToPreviewPosition(transform.position);
        }

        private static BlockType GetGeneratedBlockType()
        {
            return (BlockType) Random.Range(0, Enum.GetValues(typeof(BlockType)).Length);
        }
    }
}