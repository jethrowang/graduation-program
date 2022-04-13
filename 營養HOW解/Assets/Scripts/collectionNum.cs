using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectionNum : MonoBehaviour
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
