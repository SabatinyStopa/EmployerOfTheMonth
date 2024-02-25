using TMPro;
using UnityEngine;

namespace EmployerOfTheMonth.Common
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private Transform itemToSpawn;
        [SerializeField] private Transform pointToSpawn;

        private TextMeshProUGUI interactText;
        private bool isOnArea = false;

        private void Start() => interactText = GameObject.Find("InteractItemSpawner").GetComponent<TextMeshProUGUI>();

        public void GrabItem()
        {
            Instantiate(itemToSpawn, pointToSpawn.position, Quaternion.identity);
            isOnArea = false;
            interactText.enabled = false;
        }

        private void Update()
        {
            if (isOnArea && Input.GetMouseButtonDown(0)) GrabItem();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                interactText.enabled = true;
                isOnArea = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                interactText.enabled = false;
                isOnArea = false;
            }
        }
    }
}