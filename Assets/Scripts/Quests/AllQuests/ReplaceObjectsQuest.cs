using System.Collections.Generic;
using EmployerOfTheMonth.Common;
using System.Collections;
using UnityEngine;
using TMPro;
using System;

namespace EmployerOfTheMonth.Quests.AllQuests
{
    public class ReplaceObjectsQuest : QuestInitializer
    {
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material materialOnTheGround;
        private List<Item> items = new List<Item>();
        private float timeUntilTheyArrive = 120;
        private float timer = 0;

        public override void OnComplete()
        {
            base.OnComplete();
            UIManager.SetQuestText(string.Empty);
        }

        public override void OnFail()
        {
            base.OnFail();
            StopAllCoroutines();
            items.Clear();
            countText.text = string.Empty;
        }

        public override void OnInitializeQuest()
        {
            base.OnInitializeQuest();
            StopAllCoroutines();
            items.Clear();
            countText.text = string.Empty;
            StartCoroutine(Timer());
        }

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

                var timeSpan = TimeSpan.FromSeconds(timer);
                var minutesText = timeSpan.Minutes >= 10 ? timeSpan.Minutes.ToString() : "0" + timeSpan.Minutes;
                var secondsText = timeSpan.Seconds >= 10 ? timeSpan.Seconds.ToString() : "0" + timeSpan.Seconds;

                timerText.text = $"Time until the customers arrive: {minutesText}:{secondsText}";
            }

            QuestManager.FailQuest();
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