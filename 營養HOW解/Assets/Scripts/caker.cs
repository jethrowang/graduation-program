using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class caker : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    public bool faceleft=true;
    public float speed;
    public float jumpForce;
    static public int hp;
    public int max_hp;
    public GameObject hpbar;
    public GameObject player;
    public GameObject door;
    private float distance;
    public float attackrange;
    public bool isHurt;
    public float hurtForce;
    public GameObject cake;
    public Transform firepoint;
    public float invincibleTime,invincibleTimer;
    public bool isInvincible;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        coll=GetComponent<Collider2D>();
        hp=max_hp;
    }

    void FixedUpdate()
    {
        if(!isHurt)
        {
            Movement();
        }
        SwitchAnim();
    }

    void Update()
    {
        Hpfunction();
        Invinsible();
    }

    void Movement()
    {
        distance=Vector2.Distance(player.transform.position,transform.position);
        if(distance>attackrange)
        {
            anim.SetFloat("walking",0f);
        }else
        {
            if(faceleft)//面左
            {
                if(player.transform.position.x<transform.position.x)
                {
                    anim.SetFloat("walking",1f);
                    rb.velocity=new Vector2(-speed,0f);
                }else if(player.transform.position.x>transform.position.x)
                {
                    // transform.localScale=new Vector3(-0.3f,0.3f,0.3f);
                    Flip();
                    faceleft=false;
                    anim.SetFloat("walking",1f);
                    rb.velocity=new Vector2(speed,0f);
                }
            }else//面右
            {
                if(player.transform.position.x>transform.position.x)
                {
                    anim.SetFloat("walking",1f);
                    rb.velocity=new Vector2(speed,0f);
                }else if(player.transform.position.x<transform.position.x)
                {
                    Flip();
                    faceleft=true;
                    anim.SetFloat("walking",1f);
                    rb.velocity=new Vector2(-speed,0f);
                }
            }
        }
    }

    void Flip()
    {
        faceleft=!faceleft;
        transform.Rotate(0f, 180f, 0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="bullet")
        {
            if (collision.contacts[0].normal.x == -1)//左边碰撞
            {
                rb.velocity=new Vector2(-hurtForce,rb.velocity.y);
                Hurt();
            }
            else if (collision.contacts[0].normal.x == 1)//右边碰撞
            {
                rb.velocity=new Vector2(hurtForce,rb.velocity.y);
                Hurt();
            }
        }
    }

    void Hurt()
    {
        if(!isInvincible)
        {
            hp-=1;
            isHurt=true;
            soundmanager.sound_instance.Hurt2audio();
            if(hp>=1)
            {
                anim.SetBool("escaping",true);
                if(transform.position.x<player.transform.position.x)
                {
                    if(faceleft)
                    {
                        Flip();
                        Invoke("Throw",1.3f);
                        Invoke("Throw",2.6f);
                    }else
                    {
                        Invoke("Throw",1.3f);
                        Invoke("Throw",2.6f);
                    }
                }else if(transform.position.x>player.transform.position.x)
                {
                    if(!faceleft)
                    {
                        Flip();
                        Invoke("Throw",1.3f);
                        Invoke("Throw",2.6f);
                    }else
                    {
                        Invoke("Throw",1.3f);
                        Invoke("Throw",2.6f);
                    }
                }
            }
        }
        Startinvincible();
        
    }

    void SwitchAnim()
    {
        if(isHurt)
        {
            if(Mathf.Abs(rb.velocity.x)<0.2f)
            {
                anim.SetBool("escaping",false);
                isHurt=false;
            }
        }
    }

    void Hpfunction()
    {
        float _percent=(float)hp/(float)max_hp;
        hpbar.transform.localScale=new Vector3(_percent,hpbar.transform.localScale.y,hpbar.transform.localScale.z);
        if(hp>max_hp)
        {
            hp=max_hp;
        }
        if(hp==0)
        {
            Invoke("Escape",0.35f);
            anim.Play("escapejump");
            Invoke("Appear",1f);
        }
    }

    void Appear()
    {
        door.SetActive(true);
    }

    void Escape()
    {
        rb.AddForce(new Vector2(0f,jumpForce),ForceMode2D.Impulse);
        GetComponent<Collider2D>().enabled=false;
        GetComponent<BoxCollider2D>().enabled=false;
        Destroy(gameObject,1f);
    }

    void Throw()
    {
        anim.SetTrigger("throw");
        Invoke("Cakeinstantiate",0.35f);
        anim.SetBool("idle",true);
    }

    void Cakeinstantiate()
    {
        Instantiate(cake, firepoint.transform.position, firepoint.rotation);
        soundmanager.sound_instance.Throwaudio();
    }

    void Startinvincible()
    {
        if(isInvincible)
        {
            return;
        }
        isInvincible=true;
        invincibleTimer=invincibleTime;
    }
    void Invinsible()
    {
        if(isInvincible)
        {
            invincibleTimer-=Time.deltaTime;
            // Shake();
            if (invincibleTimer<0)
            {
                // BoxColliderClick.enabled=true;
                isInvincible=false;
            }
        }
    }
}
