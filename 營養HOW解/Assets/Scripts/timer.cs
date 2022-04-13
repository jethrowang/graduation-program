using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
    void Start()
    {
        Time.timeScale=0f;
    }
    void Des()
    {
        Destroy(gameObject);
        Time.timeScale=1f;
    }
}
