using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public static class BlockColors
    {
        public static readonly Dictionary<BlockType, Color32> Colors = new()
        {
            { BlockType.I, new Color32(0, 240, 240, 255) },
            { BlockType.T, new Color32(160, 0, 240, 255) },
            { BlockType.J, new Color32(0, 0, 240, 255) },
            { BlockType.L, new Color32(240, 160, 0, 255) },
            { BlockType.Z, new Color32(240, 0, 0, 255) },
            { BlockType.O, new Color32(240, 240, 0, 255) },
            { BlockType.S, new Color32(0, 240, 0, 255) }
        };

        public static readonly Color32 Shadow = new Color32(40, 40, 40, 50);
    }
}