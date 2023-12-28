using System.Collections;
using UnityEngine;
using TMPro;

namespace EmployerOfTheMonth.Common
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager instance;
        [SerializeField] private TextMeshProUGUI bottomText;
        [SerializeField] private TextMeshProUGUI questText;

        private bool isShowingText;

        private void Awake() => instance = this;

        private void Start()
        {
            bottomText.text = string.Empty;
            questText.text = string.Empty;
        }

        public static bool IsShowingText() => instance.isShowingText;
        public static void SetQuestText(string text) => instance.questText.text = text;
        public static void SetBottomText(string text, float waitTime = 5f)
        {
            instance.StopAllCoroutines();
            instance.StartCoroutine(SpawnText(text, waitTime));
        }

        private static IEnumerator SpawnText(string text, float waitTime)
        {
            instance.isShowingText = true;
            var counter = 0;
            instance.bottomText.text = string.Empty;
            instance.bottomText.enabled = true;

            while (counter < text.Length)
            {
                instance.bottomText.text += text[counter];
                yield return new WaitForSeconds(0.03f);
                counter++;
            }

            yield return new WaitForSeconds(waitTime);
            instance.bottomText.enabled = false;
            instance.isShowingText = false;
        }
    }
}