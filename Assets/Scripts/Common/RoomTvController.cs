using UnityEngine.Video;
using UnityEngine;
using TMPro;
using EmployerOfTheMonth.Quests;

namespace EmployerOfTheMonth.Common
{
    public class RoomTvController : MonoBehaviour
    {
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private TextMeshProUGUI instructions;
        [SerializeField] private GameObject roomDoor;

        private void Start()
        {
            videoPlayer.loopPointReached += EndReached;
            instructions.text = "TV CONTROLS: \n Press 1 to Play.\n Press 2 To pause.\n Press 3 To restart";
        }

        private void OnDestroy() => videoPlayer.loopPointReached -= EndReached;

        private void EndReached(VideoPlayer source)
        {
            roomDoor.SetActive(false);
            QuestManager.CompleteQuest();
            Destroy(gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) videoPlayer.Play();
            else if (Input.GetKeyDown(KeyCode.Alpha2)) videoPlayer.Pause();
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                videoPlayer.Stop();
                videoPlayer.Play();
            }
        }
    }
}