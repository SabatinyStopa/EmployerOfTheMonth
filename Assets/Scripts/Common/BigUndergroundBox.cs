using UnityEngine;

namespace EmployerOfTheMonth.Common
{
    public class BigUndergroundBox : MonoBehaviour
    {
        [SerializeField] private Transform respawnPoint;

        private void OnTriggerEnter(Collider other)
        {
            var rigidbody = other.GetComponent<Rigidbody>();

            if (rigidbody) rigidbody.velocity = Vector3.zero;
            
            other.transform.position = respawnPoint.position;
        }
    }
}