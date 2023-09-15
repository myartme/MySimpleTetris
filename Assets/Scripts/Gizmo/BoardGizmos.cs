using Engine;
using UnityEngine;

namespace Gizmo
{
    public class BoardGizmos : MonoBehaviour
    {
        [SerializeField] private bool isGizmosVisible = true;
        
        private void OnDrawGizmos()
        {
            if (!isGizmosVisible)
                return;

            Gizmos.color = Color.black;
            var gridSize = new Vector3(GameGrid.WIDTH, GameGrid.HEIGHT);
            Gizmos.DrawWireCube((Vector3.zero + gridSize) / 2 + transform.position, gridSize);
        }
    }
}