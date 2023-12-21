using UnityEngine;
using System;

namespace EmployerOfTheMonth.Quests
{
    public class QuestManager : MonoBehaviour
    {
        [Serializable]
        struct QuestContainer
        {
            public QuestScriptable Scriptable;
            public GameObject[] QuestObjects;
        }

        [SerializeField] private QuestContainer[] questContainers;

        private Quest currentQuest;

        public Quest CurrentQuest { get => currentQuest; set => currentQuest = value; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T)) InitializeQuest("ReplaceObjects");
        }

        public void InitializeQuest(string questId)
        {
            foreach (QuestContainer questContainer in questContainers)
            {
                if (Equals(questContainer.Scriptable.Id, questId))
                {
                    foreach (GameObject questObj in questContainer.QuestObjects) questObj.SetActive(true);

                    currentQuest = new Quest(questContainer.Scriptable.Id, questContainer.Scriptable.ShortDescription, questContainer.Scriptable.Instructions);
                    break;
                }
            }

            currentQuest.Initialize();
        }
    }
}