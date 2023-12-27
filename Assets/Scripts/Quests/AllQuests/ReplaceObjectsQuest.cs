using System.Collections.Generic;
using EmployerOfTheMonth.Common;
using UnityEngine;
using TMPro;
using System.Collections;

namespace EmployerOfTheMonth.Quests.AllQuests
{
    public class ReplaceObjectsQuest : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material materialOnTheGround;
        private List<Item> items = new List<Item>();
        private float timeUntilTheyArrive = 120;
        private float timer = 0;

        private void OnEnable() => StartCoroutine(Timer());

        private IEnumerator Timer()
        {
            countText.text = string.Empty;
            timer = timeUntilTheyArrive;
            timerText.text = timer.ToString();

            while (timer > 0)
            {
                yield return new WaitForSeconds(1);

                while (SettingsManager.IsPaused)
                {
                    yield return null;
                }

                timer -= 1;

                timerText.text = "Time until the customers arrive: " +  timer.ToString();
            }

            QuestManager.FailQuest();
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            items.Clear();
            countText.text = string.Empty;
        }

        private void OnTriggerEnter(Collider other)
        {
            var item = other.GetComponent<Item>();

            if (item != null)
            {
                items.Add(item);
                countText.text = "Items on the ground: " + items.Count;

                item.GetComponent<Renderer>().sharedMaterial = materialOnTheGround;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var item = other.GetComponent<Item>();

            if (item != null && items.Contains(item))
            {
                items.Remove(item);
                countText.text = "Items on the ground: " + items.Count;
                item.GetComponent<Renderer>().sharedMaterial = defaultMaterial;

                if (items.Count <= 0) QuestManager.CompleteQuest();
            }
        }
    }
}