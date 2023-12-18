using TMPro;
using UnityEngine;

namespace EmployerOfTheMonth.Common
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager Instance;
        [SerializeField] private TextMeshProUGUI bottomText;
        [SerializeField] private TextMeshProUGUI questText;

        private void Awake() => Instance = this;
        public static void SetQuestText(string text) => Instance.questText.text = text;
        public static void SetBottomText(string text) => Instance.bottomText.text = text;
    }
}