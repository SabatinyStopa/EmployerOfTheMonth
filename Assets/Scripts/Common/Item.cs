using EmployerOfTheMonth.Interfaces;
using EmployerOfTheMonth.Player;
using EmployerOfTheMonth.Enums;
using UnityEngine;

namespace EmployerOfTheMonth.Common
{
    public class Item : MonoBehaviour, IInteract
    {
        [SerializeField] private ItemKind kind;

        private string description;
        private bool expired;
        private Renderer meshRenderer;
        private Rigidbody body;
        private bool isBeingHolded;
        public ItemKind Kind { get => kind; set => kind = value; }
        public string Description { get => description; set => description = value; }
        public bool Expired { get => expired; set => expired = value; }

        private void Awake()
        {
            meshRenderer = GetComponent<Renderer>();
            body = GetComponent<Rigidbody>();
        }

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

        public void Interact()
        {
            Interactions.OnGrabItem?.Invoke(this);
            SetRigidBodyProperties();
            SetOutlineThick(0);
            isBeingHolded = true;

        }

        private void SetRigidBodyProperties()
        {
            body.velocity = Vector3.zero;
            body.useGravity = false;
            body.transform.localPosition = Vector3.zero;
        }

        public void SetOutlineThick(float value) => meshRenderer.materials[1].SetFloat("_OutlineThick", value);

        public void OnLookToCurrentObject() { }

        public void Release()
        {
            body.useGravity = true;
            isBeingHolded = false;
        }
    }
}