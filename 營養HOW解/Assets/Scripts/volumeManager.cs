using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class volumeManager : MonoBehaviour
{
    public AudioMixer myAudioMixer;
    public void SetVolume(float sliderValue)
    {
        Debug.Log("sliderValue");
        // myAudioMixer.SetFloat("MasterVolume",Mathf.Log10(sliderValue)*20);
        // myAudioMixer.SetFloat("MasterVolume",sliderValue);
    }
}
