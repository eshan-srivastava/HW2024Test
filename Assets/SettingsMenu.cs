using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions; //array holds resolutions available on the machine
    public void Start()
    {
        resolutions = Screen.resolutions; //fetching available resolutions
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIdx = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIdx = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIdx;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIdx)
    {
        //used Int here instead of direct resolution, because resolution cannot be set dynamically but Int can be
        //also dropdowns uses index to send data on what option was clicked
        Resolution resolution = resolutions[resolutionIdx];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("Volume", volume);
    }
}
