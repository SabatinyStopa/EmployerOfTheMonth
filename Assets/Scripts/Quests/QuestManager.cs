using UnityEngine;
using System;
using EmployerOfTheMonth.Common;

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

        public static void InitializeQuest(string questId, Action onComplete = null, Action onFail = null)
        {
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
        }

        public static void CompleteQuest() => instance.currentQuest.Complete();

        public static void FailQuest() => instance.currentQuest.Fail();
    }
}