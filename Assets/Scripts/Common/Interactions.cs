using EmployerOfTheMonth.Common;
using UnityEngine;
using TMPro;

namespace EmployerOfTheMonth.Player
{
    public class Interactions : MonoBehaviour
    {
        [SerializeField] private Transform pickupPoint;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private TextMeshProUGUI interactionText;

        private void Update()
        {
            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            interactionText.enabled = Physics.Raycast(ray, 2f, layerMask) && pickupPoint.childCount <= 0;

            if (Physics.Raycast(ray, out RaycastHit hit, 2f, layerMask) && Input.GetKeyDown(KeyCode.E) && pickupPoint.childCount <= 0)
            {
                var item = hit.transform.GetComponent<Item>();
                var itemBody = item.GetComponent<Rigidbody>();

                itemBody.isKinematic = true;
                item.transform.SetParent(pickupPoint);
                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.identity;
                itemBody.velocity = Vector3.zero;
                interactionText.enabled = false;
            }
            else if (Input.GetKeyDown(KeyCode.E) && pickupPoint.childCount > 0)
            {
                pickupPoint.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
                pickupPoint.GetChild(0).SetParent(null);
            }
        }
    }
}