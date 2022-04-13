using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logo : MonoBehaviour
{
    private Animator anim;   
    public float timeLeft,time; 
    void Start()
    {
        anim=GetComponent<Animator>();
        timeLeft=time;
    }
    void Update()
    {
        timeLeft-=Time.deltaTime;
        if(timeLeft<=0f)
        {
            anim.Play("logo");
            timeLeft=time;
        }
    }
    void LogoIdle()
    {
        anim.Play("logoIdle");
    }

}
