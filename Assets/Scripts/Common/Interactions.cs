using EmployerOfTheMonth.Common;
using UnityEngine;

namespace EmployerOfTheMonth.Player
{
    public class Interactions : MonoBehaviour
    {
        [SerializeField] private Transform pickupPoint;

        private void Update()
        {
            if (Physics.Raycast(Camera.main.transform.position,
                                Camera.main.transform.forward,
                                out RaycastHit hit,
                                10,
                                LayerMask.NameToLayer("Interactables")) && Input.GetKeyDown(KeyCode.E) && pickupPoint.childCount <= 0)
            {
                var item = hit.transform.GetComponent<Item>();

                item.transform.SetParent(pickupPoint);
                item.GetComponent<Rigidbody>().useGravity = false;
                item.transform.position = Vector3.zero;
                item.transform.rotation = Quaternion.identity;
            }
            else if (Input.GetKeyDown(KeyCode.E) && pickupPoint.childCount > 0)
            {
                pickupPoint.GetChild(0).GetComponent<Rigidbody>().useGravity = true;
                pickupPoint.GetChild(0).SetParent(null);
            }
        }
    }
}