using UnityEngine.Video;
using UnityEngine;
using TMPro;
using EmployerOfTheMonth.Quests;
using System;

namespace EmployerOfTheMonth.Common
{
    public class RoomTvController : MonoBehaviour
    {
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private TextMeshProUGUI instructions;

        private bool isNotInitialized = true;

        private void Start() => videoPlayer.loopPointReached += EndReached;
        private void OnDestroy() => videoPlayer.loopPointReached -= EndReached;

        private void EndReached(VideoPlayer source)
        {
            QuestManager.CompleteQuest();
            isNotInitialized = true;
        }

        private void Update()
        {
            if (isNotInitialized) return;

            if (Input.GetKeyDown(KeyCode.Alpha1)) videoPlayer.Play();
            else if (Input.GetKeyDown(KeyCode.Alpha2)) videoPlayer.Pause();
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                videoPlayer.Stop();
                videoPlayer.Play();
            }
        }

        public void Initialize()
        {
            instructions.text = "TV CONTROLS: \n Press 1 to Play.\n Press 2 To pause.\n Press 3 To restart";
            isNotInitialized = false;
        }
    }
}