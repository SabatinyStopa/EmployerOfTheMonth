using UnityEngine;

namespace EmployerOfTheMonth.Interfaces
{
    public interface IInteract
    {
        public void Interact(Transform pickupPoint);
        public void SetOutlineThick(float value);
    }
}