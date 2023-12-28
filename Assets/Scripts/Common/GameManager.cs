using EmployerOfTheMonth.Spawners;
using EmployerOfTheMonth.Quests;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using EmployerOfTheMonth.AI;

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

                KillTheThiefQuest();
            }, () =>
            {
                GameObject.Find("ReplaceObjects").SetActive(false);
                StartCoroutine(Start());
            });
        }

        private void KillTheThiefQuest()
        {
            QuestManager.InitializeQuest("SmileTheThiefIsHere", onComplete: () =>
            {
                QuestManager.InitializeQuest("KillTheThief");
            }, onInitialize: () =>
           {
               var baseCostumer = GameObject.Find("BaseCustomer");
               var tv = GameObject.Find("Television");

               baseCostumer.GetComponent<NavMeshAgent>().SetDestination(tv.transform.position);
               baseCostumer.GetComponent<Customer>().OnArriveInDestination += GrabTv;
           });
        }

        private void GrabTv()
        {
            QuestManager.CompleteQuest();

            var baseCostumer = GameObject.Find("BaseCustomer");
            var tv = GameObject.Find("Television");

            tv.transform.SetParent(baseCostumer.transform.GetChild(0));

            tv.transform.rotation = Quaternion.identity;
            tv.transform.localPosition = Vector3.zero;

            baseCostumer.GetComponent<Customer>().OnArriveInDestination -= GrabTv;
        }
    }
}