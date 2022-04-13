using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuAnim : MonoBehaviour
{
    private Animator anim;
    public float timer,timeLeft;
    private Vector3 lastMouseCoordinate = Vector3.zero;
    void Start()
    {
        anim=GetComponent<Animator>();
        timeLeft=timer;
    }
    void Update()
    {
        Vector3 mouseDelta = Input.mousePosition - lastMouseCoordinate;
        timeLeft-=Time.deltaTime;
        if (mouseDelta.x!=0f || mouseDelta.y!=0f)
        {
            anim.Play("notIdle");
            timeLeft=timer;
        }
        if(timeLeft<=0f)
        {
            timeLeft=0f;
            anim.Play("idle");
        }
        lastMouseCoordinate = Input.mousePosition;
    }
}
