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
        private Transform lookingTo;

        private void Update()
        {
            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            var hitted = Physics.Raycast(ray, out RaycastHit hit, 2f, layerMask);

            if (lookingTo != null)
            {
                lookingTo.GetComponent<Renderer>().materials[1].SetFloat("_OutlineThick", 0f);
                lookingTo = null;
            }

            if (hitted)
            {
                lookingTo = hit.transform;
                hit.transform.GetComponent<Renderer>().materials[1].SetFloat("_OutlineThick", 1.1f);
            }
            
            interactionText.enabled = hitted && currentItemBody == null;

            if (hitted && Input.GetMouseButtonDown(0) && currentItemBody == null)
            {
                currentItemBody = hit.transform.GetComponent<Rigidbody>();

                currentItemBody.transform.SetParent(pickupPoint);
                currentItemBody.position = pickupPoint.position;
                currentItemBody.velocity = Vector3.zero;
                currentItemBody.useGravity = false;
                currentItemBody.constraints = RigidbodyConstraints.FreezePosition;
                currentItemBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }
            else if (Input.GetMouseButtonDown(0) && currentItemBody != null)
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