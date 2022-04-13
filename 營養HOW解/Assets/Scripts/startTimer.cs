using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startTimer : MonoBehaviour
{
    public float timeLeft;
    public Text timeText;
    void Start()
    {

    }
    void Update()
    {
        // Time.timeScale=0f;
        timeText.text=timeLeft.ToString("0");
        timeLeft-=Time.deltaTime;
        if(timeLeft==0f)
        {
            Time.timeScale=1f;
            timeText.enabled=false;
        }
    }
}
