using Combine;
using UnityEngine;

namespace Engine
{
    // Вывод содержимого Block[][] grid
    public class GridTest : MonoBehaviour
    {
        private void Start()
        {
            GameGrid.OnUpdateGrid += UpdateGrid;
        }

        private void UpdateGrid(Block[][] grid)
        {
            Transform parentTransform = transform;
            
            foreach (Transform child in parentTransform)
            {
                Destroy(child.gameObject);
            }
            
            for (var i = 0; i < grid.Length; i++)
            {
                for (var j = 0; j < grid[i].Length; j++)
                {
                    if(grid[i][j] == null) continue;
                    var gO = Instantiate(grid[i][j], gameObject.transform);
                    gO.transform.localPosition = new Vector3(j, i);
                }
            }
        }
    }
}