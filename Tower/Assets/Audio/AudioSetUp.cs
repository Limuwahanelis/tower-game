
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSetUp : MonoBehaviour
{
    private void Awake()
    {
        //    AudioSettingsData audioData;
        //    if (ScreenSettingsSaver.LoadScreenSettings() == null)
        //    {
        //        audioData = new AudioSettingsData(50,50,50);
        //        AudioSettingsSaver.SaveAudioSettings(audioData);
        //    }
        //    else
        //    {
        //        audioData = AudioSettingsSaver.LoadAudioSettings();
        //    }
        //    AudioVolumes.SetMasterVolume(audioData.masterVolume);
        //    AudioVolumes.SetBGMVolume(audioData.BGMVolume);
        //    AudioVolumes.SetSFXVolume(audioData.sfxVolume);
        AudioVolumes.SetMasterVolume(50);
        AudioVolumes.SetBGMVolume(100);
        AudioVolumes.SetSFXVolume(50);
    }
}
