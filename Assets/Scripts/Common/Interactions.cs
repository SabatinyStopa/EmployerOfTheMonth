using EmployerOfTheMonth.Interfaces;
using EmployerOfTheMonth.Common;
using UnityEngine;
using System;
using TMPro;

namespace EmployerOfTheMonth.Player
{
    public class Interactions : MonoBehaviour
    {
        public static Action<Grababble> OnGrabItem;
        [SerializeField] private Transform pickupPoint;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private TextMeshProUGUI interactionText;
        [SerializeField] private TextMeshProUGUI detailsText;

        private FirstPersonShooterController firstPersonShooterController;
        private Grababble holdingItem;
        private IInteract lookingTo;
        private UIItem UIItem;

        private void Start()
        {
            UIItem = FindObjectOfType<UIItem>();
            firstPersonShooterController = GetComponent<FirstPersonShooterController>();

            OnGrabItem += OnClickToGrabItem;
        }

        private void OnDestroy() => OnGrabItem -= OnClickToGrabItem;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && holdingItem != null) UIItem.ToggleDescription(holdingItem.transform);

            detailsText.enabled = holdingItem != null && !UIItem.IsShowing;

            if (firstPersonShooterController.IsShowingGun || UIItem.IsShowing) return;

            GrabItemsHandler();

            if (Input.GetMouseButtonDown(2) && holdingItem != null) holdingItem.transform.localPosition = Vector3.zero;
            pickupPoint.transform.Rotate(pickupPoint.transform.up * Input.mouseScrollDelta.y * 50, Space.Self);
        }

        private void GrabItemsHandler()
        {
            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out RaycastHit hit, 1.5f, layerMask) && holdingItem == null)
            {
                var isLookingInThisFrame = hit.transform.GetComponent<IInteract>();

                if (isLookingInThisFrame != lookingTo)
                {
                    if (lookingTo != null)
                        lookingTo.SetOutlineThick(0);

                    isLookingInThisFrame.SetOutlineThick(1.1f);
                    lookingTo = isLookingInThisFrame;
                }

                interactionText.enabled = true;
            }
            else
            {
                interactionText.enabled = false;
                lookingTo?.SetOutlineThick(0);
                lookingTo = null;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (holdingItem == null && lookingTo != null)
                    lookingTo.Interact();
                else if (holdingItem != null)
                    ReleaseItem();
            }
        }

        private void ReleaseItem()
        {
            holdingItem.transform.SetParent(null);
            holdingItem.Release();
            holdingItem = null;
        }

        private void OnClickToGrabItem(Grababble item)
        {
            holdingItem = item;
            pickupPoint.rotation = Quaternion.identity;
            item.transform.SetParent(pickupPoint);
        }
    }
}