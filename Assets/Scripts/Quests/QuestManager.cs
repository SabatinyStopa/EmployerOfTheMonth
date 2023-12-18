using UnityEngine;

namespace EmployerOfTheMonth.Quests
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private Quest[] quests;

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