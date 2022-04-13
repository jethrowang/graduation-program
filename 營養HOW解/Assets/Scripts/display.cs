using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class display : MonoBehaviour
{
    private Animator anim;
    public GameObject player;
    void Start()
    {
        anim=GetComponent<Animator>();
    }
    void Update()
    {
        transform.position = new Vector2(player.transform.position.x,player.transform.position.y+1.5f);
    }
    void Idle()
    {
        anim.Play("idle");
    }
}
