using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmController3_2 : MonoBehaviour
{
    private void Awake()
    {
        Destroy(GameObject.FindGameObjectWithTag("sound"));
        GameObject[] bgmObj = GameObject.FindGameObjectsWithTag("sound3-2");
        if(bgmObj.Length>1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
