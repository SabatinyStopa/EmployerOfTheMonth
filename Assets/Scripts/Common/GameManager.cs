using System.Collections;
using EmployerOfTheMonth.Quests;
using EmployerOfTheMonth.Spawners;
using UnityEngine;

namespace EmployerOfTheMonth.Common
{

    public class GameManager : MonoBehaviour
    {
        private static GameManager Instance;

        private void Awake() => Instance = this;

        private IEnumerator Start() {
            yield return new WaitForSeconds(3f);
            FindObjectOfType<ShelvesSpawner>().Spawn();
            QuestManager.InitializeQuest("ReplaceObjects");
        }
    }
}