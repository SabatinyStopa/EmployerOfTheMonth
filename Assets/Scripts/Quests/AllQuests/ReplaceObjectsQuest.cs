using System.Collections.Generic;
using EmployerOfTheMonth.Common;
using UnityEngine;
using System;
using TMPro;

namespace EmployerOfTheMonth.Quests.AllQuests
{
    public class ReplaceObjectsQuest : Quest
    {
        [SerializeField] private TextMeshProUGUI countText;

        private List<Item> items = new List<Item>();

        public ReplaceObjectsQuest(string id,
                                   string shortDescription,
                                   string instructions,
                                   Action onComplete) : base(id, shortDescription, instructions, onComplete)
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            var item = other.GetComponent<Item>();

            if (item != null)
            {
                items.Add(item);
                countText.text = "Items on the ground: " + items.Count;
            }
        }
    }
}