using System.Collections.Generic;
using EmployerOfTheMonth.Helpers;
using EmployerOfTheMonth.Common;
using System.Linq;
using UnityEngine;
using TMPro;

namespace EmployerOfTheMonth.Quests.AllQuests
{
    public class ExpiredObjectsQuest : QuestInitializer
    {
        [SerializeField] private int productsToExpire = 10;
        [SerializeField] private TextMeshProUGUI quantityText;
        [SerializeField] private Material debugMaterial;

        private List<Item> expiredItems = new List<Item>();
        private List<Item> itemsOnTheBox = new List<Item>();

        public override void OnInitializeQuest()
        {
            base.OnInitializeQuest();

            var allItems = FindObjectsOfType<Item>();

            if (productsToExpire > allItems.Length) productsToExpire = allItems.Length;

            var randomizeItems = allItems.ToList();

            randomizeItems.Shuffle();

            foreach (Item item in allItems) item.Description = DescriptionText(item, false);

            for (int i = 0; i < productsToExpire; i++)
            {
                expiredItems.Add(randomizeItems[i]);

                randomizeItems[i].Description = DescriptionText(randomizeItems[i], true);

                randomizeItems[i].GetComponent<Renderer>().sharedMaterial = debugMaterial;
            }

            quantityText.text = "Total expired items: " + productsToExpire + " \n" +
                                "Total in the box: " + itemsOnTheBox.Count;
        }

        private void OnTriggerEnter(Collider other)
        {
            var item = other.GetComponent<Item>();

            if (item == null) return;

            if (!expiredItems.Contains(item))
            {
                UIManager.SetBottomText("Idiot! This item is not expired!", 1.2f);
                return;
            }

            itemsOnTheBox.Add(item);

            quantityText.text = "Total expired items: " + productsToExpire + " \n" +
                                "Discarted in the box: " + itemsOnTheBox.Count;


            if (itemsOnTheBox.Count >= productsToExpire)
            {
                for (int i = 0; i < itemsOnTheBox.Count; i++) Destroy(itemsOnTheBox[i].gameObject);
                QuestManager.CompleteQuest();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var item = other.GetComponent<Item>();
            if (item == null) return;

            itemsOnTheBox.Remove(item);

            quantityText.text = "Total expired items: " + productsToExpire + " \n" +
                                "Total in the box: " + itemsOnTheBox.Count;
        }

        private string DescriptionText(Item item, bool isExpired) =>
                                    "Product: " + item.name.Split('(')[0] + "\n" +
                                    "This product can be consumed without any problem \n" +
                                    "Expiration date: " + (!isExpired ? "Not expired" : "Expired") + "\n";
    }
}