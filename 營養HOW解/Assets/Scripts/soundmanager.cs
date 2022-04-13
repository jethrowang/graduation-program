using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundmanager : MonoBehaviour
{
    public static soundmanager sound_instance;
    public AudioSource audioSource;
    [Header("介面")]
    public AudioClip hoverAudio;
    public AudioClip pressAudio;
    public AudioClip correctAudio;
    public AudioClip wrongAudio;
    public AudioClip warningAudio;
    [Header("玩家")]
    public AudioClip jumpAudio;
    public AudioClip hurtAudio;
    public AudioClip throwAudio;
    public AudioClip swallowAudio;
    public AudioClip heartAudio;
    public AudioClip biggerAudio;
    public AudioClip crackAudio;
    public AudioClip swimAudio;
    [Header("口水")]
    public AudioClip floorAudio;
    public AudioClip attackAudio;
    [Header("寶箱")]
    public AudioClip openAudio;
    public AudioClip flagAudio;
    [Header("敵人")]
    public AudioClip hurt0Audio;
    public AudioClip hurt1Audio;
    public AudioClip hurt2Audio;
    public AudioClip cakeAudio;
    public AudioClip counterAudio;
    public AudioClip doorAudio;
    
    private void Awake()
    {
        sound_instance=this;
    }
    public void Jumpaudio()
    {
        audioSource.clip=jumpAudio;
        audioSource.Play();
    }
    public void Hurtaudio()
    {
        audioSource.clip=hurtAudio;
        audioSource.Play();
    }
    public void Throwaudio()
    {
        audioSource.clip=throwAudio;
        audioSource.Play();
    }
    public void Flooraudio()
    {
        audioSource.clip=floorAudio;
        audioSource.Play();
    }
    public void Attackaudio()
    {
        audioSource.clip=attackAudio;
        audioSource.Play();
    }
    public void Openaudio()
    {
        audioSource.clip=openAudio;
        audioSource.Play();
    }
    public void Hoveraudio()
    {
        audioSource.clip=hoverAudio;
        audioSource.Play();
    }

    public void Pressaudio()
    {
        audioSource.clip=pressAudio;
        audioSource.Play();
    }
    public void Correctaudio()
    {
        audioSource.clip=correctAudio;
        audioSource.Play();
    }
    public void Wrongaudio()
    {
        audioSource.clip=wrongAudio;
        audioSource.Play();
    }
    public void Hurt0audio()
    {
        audioSource.clip=hurt0Audio;
        audioSource.Play();
    }
    public void Hurt1audio()
    {
        audioSource.clip=hurt1Audio;
        audioSource.Play();
    }
    public void Hurt2audio()
    {
        audioSource.clip=hurt2Audio;
        audioSource.Play();
    }
    public void Cakeaudio()
    {
        audioSource.clip=cakeAudio;
        audioSource.Play();
    }
    public void SwallowAudio()
    {
        audioSource.clip=swallowAudio;
        audioSource.Play();
    }
    public void HeartAudio()
    {
        audioSource.clip=heartAudio;
        audioSource.Play();
    }
    public void FlagAudio()
    {
        audioSource.clip=flagAudio;
        audioSource.Play();
    }
    public void BiggerAudio()
    {
        audioSource.clip=biggerAudio;
        audioSource.Play();
    }
    public void WarningAudio()
    {
        audioSource.clip=warningAudio;
        audioSource.Play();
    }
    public void CrackAudio()
    {
        audioSource.clip=crackAudio;
        audioSource.Play();
    }
    public void SwimAudio()
    {
        audioSource.clip=swimAudio;
        audioSource.Play();
    }
    public void CounterAudio()
    {
        audioSource.clip=counterAudio;
        audioSource.Play();
    }
    public void DoorAudio()
    {
        audioSource.clip=doorAudio;
        audioSource.Play();
    }
}
