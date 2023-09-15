using Engine;
using UnityEngine;

namespace Gizmo
{
    public class SpawnerGizmos : MonoBehaviour
    {
        [SerializeField] private bool isGizmosVisible = true;
        private void OnDrawGizmos()
        {
            if (!isGizmosVisible)
                return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 1);
        }
    }
}