using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ship : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim,collectionAnim,hpAnim,winAnim;
    public bool ishurt=false;
    private Vector2 horizontalMove;
    public float speed;
    public float accelerate;
    public float decelerate;
    public float jumpForce;
    public float hurtForce;
    public int collections;
    public Text collectionsnum;
    public int hp;
    public int max_hp;
    public GameObject hp1_red,hp1,hp2,hp3,hp4,hp5;
    public float invincibleTime;
    public float invincibleTimer;
    static public bool isInvincible;
    private float shake;
    private SpriteRenderer BoxColliderClick;
    public GameObject loseDialog;
    public bool passLevel;
    public GameObject door;
    private PlayerInputActions controls;
    private void Awake()
    {
        controls = new PlayerInputActions();
        controls.Player.Move.performed += ctx => horizontalMove = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => horizontalMove = Vector2.zero;
        controls.Player.Jump.performed += ctx => Jump();
        controls.Player.Jump.canceled += ctx => JumpCancel();
        controls.Player.GoGo.started += ctx => GoGo();
    }
    void OnEnable()
    {
        controls.Player.Enable();
    }
    void OnDisable()
    {
        controls.Player.Disable();
    }

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        hp = max_hp;
        BoxColliderClick=GetComponent<SpriteRenderer>();
        collections=0;
        collectionAnim=GameObject.Find("collectionsnum").GetComponent<Animator>();
        hpAnim=GameObject.Find("hpbar_bottom").GetComponent<Animator>();
        winAnim=GameObject.Find("win").GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(hp>0)
        {
            Movement();
            // Jump();
        }
    }

    void Update()
    {
        Hpfunction();
        Collectionsnum();
        Invinsible();
    }

    void GoGo()
    {
        hp=max_hp;
        collectionAnim.SetTrigger("plus");
        collections+=10;
        this.gameObject.transform.position=new Vector2(78f,35f);
    }

    void Movement()
    {
        if(horizontalMove.x!=0f)
        {
            if(horizontalMove.x>0f)
            {
                rb.velocity=new Vector2(accelerate*speed*Time.fixedDeltaTime,rb.velocity.y);
            }else if(horizontalMove.x<0f)
            {
                rb.velocity=new Vector2(decelerate*speed*Time.fixedDeltaTime,rb.velocity.y);
            }
        }else
        {
            rb.velocity=new Vector2(speed*Time.fixedDeltaTime,rb.velocity.y);
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f,jumpForce),ForceMode2D.Impulse);
    }
    void JumpCancel()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="door")
        {
            soundmanager.sound_instance.DoorAudio();
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            passLevel=true;
            anim.Play("fadeout");
            Invoke("GotoMenu",0.8f);
        }
    }

    void GotoMenu()
    {
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadSomeLevel(2);
    }

    void Lose()
    {
        hp=0;
        loseDialog.SetActive(true);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag=="bosscorner")
        {
            if(collections<=7)
            {
                Invoke("Lose",1.5f);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="enemy")
        {
            Hurt();
            if(collision.gameObject.transform.position.y>this.transform.position.y)
            {
                rb.AddForce(new Vector2(0f,-hurtForce),ForceMode2D.Impulse);
            }else if(collision.gameObject.transform.position.y<this.transform.position.y)
            {
                rb.AddForce(new Vector2(0f,hurtForce),ForceMode2D.Impulse);
            }
        }
    }

    void Hurt()
    {
        if(!isInvincible)
        {
            ishurt=true;
            soundmanager.sound_instance.Hurtaudio();
            hp-=1;
        }
        Startinvincible();
    }

    void Hpfunction()
    {
        if(hp>=5)
        {
            hp=5;
            hpAnim.Play("idle");
        }
        if(hp==4)
        {
            hpAnim.Play("hp5");
        }
        if(hp==3)
        {
            hpAnim.Play("hp4");
        }
        if(hp==2)
        {
            hpAnim.Play("hp3");
        }
        if(hp==1)
        {
            hpAnim.Play("hp2");
        }
        if(hp<=0)
        {
            hpAnim.Play("hp1");
            Invoke("Lose",1.5f);
        }
    }

    void Collectionsnum()
    {
        collectionsnum.text=collections.ToString();
        if(collections>=8)
        {
            door.SetActive(true);
            winAnim.Play("win");
        }
    }

    public void CollectionsPlus()
    {
        collectionAnim.SetTrigger("plus");
        collections+=1;
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
            Shake();
            if (invincibleTimer<0)
            {
                BoxColliderClick.enabled=true;
                isInvincible=false;
            }
        }
    }
    void Shake()
    {
        shake+=Time.deltaTime;
        if (shake%0.2f>0.1f)
        {
            BoxColliderClick.enabled=true;
        }
        else
        {
            BoxColliderClick.enabled=false;
        }
    }

    public bool Pass()
    {
        if(passLevel)
        {
            return true;
        }else
        {
            return false;
        }
    }
}
