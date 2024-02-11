using EmployerOfTheMonth.Common;
using System.Collections;
using UnityEngine;

namespace EmployerOfTheMonth.Quests
{
    public class TutorialQuest : QuestInitializer
    {
        [SerializeField] private string doorName = "TEMPDOOR";

        private IEnumerator TutorialText()
        {
            var secondsToWait = 3f;
            yield return new WaitForSeconds(1f);
            UIManager.SetBottomText("Move around using WASD", secondsToWait);
            yield return new WaitForSeconds(secondsToWait);
            UIManager.SetBottomText("Use the mouse to look around", secondsToWait);
            yield return new WaitForSeconds(secondsToWait);
            FindObjectOfType<RoomTvController>().Initialize();
        }

        public override void OnComplete()
        {
            base.OnComplete();
            GameObject.Find(doorName).SetActive(false);
        }

        public override void OnInitializeQuest()
        {
            base.OnInitializeQuest();
            StartCoroutine(TutorialText());
        }
    }
}