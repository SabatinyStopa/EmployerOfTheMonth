using EmployerOfTheMonth.Common;
using System.Collections;
using UnityEngine;
using System;

namespace EmployerOfTheMonth.Quests
{
    public class QuestManager : MonoBehaviour
    {
        private static QuestManager instance;

        [SerializeField] private QuestInitializer[] questInitializers;
        [SerializeField] private int currentQuestIndex = 0;

        private Quest currentQuest;

        private void Awake() => instance = this;

        public static void InitializeSequenceOfQuests()
        {
            var questInitialer = Instantiate(instance.questInitializers[instance.currentQuestIndex]);
            questInitialer.Initialize();
        }

        public static void InitializeQuest(
            QuestScriptable questScriptable,
            Action onComplete = null,
            Action onFail = null, Action onInitialize = null) => instance.StartCoroutine(instance.Initialize(questScriptable, onComplete, onFail, onInitialize));

        public static void CompleteQuest(bool initializeNext = true)
        {
            UIManager.SetBottomText(string.Empty, 0);
            UIManager.SetQuestText(string.Empty);
            instance.currentQuest.Complete();

            if (initializeNext)
            {
                instance.currentQuestIndex++;

                if (instance.currentQuestIndex >= instance.questInitializers.Length) instance.currentQuestIndex = 1;

                InitializeSequenceOfQuests();
            }
        }

        public static void FailQuest() => instance.currentQuest.Fail();

        private IEnumerator Initialize(QuestScriptable questScriptable, Action onComplete = null, Action onFail = null, Action onInitialize = null)
        {
            while (UIManager.IsShowingText()) yield return null;

            instance.currentQuest = new Quest(  questScriptable.Id,
                                                questScriptable.ShortDescription,
                                                questScriptable.Instructions, onComplete, onFail);
            

            instance.currentQuest.Initialize();

            onInitialize?.Invoke();
        }
    }
}