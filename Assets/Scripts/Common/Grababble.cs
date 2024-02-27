using EmployerOfTheMonth.Interfaces;
using EmployerOfTheMonth.Player;
using UnityEngine;

namespace EmployerOfTheMonth.Common
{
    public class Grababble : MonoBehaviour, IInteract
    {
        [SerializeField] protected Renderer meshRenderer;
        protected Rigidbody body;
        protected bool isBeingHolded;

        private void Awake()
        {
            if (meshRenderer == null)
                meshRenderer = GetComponent<Renderer>();
            body = GetComponent<Rigidbody>();
        }

        public virtual void Interact()
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
        }

        public void SetOutlineThick(float value) => meshRenderer.materials[1].SetFloat("_OutlineThick", value);

        public virtual void Release()
        {
            body.useGravity = true;
            isBeingHolded = false;
        }
    }
}