using System.Collections;
using EmployerOfTheMonth.Quests;
using EmployerOfTheMonth.Spawners;
using UnityEngine;

namespace EmployerOfTheMonth.Common
{

    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        private void Awake() => instance = this;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            FindObjectOfType<ShelvesSpawner>().Spawn();
            QuestManager.InitializeQuest("ReplaceObjects");
        }
    }
}