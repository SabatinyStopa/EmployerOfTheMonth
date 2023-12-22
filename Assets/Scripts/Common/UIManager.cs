using System.Collections;
using UnityEngine;
using TMPro;

namespace EmployerOfTheMonth.Common
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager Instance;
        [SerializeField] private TextMeshProUGUI bottomText;
        [SerializeField] private TextMeshProUGUI questText;

        private void Awake() => Instance = this;

        private void Start()
        {
            bottomText.text = string.Empty;
            questText.text = string.Empty;
        }
        public static void SetQuestText(string text) => Instance.questText.text = text;
        public static void SetBottomText(string text) => Instance.StartCoroutine(SpawnText(text));
        private static IEnumerator SpawnText(string text)
        {
            var counter = 0;
            Instance.bottomText.text = string.Empty;

            while (counter < text.Length)
            {
                Instance.bottomText.text += text[counter];
                yield return new WaitForSeconds(0.01f);
                counter++;
            }
        }
    }
}