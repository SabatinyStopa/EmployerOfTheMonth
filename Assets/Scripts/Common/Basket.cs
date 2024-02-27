
using UnityEngine;

namespace EmployerOfTheMonth.Common
{
    public class Basket : Grababble
    {
        public override void Interact()
        {
            base.Interact();
            transform.rotation = Quaternion.identity;
            body.freezeRotation = true;
        }

        private void LateUpdate()
        {
            if (isBeingHolded) body.velocity = Vector3.zero;
        }

        public override void Release()
        {
            base.Release();
            body.freezeRotation = false;
        }
    }
}
