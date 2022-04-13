using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmController : MonoBehaviour
{
    private void Awake()
    {
        Destroy(GameObject.FindGameObjectWithTag("sound1"));
        Destroy(GameObject.FindGameObjectWithTag("sound2"));
        Destroy(GameObject.FindGameObjectWithTag("sound3-1"));
        Destroy(GameObject.FindGameObjectWithTag("sound3-2"));
        Destroy(GameObject.FindGameObjectWithTag("sound4"));
        GameObject[] bgmObj = GameObject.FindGameObjectsWithTag("sound");
        if(bgmObj.Length>1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
