using UnityEngine;

namespace EmployerOfTheMonth.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Supermarket/Quest", order = 0)]
    public class QuestScriptable : ScriptableObject
    {
        public string Id;
        public string ShortDescription;
        public string Instructions;
        public bool DesactiveAfterQuest = true;
    }
}