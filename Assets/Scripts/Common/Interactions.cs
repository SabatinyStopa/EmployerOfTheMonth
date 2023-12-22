using UnityEngine;
using TMPro;

namespace EmployerOfTheMonth.Player
{
    public class Interactions : MonoBehaviour
    {
        [SerializeField] private Transform pickupPoint;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private TextMeshProUGUI interactionText;

        private Rigidbody currentItemBody;

        private void Update()
        {
            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            interactionText.enabled = Physics.Raycast(ray, 2f, layerMask) && currentItemBody == null;

            if (Physics.Raycast(ray, out RaycastHit hit, 2f, layerMask) && Input.GetKeyDown(KeyCode.E) && currentItemBody == null)
            {
                currentItemBody = hit.transform.GetComponent<Rigidbody>();
                
                currentItemBody.transform.SetParent(pickupPoint);
                currentItemBody.position = pickupPoint.position;
                currentItemBody.velocity = Vector3.zero;
                currentItemBody.useGravity = false;
                currentItemBody.constraints = RigidbodyConstraints.FreezePosition;
                currentItemBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }
            else if (Input.GetKeyDown(KeyCode.E) && currentItemBody != null)
            {
                currentItemBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
                currentItemBody.constraints = RigidbodyConstraints.None;
                currentItemBody.transform.SetParent(null);
                currentItemBody.useGravity = true;
                currentItemBody = null;
            }
        }
    }
}