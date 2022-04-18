using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class volumeManager : MonoBehaviour
{
    [SerializeField]private AudioMixer myAudioMixer;
    public void SetVolume(float sliderValue)
    {
        myAudioMixer.SetFloat("MasterVolume",Mathf.Log10(sliderValue)*20);
        Debug.Log(Mathf.Log10(sliderValue)*20);
    }
}
