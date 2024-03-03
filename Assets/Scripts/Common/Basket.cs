using System.Collections.Generic;
using UnityEngine;

namespace EmployerOfTheMonth.Common
{
    public class Basket : Grababble
    {
        [SerializeField] private List<Rigidbody> rigidbodiesInside = new List<Rigidbody>();

        public override void Interact()
        {
            base.Interact();
            transform.rotation = Quaternion.identity;
            body.freezeRotation = true;
        }

        private void LateUpdate()
        {
            if (isBeingHolded) body.velocity = Vector3.zero;

            if (Input.GetKey(KeyCode.Space))
                body.velocity = transform.forward * 2f;
            else
                body.velocity += Vector3.zero;

            foreach (Rigidbody body in rigidbodiesInside) body.velocity = Vector3.zero;
        }

        public override void Release()
        {
            base.Release();
            body.freezeRotation = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Item item))
            {
                item.transform.SetParent(transform);

                if (item.TryGetComponent(out Rigidbody body))
                {
                    rigidbodiesInside.Add(body);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Item item))
            {
                item.transform.SetParent(null);

                if (item.TryGetComponent(out Rigidbody body))
                {
                    rigidbodiesInside.Remove(body);
                }
            }
        }
    }
}
