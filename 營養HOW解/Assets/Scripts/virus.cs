using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class virus : enemy
{
    public Collider2D coll;
    public LayerMask ground;
    public Transform leftpoint,rightpoint;
    public bool faceleft=true;
    private float leftx,rightx;
    public static int hp;
    protected override void Start()
    {
        base.Start();
        hp=1;
        leftx=leftpoint.position.x;
        rightx=rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    void FixedUpdate()
    {
        SwitchAnim();
        Movement();
    }

    void Movement()
    {
        if(faceleft)//面左
        {
            if(coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping",true);
                rb.velocity=new Vector2(-speed,jumpforce);
            }
            if(transform.position.x<leftx)
            {
                transform.localScale=new Vector3(-0.35f,0.35f,0.35f);
                faceleft=false;
            }
        }else//面右
        {
            if(coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping",true);
                rb.velocity=new Vector2(speed,jumpforce);
            }
            if(transform.position.x>rightx)
            {
                transform.localScale=new Vector3(0.35f,0.35f,0.35f);
                faceleft=true;
            }
        }
    }

    void SwitchAnim()
    {
        if(anim.GetBool("jumping"))
        {
            if(rb.velocity.y<0.1f)
            {
                anim.SetBool("jumping",false);
                anim.SetBool("falling",true);
            }
        }
        if(coll.IsTouchingLayers(ground)&&anim.GetBool("falling"))
        {
            anim.SetBool("falling",false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="bullet")
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            anim.SetTrigger("gone");
            GetComponent<Collider2D>().enabled=false;
            GetComponent<BoxCollider2D>().enabled=false;
            soundmanager.sound_instance.Hurt1audio();
        }
    }
}
