using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    [SerializeField] private List<AudioClip> clips;
    public static AudioManager Instance;

    public bool SFXMuted;
    public float SFXVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
        DontDestroyOnLoad(Instance);
        musicSource.volume = 0.8f;
        SFXVolume = 0.8f;
        StartCoroutine(LoopMusic());
    }

    public void SetMusicVolume(float value)
    {
        value = Mathf.Clamp01(value);
        musicSource.volume = value * value * value;
    }
    public void SetSFXVolume(float value)
    {
        value = Mathf.Clamp01(value);
        SFXVolume = value * value * value;
    }

    public void MuteUnmuteSFX(bool mute)
    {
        SFXMuted = mute;
    }

    public void MuteUnmuteMusic(bool mute)
    {
        musicSource.mute = mute;
    }

    private IEnumerator LoopMusic()
    {
        int i = 0;
        while (true)
        {
            musicSource.clip = clips[i];
            i = (i + 1) % clips.Count;
            musicSource.Play();
            //yield return null;
            yield return new WaitUntil(() => !musicSource.isPlaying);
        }
    }
}
