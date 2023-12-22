using System.Collections.Generic;
using EmployerOfTheMonth.Common;
using UnityEngine;
using TMPro;

namespace EmployerOfTheMonth.Quests.AllQuests
{
    public class ReplaceObjectsQuest : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private List<Item> items = new List<Item>();

        private void Awake() => countText.text = string.Empty;

        private void OnTriggerEnter(Collider other)
        {
            var item = other.GetComponent<Item>();

            if (item != null)
            {
                items.Add(item);
                countText.text = "Items on the ground: " + items.Count;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var item = other.GetComponent<Item>();

            if (item != null && items.Contains(item))
            {
                items.Remove(item);
                countText.text = "Items on the ground: " + items.Count;

                if (items.Count <= 0) QuestManager.CompleteQuest();
            }
        }
    }
}