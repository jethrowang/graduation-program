using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected float speed=1,jumpforce=4;
    protected virtual void Start()
    {
        anim=GetComponent<Animator>();
        rb=GetComponent<Rigidbody2D>();
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void JumpOn()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        anim.Play("gone");
        GetComponent<Collider2D>().enabled=false;
        GetComponent<BoxCollider2D>().enabled=false;
        soundmanager.sound_instance.Hurt0audio();
        FindObjectOfType<player>().Collectionscount1();
    }
}
