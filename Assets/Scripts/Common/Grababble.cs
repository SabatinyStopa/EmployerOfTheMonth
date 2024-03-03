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
        private Transform pointToMove;
        private float lerpSpeed = 100f;


        private void Awake()
        {
            if (meshRenderer == null)
                meshRenderer = GetComponent<Renderer>();
            body = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (pointToMove == null) return;

            body.MovePosition(Vector3.Lerp(transform.position, pointToMove.position, Time.deltaTime * lerpSpeed));

            // if (Vector3.Distance(pointToMove.position, body.position) >= 0.1f)
            //     body.velocity = (pointToMove.position - body.position).normalized * lerpSpeed;
            // else
            //     body.velocity = Vector3.zero;
        }

        public virtual void Interact(Transform pickupPoint)
        {
            Interactions.OnGrabItem?.Invoke(this);
            body.useGravity = false;
            SetOutlineThick(0);
            isBeingHolded = true;
            pointToMove = pickupPoint;

            transform.position = pickupPoint.position;
        }

        public void SetOutlineThick(float value) => meshRenderer.materials[1].SetFloat("_OutlineThick", value);

        public virtual void Release()
        {
            body.useGravity = true;
            isBeingHolded = false;
            pointToMove = null;
            Interactions.OnReleaseItem?.Invoke();
        }
    }
}