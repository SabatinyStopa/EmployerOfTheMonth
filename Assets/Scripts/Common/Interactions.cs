using UnityEngine;
using TMPro;
using System;

namespace EmployerOfTheMonth.Player
{
    public class Interactions : MonoBehaviour
    {
        [SerializeField] private Transform pickupPoint;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private TextMeshProUGUI interactionText;


        private Rigidbody currentItemBody;
        private Transform lookingTo;
        private FirstPersonShooterController firstPersonShooterController;

        private void Start() => firstPersonShooterController = GetComponent<FirstPersonShooterController>();

        private void Update()
        {
            if (firstPersonShooterController.HasGun) return;

            GrabItemsHandler();
            GrabGunsHandler();
            pickupPoint.transform.Rotate(pickupPoint.transform.up * Input.mouseScrollDelta.y * 50, Space.Self);
        }

        private void GrabGunsHandler()
        {
            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            var hitted = Physics.Raycast(ray, out RaycastHit hit, 2f);

            interactionText.enabled = hitted && hit.transform.CompareTag("Gun");

            if (hitted && Input.GetMouseButtonDown(0) && hit.transform.CompareTag("Gun"))
            {
                firstPersonShooterController.Grab(hit.transform);
                hit.transform.GetComponent<Collider>().enabled = false;
            }
        }

        private void GrabItemsHandler()
        {
            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            var hitted = Physics.Raycast(ray, out RaycastHit hit, 2f, layerMask);

            SetOutlineToTheCurrentLooking(hitted, hit);

            interactionText.enabled = hitted && currentItemBody == null;

            if (hitted && Input.GetMouseButtonDown(0) && currentItemBody == null)
                OnClickToGrabItem(hit);
            else if (Input.GetMouseButtonDown(0) && currentItemBody != null)
                OnClickToReleaseItem();
        }

        private void OnClickToReleaseItem()
        {
            currentItemBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            currentItemBody.constraints = RigidbodyConstraints.None;
            currentItemBody.transform.SetParent(null);
            currentItemBody.useGravity = true;
            currentItemBody = null;
        }

        private void OnClickToGrabItem(RaycastHit hit)
        {
            currentItemBody = hit.transform.GetComponent<Rigidbody>();

            pickupPoint.rotation = Quaternion.identity;

            currentItemBody.transform.SetParent(pickupPoint);
            currentItemBody.velocity = Vector3.zero;
            currentItemBody.useGravity = false;
            currentItemBody.constraints = RigidbodyConstraints.FreezePosition;
            currentItemBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            currentItemBody.transform.localPosition = Vector3.zero;

            currentItemBody.GetComponent<Renderer>().materials[1].SetFloat("_OutlineThick", 0f);
        }

        private void SetOutlineToTheCurrentLooking(bool hitted, RaycastHit hit)
        {
            if (lookingTo != null)
            {
                lookingTo.GetComponent<Renderer>().materials[1].SetFloat("_OutlineThick", 0f);
                lookingTo = null;
            }

            if (hitted && pickupPoint.childCount <= 0)
            {
                lookingTo = hit.transform;
                hit.transform.GetComponent<Renderer>().materials[1].SetFloat("_OutlineThick", 1.1f);
            }
        }
    }
}