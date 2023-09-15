using System.Collections.Generic;
using UnityEngine;

public class BlockCoordinates
{
    private readonly Dictionary<BlockType, Vector3[]> _coordinates = new();

    public BlockCoordinates()
    {
        InitializeCoordinates();
    }
    
    /*private void InitializeCoordinates()
    {
        _coordinates[BlockType.I] = new[]
        {
            new Vector3(0f, 1f, 0),
            new Vector3(0f, 0f, 0),
            new Vector3(0f, -1f, 0),
            new Vector3(0f, -2f, 0)
        };

        _coordinates[BlockType.J] = new[]
        {
            new Vector3(-0.5f, 0.5f, 0),
            new Vector3(-0.5f, -0.5f, 0),
            new Vector3(-0.5f, -1.5f, 0),
            new Vector3(-1.5f, -1.5f, 0)
        };
        
        _coordinates[BlockType.L] = new[]
        {
            new Vector3(-0.5f, 0.5f, 0),
            new Vector3(-0.5f, -0.5f, 0),
            new Vector3(-0.5f, -1.5f, 0),
            new Vector3(0.5f, -1.5f, 0)
        };
        
        _coordinates[BlockType.O] = new[]
        {
            new Vector3(-1f, 0f, 0),
            new Vector3(0f, 0f, 0),
            new Vector3(0f, -1f, 0),
            new Vector3(-1f, -1f, 0)
        };
        
        _coordinates[BlockType.S] = new[]
        {
            new Vector3(-1.5f, -0.5f, 0),
            new Vector3(-0.5f, -0.5f, 0),
            new Vector3(-0.5f, 0.5f, 0),
            new Vector3(0.5f, 0.5f, 0)
        };
        
        _coordinates[BlockType.T] = new[]
        {
            new Vector3(-1.5f, -0.5f, 0),
            new Vector3(-0.5f, -0.5f, 0),
            new Vector3(0.5f, -0.5f, 0),
            new Vector3(-0.5f, -1.5f, 0)
        };
        
        _coordinates[BlockType.Z] = new[]
        {
            new Vector3(-1.5f, 0.5f, 0),
            new Vector3(-0.5f, 0.5f, 0),
            new Vector3(-0.5f, -0.5f, 0),
            new Vector3(0.5f, -0.5f, 0)
        };

    }*/

    private void InitializeCoordinates()
    {
        _coordinates[BlockType.I] = new[]
        {
            new Vector3(0.5f, 1.5f, 0),
            new Vector3(0.5f, 0.5f, 0),
            new Vector3(0.5f, -0.5f, 0),
            new Vector3(0.5f, -1.5f, 0)
        };

        _coordinates[BlockType.J] = new[]
        {
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, -1, 0),
            new Vector3(-1, -1, 0)
        };
        
        _coordinates[BlockType.L] = new[]
        {
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, -1, 0),
            new Vector3(1, -1, 0)
        };
        
        _coordinates[BlockType.O] = new[]
        {
            new Vector3(0.5f, 0.5f, 0),
            new Vector3(0.5f, -0.5f, 0),
            new Vector3(-0.5f, -0.5f, 0),
            new Vector3(-0.5f, 0.5f, 0)
        };
        
        _coordinates[BlockType.S] = new[]
        {
            new Vector3(1, 1, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 0),
            new Vector3(-1, 0, 0)
        };
        
        _coordinates[BlockType.T] = new[]
        {
            new Vector3(-1, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(0, -1, 0)
        };
        
        _coordinates[BlockType.Z] = new[]
        {
            new Vector3(-1, 1, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 0),
            new Vector3(1, 0, 0)
        };

    }

    public Vector3[] GetCoordinates(BlockType blockType)
    {
        if (_coordinates.ContainsKey(blockType))
        {
            return _coordinates[blockType];
        }

        Debug.LogWarning($"No coordinates found for the block {blockType}.");
        return null;
    }
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