using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public static class AudioVolumes
{
    public static Action<int> OnMasterVolumeChanged;
    public static Action<int> OnBGMVolumeChanged;
    public static Action<int> OnSFXVolumeChanged;
    public static int Master => _master;
    public static int BGM => _BGM;
    public static int SFX=>_sfx;

    private static int _master;
    private static int _BGM=100;
    private static int _sfx;


    public static void SetMasterVolume(int volume)
    {
        _master = math.clamp(volume,0,100);
        OnMasterVolumeChanged?.Invoke(volume);
    }
    public static void SetBGMVolume(int volume)
    {
        _BGM = math.clamp(volume, 0, 100);
        OnBGMVolumeChanged?.Invoke(volume);
    }
    public static void SetSFXVolume(int volume)
    {
        _sfx = math.clamp(volume, 0,100);
        OnSFXVolumeChanged?.Invoke(volume);
    }

}
