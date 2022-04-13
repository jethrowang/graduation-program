using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enterdialog : MonoBehaviour
{
    public GameObject dialog;
    public GameObject treasure,treasure_open;
    public GameObject black;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="player"||collision.gameObject.tag=="ball")
        {
            soundmanager.sound_instance.Openaudio();
            treasure_open.SetActive(true);
            treasure.SetActive(false);
            Time.timeScale=0f;
            dialog.SetActive(true);
            black.SetActive(true);
        }
    }
}
