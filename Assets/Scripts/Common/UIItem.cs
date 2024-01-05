using UnityEngine;
using TMPro;

namespace EmployerOfTheMonth.Common
{
    public class UIItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private GameObject containerDescription;

        private bool isShowing = false;

        public bool IsShowing { get => isShowing; }

        public void ToggleDescription(Transform currentItemBody)
        {
            descriptionText.text = currentItemBody.GetComponent<Item>().Description;
            containerDescription.SetActive(!IsShowing);
            isShowing = !IsShowing;
        }
    }
}