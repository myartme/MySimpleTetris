using System;
using System.Collections.Generic;
using System.Linq;
using GameFigures;
using Random = UnityEngine.Random;

namespace Spawn
{
    public class TetrominoGenerator
    {
        private Queue<int> _queue = new();
        private readonly int _max = Enum.GetValues(typeof(BlockType)).Length;

        public BlockType Next()
        {
            QueueUpdate();
            
            return (BlockType)_queue.Last();
        }

        private void QueueUpdate()
        {
            var randomValue = GetInt();
            var missing = -1;
            
            if (_queue.Count > 1)
            {
                if (_queue.Count == 10)
                {
                    missing = CheckQueueMissingValues();
                    _queue.Dequeue();
                }
                
                randomValue = missing != -1 ? missing : GetInt(_queue.Last());
            }
            
            _queue.Enqueue(randomValue);
        }

        private int CheckQueueMissingValues()
        {
            for (var i = 0; i < _max; i++)
            {
                if (!_queue.Contains(i))
                {
                    return i;
                }
            }

            return -1;
        }

        private int GetInt(int forbiddenValue)
        {
            int value;
            do
            {
                value = GetInt();
            } while (value == forbiddenValue);

            return value;
        }

        private int GetInt()
        {
            return Random.Range(0, _max);
        }
    }
}