using System.Collections.Generic;
using GameFigures;
using Service;
using UnityEngine;

namespace View.Scene
{
    public static class BlockColors
    {
        public static readonly Dictionary<BlockType, Color32> Colors = new()
        {
            { BlockType.I, new Color32(50, 180, 180, 255) },
            { BlockType.T, new Color32(100, 50, 180, 255) },
            { BlockType.J, new Color32(50, 50, 180, 255) },
            { BlockType.L, new Color32(180, 100, 50, 255) },
            { BlockType.Z, new Color32(180, 50, 50, 255) },
            { BlockType.O, new Color32(180, 180, 50, 255) },
            { BlockType.S, new Color32(50, 180, 50, 255) }
        };

        public static readonly Color32 Shadow = new Color32(100, 100, 100, 80);
    }
}