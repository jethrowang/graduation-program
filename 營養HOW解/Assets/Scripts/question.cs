using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class question : MonoBehaviour
{
    public GameObject questions,answers,correct,wrong1,wrong2;
    public GameObject flag,newFlag;
    public GameObject black;
    public void Wrong1()
    {
        soundmanager.sound_instance.Wrongaudio();
        wrong1.SetActive(true);
    }

    public void Wrong2()
    {
        soundmanager.sound_instance.Wrongaudio();
        wrong2.SetActive(true);
    }

    public void WrongFlag()
    {
        soundmanager.sound_instance.Wrongaudio();
        correct.SetActive(true);
        flag.SetActive(false);

    }

    public void Skip()
    {
        questions.SetActive(false);
        answers.SetActive(false);
        black.SetActive(false);
        Time.timeScale=1f;
    }

    public void Correct()
    {
        soundmanager.sound_instance.Correctaudio();
        correct.SetActive(true);
        FindObjectOfType<player>().Collectionscount2();
        FindObjectOfType<player>().Display2();
    }

    public void CorrectFlag()
    {
        soundmanager.sound_instance.Correctaudio();
        correct.SetActive(true);
        GameObject.Find("ship3-2").GetComponent<ship>().CollectionsPlus();
        flag.SetActive(false);
        newFlag.SetActive(true);
    }
}
