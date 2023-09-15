using Engine;
using UnityEngine;

namespace Gizmo
{
    public class PreviewerGizmos : MonoBehaviour
    {
        [SerializeField] private bool isGizmosVisible = true;
        private void OnDrawGizmos()
        {
            if (!isGizmosVisible)
                return;
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 1);
        }
    }
}