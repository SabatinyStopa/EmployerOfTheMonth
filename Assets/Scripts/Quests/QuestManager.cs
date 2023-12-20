using System.Collections.Generic;
using UnityEngine;

namespace EmployerOfTheMonth.Quests
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private QuestScriptable[] questScriptables;

        private List<Quest> quests = new List<Quest>();

        private void Start()
        {
            foreach (QuestScriptable questScriptable in questScriptables)
                quests.Add(new Quest(questScriptable.Id, questScriptable.ShortDescription, questScriptable.Instructions, null));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T)) InitializeQuest("ReplaceObjects");
        }

        public void InitializeQuest(string questId)
        {
            foreach (Quest quest in quests)
            {
                if (Equals(quest.Id, questId))
                {
                    quest.Initialize();
                    break;
                }
            }
        }
    }
}