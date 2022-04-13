using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class video : MonoBehaviour
{
    VideoPlayer VP=new VideoPlayer();
    public GameObject playbutton,pausebutton;
    void Start()
    {
        VP=GetComponent<VideoPlayer>();
        VP.loopPointReached+=EndReached;
    }
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadNextLevel();
    }
    public void Play()
    {
        playbutton.SetActive(false);
        pausebutton.SetActive(true);
        VP.Play();
    }
    public void Pause()
    {
        pausebutton.SetActive(false);
        playbutton.SetActive(true);
        VP.Pause();
    }
}
