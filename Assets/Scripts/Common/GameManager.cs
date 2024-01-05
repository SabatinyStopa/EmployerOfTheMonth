using EmployerOfTheMonth.Spawners;
using EmployerOfTheMonth.Quests;
using EmployerOfTheMonth.AI;
using System.Collections;
using UnityEngine.AI;
using UnityEngine;

namespace EmployerOfTheMonth.Common
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        private void Awake() => instance = this;

        private IEnumerator Start()
        {
            FindObjectOfType<ShelvesSpawner>().Spawn();
            // yield return new WaitForSeconds(1f);
            // KillTheThiefQuest();

            yield return null;

            QuestManager.InitializeQuest("ExpiredProducts");

            // yield return TutorialText();

            // QuestManager.InitializeQuest("Tutorial", () =>
            // {
            //     StartReplaceObjectsQuest();
            // });
        }

        private void StartReplaceObjectsQuest()
        {
            QuestManager.InitializeQuest("ReplaceObjects", () =>
            {
                GameObject.Find("ReplaceObjects").SetActive(false);
                UIManager.SetQuestText(string.Empty);

                KillTheThiefQuest();
            }, () =>
            {
                GameObject.Find("ReplaceObjects").SetActive(false);
                StartReplaceObjectsQuest();
            }, () =>
            {
                FindObjectOfType<ShelvesSpawner>().Spawn();
            });
        }

        private IEnumerator TutorialText()
        {
            var secondsToWait = 3f;
            yield return new WaitForSeconds(1f);
            UIManager.SetBottomText("Move around using WASD", secondsToWait);
            yield return new WaitForSeconds(secondsToWait);
            UIManager.SetBottomText("Use the mouse to look around", secondsToWait);
            yield return new WaitForSeconds(secondsToWait);
        }

        private void KillTheThiefQuest()
        {
            QuestManager.InitializeQuest("SmileTheThiefIsHere", onComplete: () =>
            {
                UIManager.SetQuestText(string.Empty);
                UIManager.SetBottomText("There are guns in the staff small room!", 2f);
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