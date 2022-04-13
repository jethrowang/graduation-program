using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleAnim : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim=GetComponent<Animator>();
    }

    void Idle()
    {
        anim.Play("idle");
    }
}
