using System;
using UnityEngine;

namespace EmployerOfTheMonth.Player
{
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float mouseSensitive = 100f;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Camera cam;
        [SerializeField] private Light flashlight;
        private float xRotation = 0f;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            if (SettingsManager.IsPaused) return;

            // MovementHandler();
            // MouseLook();

            if (Input.GetKeyDown(KeyCode.F)) flashlight.enabled = !flashlight.enabled;
        }

        private void FixedUpdate()
        {
            if (SettingsManager.IsPaused) return;

            MovementHandler();
            MouseLook();
        }

        public void SetMouseSensitivity(float mouseSensitive) => this.mouseSensitive = mouseSensitive;

        private void MouseLook()
        {
            var mouseX = Input.GetAxis("Mouse X") * mouseSensitive * Time.deltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * mouseSensitive * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Math.Clamp(xRotation, -90f, 90f);

            cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }

        private void MovementHandler()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var moveDirection = transform.TransformDirection(new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime);

            characterController.Move(moveDirection);
            characterController.Move(Physics.gravity * Time.deltaTime);
        }
    }
}