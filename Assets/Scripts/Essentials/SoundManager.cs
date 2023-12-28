using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private SoundScriptable[] allScriptables;
    [SerializeField] private bool testSound = true;
    [SerializeField] private string testId = "Shot";
    [SerializeField] private string testMusicId = "Music";

    private void Awake() => Instance = this;

    private void Start()
    {
        if (testSound) PlaySound(testMusicId);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && testSound) SoundManager.Instance.PlaySound(testId);
    }

    public void PlaySound(string id)
    {
        foreach (SoundScriptable soundScriptable in allScriptables)
        {
            if (soundScriptable.Id == id)
            {
                Play(id, soundScriptable);
                break;
            }
        }
    }

    private void Play(string id, SoundScriptable soundScriptable)
    {
        var sound = new GameObject("Sound-" + id).AddComponent<AudioSource>();

        sound.clip = soundScriptable.AudioClip;
        sound.playOnAwake = false;
        sound.loop = soundScriptable.Loop;
        sound.outputAudioMixerGroup = soundScriptable.AudioMixer;

        sound.Play();

        if (!sound.loop)
            Destroy(sound.gameObject, soundScriptable.AudioClip.length + 0.01f);
    }
}
