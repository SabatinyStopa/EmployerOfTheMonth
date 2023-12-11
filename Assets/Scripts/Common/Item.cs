using UnityEngine;

namespace EmployerOfTheMonth.Common
{
    public class Item : MonoBehaviour
    {
        public void OnDrawGizmosSelected()
        {
            var meshRenderer = GetComponent<Renderer>();

            if (meshRenderer == null) return;
            
            var bounds = meshRenderer.bounds;
            Gizmos.matrix = Matrix4x4.identity;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(bounds.center, bounds.extents * 2);
        }
    }
}