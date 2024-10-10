using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] Slider _masterVolumeSlider;
    [SerializeField] Slider _BGMVolumeSlider;
    [SerializeField] Slider _sfxVolumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMasterVolumee(float volume)=> AudioVolumes.SetMasterVolume(((int)volume));
    public void SetBGMVolume(float volume) => AudioVolumes.SetBGMVolume(((int)volume));
    public void SetSfxVolume(float volume) =>AudioVolumes.SetSFXVolume(((int)volume));
}
