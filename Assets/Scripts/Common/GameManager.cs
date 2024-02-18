using EmployerOfTheMonth.Spawners;
using EmployerOfTheMonth.Quests;
using System.Collections;
using UnityEngine;

namespace EmployerOfTheMonth.Common
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        private void Awake() => instance = this;

        public static GameManager GetInstance() => instance;

        private IEnumerator Start()
        {
            FindObjectOfType<ShelvesSpawner>().Spawn();
            yield return new WaitForSeconds(1f);
            QuestManager.InitializeSequenceOfQuests();
        }
    }
}