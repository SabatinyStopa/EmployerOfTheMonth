using UnityEngine;
using System;
using EmployerOfTheMonth.Common;
using System.Collections;

namespace EmployerOfTheMonth.Quests
{
    public class QuestManager : MonoBehaviour
    {
        private static QuestManager instance;
        [Serializable]
        struct QuestContainer
        {
            public QuestScriptable Scriptable;
            public GameObject[] QuestObjects;
        }

        [SerializeField] private QuestContainer[] questContainers;

        private Quest currentQuest;

        private void Awake() => instance = this;

        public static void InitializeQuest(
            string questId,
            Action onComplete = null,
            Action onFail = null, Action onInitialize = null) => instance.StartCoroutine(instance.Initialize(questId, onComplete, onFail, onInitialize));

        public static void CompleteQuest()
        {
            UIManager.SetBottomText(string.Empty, 0);
            UIManager.SetQuestText(string.Empty);
            instance.currentQuest.Complete();

            foreach (QuestContainer questContainer in instance.questContainers)
            {
                if (Equals(questContainer.Scriptable.Id, instance.currentQuest.Id))
                {
                    foreach (GameObject questObj in questContainer.QuestObjects) questObj.SetActive(false);
                    break;
                }
            }
        }

        public static void FailQuest() => instance.currentQuest.Fail();

        private IEnumerator Initialize(string questId, Action onComplete = null, Action onFail = null, Action onInitialize = null)
        {
            while (UIManager.IsShowingText()) yield return null;

            foreach (QuestContainer questContainer in instance.questContainers)
            {
                if (Equals(questContainer.Scriptable.Id, questId))
                {
                    foreach (GameObject questObj in questContainer.QuestObjects) questObj.SetActive(true);

                    instance.currentQuest = new Quest(questContainer.Scriptable.Id,
                                                      questContainer.Scriptable.ShortDescription,
                                                      questContainer.Scriptable.Instructions, onComplete, onFail);
                    break;
                }
            }

            instance.currentQuest.Initialize();

            onInitialize?.Invoke();
        }
    }
}