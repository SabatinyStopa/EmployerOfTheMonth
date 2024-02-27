using EmployerOfTheMonth.Enums;
using UnityEngine;

namespace EmployerOfTheMonth.Common
{
    public class Item : Grababble
    {
        [SerializeField] private ItemKind kind;

        private string description;
        private bool expired;
        public ItemKind Kind { get => kind; set => kind = value; }
        public string Description { get => description; set => description = value; }
        public bool Expired { get => expired; set => expired = value; }

        private void LateUpdate()
        {
            if (isBeingHolded) body.velocity = Vector3.zero;
        }

        public void OnDrawGizmosSelected()
        {
            if (meshRenderer == null) return;

            var bounds = meshRenderer.bounds;
            Gizmos.matrix = Matrix4x4.identity;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(bounds.center, bounds.extents * 2);
        }
    }
}