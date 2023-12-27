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

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            FindObjectOfType<ShelvesSpawner>().Spawn();
            QuestManager.InitializeQuest("ReplaceObjects", () =>
            {
                GameObject.Find("ReplaceObjects").SetActive(false);
                UIManager.SetQuestText(string.Empty);
            }, () =>
            {
                GameObject.Find("ReplaceObjects").SetActive(false);
                StartCoroutine(Start());
            });
        }
    }
}