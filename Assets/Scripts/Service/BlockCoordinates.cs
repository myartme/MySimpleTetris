using System.Collections.Generic;
using UnityEngine;

namespace Service
{
    public static class BlockCoordinates
    {
        public static readonly Dictionary<BlockType, List<Vector3[]>> Coordinates = new()
        {
            {
                BlockType.I,
                new List<Vector3[]>
                {
                    new[] {
                        new Vector3(0f, 0f, 0),
                        new Vector3(0f, -1f, 0),
                        new Vector3(0f, -2f, 0),
                        new Vector3(0f, -3f, 0)
                    },
                    new[] {
                        new Vector3(1f, -2f, 0),
                        new Vector3(0f, -2f, 0),
                        new Vector3(-1f, -2f, 0),
                        new Vector3(-2f, -2f, 0)
                    },
                    new[] {
                        new Vector3(-1f, -3f, 0),
                        new Vector3(-1f, -2f, 0),
                        new Vector3(-1f, -1f, 0),
                        new Vector3(-1f, 0f, 0)
                    },
                    new[] {
                        new Vector3(-2f, -1f, 0),
                        new Vector3(-1f, -1f, 0),
                        new Vector3(0f, -1f, 0),
                        new Vector3(1f, -1f, 0)
                    }
                }
            },
            {
                BlockType.J,
                new List<Vector3[]>
                {
                    new[] {
                        new Vector3(0, 0, 0),
                        new Vector3(0, -1, 0),
                        new Vector3(0, -2, 0),
                        new Vector3(-1, -2, 0)
                    },
                    new[] {
                        new Vector3(1f, -1f, 0),
                        new Vector3(0f, -1f, 0),
                        new Vector3(-1f, -1f, 0),
                        new Vector3(-1f, 0f, 0)
                    },
                    new[] {
                        new Vector3(0f, -2f, 0),
                        new Vector3(0f, -1f, 0),
                        new Vector3(0f, 0f, 0),
                        new Vector3(1f, 0f, 0)
                    },
                    new[] {
                        new Vector3(-1f, -1f, 0),
                        new Vector3(0f, -1f, 0),
                        new Vector3(1f, -1f, 0),
                        new Vector3(1f, -2f, 0)
                    }
                }
            },
            {
                BlockType.L,
                new List<Vector3[]>
                {
                    new[] {
                        new Vector3(0, 0, 0),
                        new Vector3(0, -1, 0),
                        new Vector3(0, -2, 0),
                        new Vector3(1, -2, 0)
                    },
                    new[] {
                        new Vector3(1f, -1f, 0),
                        new Vector3(0f, -1f, 0),
                        new Vector3(-1f, -1f, 0),
                        new Vector3(-1f, -2f, 0)
                    },
                    new[] {
                        new Vector3(0f, -2f, 0),
                        new Vector3(0f, -1f, 0),
                        new Vector3(0f, 0f, 0),
                        new Vector3(-1f, 0f, 0)
                    },
                    new[] {
                        new Vector3(-1f, -1f, 0),
                        new Vector3(0f, -1f, 0),
                        new Vector3(1f, -1f, 0),
                        new Vector3(1f, 0f, 0)
                    }
                }
            },
            {
                BlockType.O,
                new List<Vector3[]>
                {
                    new[] {
                        new Vector3(0f, 0f, 0),
                        new Vector3(0f, -1f, 0),
                        new Vector3(-1f, -1f, 0),
                        new Vector3(-1f, 0f, 0)
                    },
                    new[] {
                        new Vector3(-1f, 0f, 0),
                        new Vector3(0f, 0f, 0),
                        new Vector3(0f, -1f, 0),
                        new Vector3(-1f, -1f, 0)
                    },
                    new[] {
                        new Vector3(-1f, -1f, 0),
                        new Vector3(-1f, 0f, 0),
                        new Vector3(0f, 0f, 0),
                        new Vector3(0f, -1f, 0)
                    },
                    new[] {
                        new Vector3(0f, -1f, 0),
                        new Vector3(-1f, -1f, 0),
                        new Vector3(-1f, 0f, 0),
                        new Vector3(0f, 0f, 0)
                    }
                }
            },
            {
                BlockType.S,
                new List<Vector3[]>
                {
                    new[] {
                        new Vector3(1, 0, 0),
                        new Vector3(0, 0, 0),
                        new Vector3(0, -1, 0),
                        new Vector3(-1, -1, 0)
                    },
                    new[] {
                        new Vector3(1, -2, 0),
                        new Vector3(1, -1, 0),
                        new Vector3(0, -1, 0),
                        new Vector3(0, 0, 0)
                    },
                    new[] {
                        new Vector3(-1, -2, 0),
                        new Vector3(0, -2, 0),
                        new Vector3(0, -1, 0),
                        new Vector3(1, -1, 0)
                    },
                    new[] {
                        new Vector3(-1, 0, 0),
                        new Vector3(-1, -1, 0),
                        new Vector3(0, -1, 0),
                        new Vector3(0, -2, 0)
                    }
                }
            },
            {
                BlockType.T,
                new List<Vector3[]>
                {
                    new[] {
                        new Vector3(0, 0, 0),
                        new Vector3(-1, -1, 0),
                        new Vector3(0, -1, 0),
                        new Vector3(1, -1, 0)
                    },
                    new[] {
                        new Vector3(1, -1, 0),
                        new Vector3(0, 0, 0),
                        new Vector3(0, -1, 0),
                        new Vector3(0, -2, 0)
                    },
                    new[] {
                        new Vector3(0, -2, 0),
                        new Vector3(1, -1, 0),
                        new Vector3(0, -1, 0),
                        new Vector3(-1, -1, 0)
                    },
                    new[] {
                        new Vector3(-1, -1, 0),
                        new Vector3(0, -2, 0),
                        new Vector3(0, -1, 0),
                        new Vector3(0, 0, 0)
                    }
                }
            },
            {
                BlockType.Z,
                new List<Vector3[]>
                {
                    new[] {
                        new Vector3(-1, 0, 0),
                        new Vector3(0, 0, 0),
                        new Vector3(0, -1, 0),
                        new Vector3(1, -1, 0)
                    },
                    new[] {
                        new Vector3(1, 0, 0),
                        new Vector3(1, -1, 0),
                        new Vector3(0, -1, 0),
                        new Vector3(0, -2, 0)
                    },
                    new[] {
                        new Vector3(1, -2, 0),
                        new Vector3(0, -2, 0),
                        new Vector3(0, -1, 0),
                        new Vector3(-1, -1, 0)
                    },
                    new[] {
                        new Vector3(-1, -2, 0),
                        new Vector3(-1, -1, 0),
                        new Vector3(0, -1, 0),
                        new Vector3(0, 0, 0)
                    }
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