using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Sound", menuName = "Configuration/Sound", order = 0)]
public class SoundScriptable : ScriptableObject
{
    public string Id;
    public AudioClip AudioClip;
    public AudioMixerGroup AudioMixer;
    public bool Loop;
}