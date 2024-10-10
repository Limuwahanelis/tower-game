using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioEvent _musicToPlay;
    public static BGMPlayer _instance;
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            AudioVolumes.OnMasterVolumeChanged += UpdateVolume;
            DontDestroyOnLoad(gameObject.transform.parent);
        }
        else Destroy(gameObject);
    }
    private void Start()
    {
        if (_audioSource == null) _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        Play();
    }
    public void Play()
    {
        _musicToPlay.Play(_audioSource);
    }
    private void UpdateVolume(int volume)
    {
        _audioSource.volume = (AudioVolumes.Master / 100.0f) * (AudioVolumes.BGM / 100.0f);
    }
    private void OnValidate()
    {
        if (_audioSource == null) _audioSource = GetComponent<AudioSource>();
    }
    private void OnDestroy()
    {
        AudioVolumes.OnMasterVolumeChanged -= UpdateVolume;
    }
}
