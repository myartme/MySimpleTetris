using Engine.Grid;
using UnityEngine;

namespace View.Scene
{
    public class GridGenerator : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            for (var i = 0; i < GameGrid.HEIGHT; i++)
            {
                for (var j = 0; j < GameGrid.WIDTH; j++)
                {
                    var gridCell = new GameObject($"Grid[{i}][{j}]");
                    var gridCellSpriteRenderer = gridCell.AddComponent<SpriteRenderer>();
                    gridCellSpriteRenderer.sprite = _spriteRenderer.sprite;
                    gridCellSpriteRenderer.color = _spriteRenderer.color;
                    gridCell.transform.SetParent(transform);
                    gridCell.transform.localPosition = new Vector3(j, i);
                }
            }
        }
    }
}