using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmmanager : MonoBehaviour
{
    public static bgmmanager bgm_instance;
    public AudioSource audioSource;
    [Header("BGM")]
    public AudioClip bgmAudio;
    public AudioClip bossAudio;
    private void Awake()
    {
        bgm_instance=this;
    }

    void Start()
    {
        BGMaudio();
    }

    public void BGMaudio()
    {
        audioSource.clip=bgmAudio;
        audioSource.Play();
    }
    public void Bossaudio()
    {
        audioSource.clip=bossAudio;
        audioSource.Play();
    }
}
