using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using EmployerOfTheMonth.Player;


public class SettingsManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject panel;
    [Header("Resolution")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullScreenMode;
    [Header("Audio")]
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [Header("Sensibility")]
    [SerializeField] private Slider mouseSlider;
    private FirstPersonController firstPersonController;

    public static bool IsPaused;

    private void Start()
    {
        firstPersonController = FindObjectOfType<FirstPersonController>();
        SetResolutionDropDown();
        SetMasterVolume();
        SetSfxVolume();
        SetMusicVolume();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(!panel.activeInHierarchy);
            Cursor.visible = panel.activeInHierarchy;
            Cursor.lockState = panel.activeInHierarchy ? CursorLockMode.None : CursorLockMode.Locked;
            IsPaused = panel.activeInHierarchy;
        }
    }

    public void QuitApplication() => Application.Quit();
    public void SetMasterVolume() => masterMixer.SetFloat("MasterVolume", Mathf.Log10(masterSlider.value) * 20);
    public void SetSfxVolume() => masterMixer.SetFloat("SfxVolume", Mathf.Log10(sfxSlider.value) * 20);
    public void SetMusicVolume() => masterMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value) * 20);
    public void SetMouseSensitivity() => firstPersonController.SetMouseSensitivity(mouseSlider.value);
    public void SetFullScreenMode()
    {
        Screen.fullScreen = fullScreenMode.isOn;
        Screen.fullScreenMode = fullScreenMode.isOn ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;
    }

    public void SetResolution() => Screen.SetResolution(Screen.resolutions[resolutionDropdown.value].width, Screen.resolutions[resolutionDropdown.value].height, Screen.fullScreenMode);

    private void SetResolutionDropDown()
    {
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(GetResolutionList(out var index));
        resolutionDropdown.value = index;
        resolutionDropdown.RefreshShownValue();
    }

    private List<TMP_Dropdown.OptionData> GetResolutionList(out int index)
    {
        var list = new List<TMP_Dropdown.OptionData>();
        var currentResolution = 0;

        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            var resolution = Screen.resolutions[i];
            list.Add(new TMP_Dropdown.OptionData(resolution.width + "x" + resolution.height));

            if (resolution.width == Screen.currentResolution.width &&
                resolution.height == Screen.currentResolution.height)
                currentResolution = i;
        }

        index = currentResolution;

        return list;
    }
}
