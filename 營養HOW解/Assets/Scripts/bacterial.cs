using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bacterial : enemy
{
    private Collider2D coll;
    public Transform top,buttom;
    private float topy,buttomy;
    public bool isup=true;
    protected override void Start()
    {
        base.Start();
        coll=GetComponent<Collider2D>();
        topy=top.position.y;
        buttomy=buttom.position.y;
        Destroy(top.gameObject);
        Destroy(buttom.gameObject);
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if(isup)
        {
            rb.velocity=new Vector2(rb.velocity.x,speed);
            if(transform.position.y>topy)
            {
                isup=false;
            }
        }
        else
        {
            rb.velocity=new Vector2(rb.velocity.x,-speed);
            if(transform.position.y<buttomy)
            {
                isup=true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="bullet")
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            anim.Play("gone");
            GetComponent<Collider2D>().enabled=false;
            soundmanager.sound_instance.Hurt1audio();
        }
    }
}
