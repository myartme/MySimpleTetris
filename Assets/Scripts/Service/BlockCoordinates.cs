using System.Collections.Generic;
using UnityEngine;

namespace Service
{
    public static class BlockCoordinates
    {
        public static readonly Dictionary<BlockType, Vector3[]> Coordinates = new()
        {
            {
                BlockType.I,
                new[] {
                    new Vector3(0.5f, 1.5f, 0),
                    new Vector3(0.5f, 0.5f, 0),
                    new Vector3(0.5f, -0.5f, 0),
                    new Vector3(0.5f, -1.5f, 0)
                }
            },
            {
                BlockType.J,
                new[] {
                    new Vector3(0, 1, 0),
                    new Vector3(0, 0, 0),
                    new Vector3(0, -1, 0),
                    new Vector3(-1, -1, 0)
                }
            },
            {
                BlockType.L,
                new[] {
                    new Vector3(0, 1, 0),
                    new Vector3(0, 0, 0),
                    new Vector3(0, -1, 0),
                    new Vector3(1, -1, 0)
                }
            },
            {
                BlockType.O,
                new[] {
                    new Vector3(0.5f, 0.5f, 0),
                    new Vector3(0.5f, -0.5f, 0),
                    new Vector3(-0.5f, -0.5f, 0),
                    new Vector3(-0.5f, 0.5f, 0)
                }
            },
            {
                BlockType.S,
                new[] {
                    new Vector3(1, 1, 0),
                    new Vector3(0, 1, 0),
                    new Vector3(0, 0, 0),
                    new Vector3(-1, 0, 0)
                }
            },
            {
                BlockType.T,
                new[] {
                    new Vector3(-1, 0, 0),
                    new Vector3(0, 0, 0),
                    new Vector3(1, 0, 0),
                    new Vector3(0, -1, 0)
                }
            },
            {
                BlockType.Z,
                new[] {
                    new Vector3(-1, 1, 0),
                    new Vector3(0, 1, 0),
                    new Vector3(0, 0, 0),
                    new Vector3(1, 0, 0)
                }
            }
        };
    }

    public enum BlockType
    {
        I = 0,
        J,
        L,
        O,
        S,
        T,
        Z
    }
}