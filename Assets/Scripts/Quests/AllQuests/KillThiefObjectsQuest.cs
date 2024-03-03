using EmployerOfTheMonth.Common;
using EmployerOfTheMonth.AI;
using UnityEngine.AI;
using UnityEngine;
using System.Collections;

namespace EmployerOfTheMonth.Quests.AllQuests
{
    public class KillThiefObjectsQuest : QuestInitializer
    {
        [SerializeField] private GameObject gunsPart;
        private GameObject thief;

        public override void Initialize()
        {
            base.Initialize();
            
            UIManager.SetQuestText(string.Empty);
            UIManager.SetBottomText("Walk around and smile to the customers!", 2f);

            thief = GameObject.Find("Thief");
            var tv = GameObject.Find("Television");

            thief.GetComponent<NavMeshAgent>().SetDestination(tv.transform.position);
            thief.GetComponent<Customer>().OnArriveInDestination += GrabTv;
        }

        private void GrabTv()
        {
            var customerThief = thief.GetComponent<Customer>();
            var tv = GameObject.Find("Television");
            var lenght = customerThief.AnimationLenght("GrabTv");

            customerThief.tag = "Enemy";
            customerThief.transform.forward = -tv.transform.forward;
            customerThief.SetSpeed(0.6f);

            customerThief.PlayAnimation("GrabTv");
            customerThief.GetComponent<NavMeshAgent>().isStopped = true;

            Invoke(nameof(CompleteThiefPart), lenght + 2f);

            customerThief.OnArriveInDestination -= GrabTv;
        }

        public void CompleteThiefPart()
        {
            UIManager.SetQuestText("Kill the thief");
            UIManager.SetBottomText("Some one is trying to steal the TV", 2f);
            gunsPart.SetActive(true);

            var tv = GameObject.Find("Television");
            var tvPosition = tv.transform.position;
            var tvRotation = tv.transform.rotation;

            thief.GetComponent<Life>().OnDie += () =>
            {
                tv.transform.SetParent(null);
                Destroy(thief);
                QuestManager.CompleteQuest();
                tv.AddComponent<Rigidbody>();
                tv.AddComponent<BoxCollider>();

                GameManager.GetInstance().StartCoroutine(MakeTvGoBack(tv, tvPosition, tvRotation));
            };

            tv.transform.SetParent(thief.transform.GetChild(0));

            thief.GetComponent<NavMeshAgent>().isStopped = false;

            tv.transform.rotation = Quaternion.identity;
            tv.transform.localPosition = Vector3.zero;
        }

        private IEnumerator MakeTvGoBack(GameObject tv, Vector3 tvPosition, Quaternion tvRotation)
        {
            yield return new WaitForSeconds(5f);
            tv.transform.position = tvPosition;
            tv.transform.rotation = tvRotation;

            Destroy(tv.GetComponent<Rigidbody>());
            Destroy(tv.GetComponent<BoxCollider>());
        }
    }
}
