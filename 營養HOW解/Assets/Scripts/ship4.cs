using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ship4 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 horizontalMove,verticalMove;
    public bool facing_right=true;
    public float speed,force;
    public float hurtforce;
    public float timeLeft;
    public Text timeText;
    public GameObject timeTextObject;
    public GameObject loseDialog;
    public GameObject door;
    public GameObject[] timeImage;
    public bool passLevel;
    private PlayerInputActions controls;
    public GameObject water;
    private void Awake()
    {
        controls = new PlayerInputActions();
        controls.Player.Move.performed += ctx => horizontalMove = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => horizontalMove = Vector2.zero;
        controls.Player.Move.performed += ctx => verticalMove = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => verticalMove = Vector2.zero;
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
        // Time.timeScale=1f;
    }

    void FixedUpdate()
    {
        if(timeLeft>0f)
        {
            Movement();
        }
    }
    void Update()
    {
        Timer();
        Bigger();
    }

    void GoGo()
    {
        this.gameObject.transform.position=new Vector2(water.transform.position.x,water.transform.position.y);
    }

    void Timer()
    {
        timeText.text=timeLeft.ToString("F0");
        if(timeLeft>0f)
        {
            timeLeft-=Time.deltaTime;
            if(timeLeft<=5f)
            {
                // soundmanager.sound_instance.CounterAudio();
            }
            if(timeLeft<5.5f&&timeLeft>=4.5f)
            {
                timeImage[4].SetActive(true);
                timeImage[3].SetActive(false);
                timeImage[2].SetActive(false);
                timeImage[1].SetActive(false);
                timeImage[0].SetActive(false);
            }else if(timeLeft<4.5f&&timeLeft>=3.5f)
            {
                timeImage[4].SetActive(false);
                timeImage[3].SetActive(true);
                timeImage[2].SetActive(false);
                timeImage[1].SetActive(false);
                timeImage[0].SetActive(false);
            }else if(timeLeft<3.5f&&timeLeft>=2.5f)
            {
                timeImage[4].SetActive(false);
                timeImage[3].SetActive(false);
                timeImage[2].SetActive(true);
                timeImage[1].SetActive(false);
                timeImage[0].SetActive(false);
            }else if(timeLeft<2.5f&&timeLeft>=1.5f)
            {
                timeImage[4].SetActive(false);
                timeImage[3].SetActive(false);
                timeImage[2].SetActive(false);
                timeImage[1].SetActive(true);
                timeImage[0].SetActive(false);
            }else if(timeLeft<1.5f&&timeLeft>=0.5f)
            {
                timeImage[4].SetActive(false);
                timeImage[3].SetActive(false);
                timeImage[2].SetActive(false);
                timeImage[1].SetActive(false);
                timeImage[0].SetActive(true);
            }else
            {
                timeImage[4].SetActive(false);
                timeImage[3].SetActive(false);
                timeImage[2].SetActive(false);
                timeImage[1].SetActive(false);
                timeImage[0].SetActive(false);
            }
        }else
        {
            water.GetComponent<BoxCollider2D>().enabled=false;
            timeLeft=0f;
            loseDialog.SetActive(true);
        }
    }

    void Movement()
    {
        // horizontalMove=Input.GetAxis("Horizontal");
        // verticalMove=Input.GetAxis("Vertical");
        if(horizontalMove.x!=0f)
        {
            // rb.velocity=new Vector2(horizontalMove.x*speed,rb.velocity.y);
            rb.AddForce(new Vector2(horizontalMove.x*force, 0f),ForceMode2D.Force);
            if(Mathf.Abs(rb.velocity.x) >= Mathf.Abs(horizontalMove.x*speed))
            {
                rb.velocity=new Vector2(horizontalMove.x*speed,rb.velocity.y);
            }
        }
        if(horizontalMove.x>0f&&!facing_right)
        {
            Flip();
        }else if(horizontalMove.x<0f&&facing_right)
        {
            Flip();
        }
        if(verticalMove.y!=0f)
        {
            // rb.velocity=new Vector2(rb.velocity.x,verticalMove.y*speed);
            rb.AddForce(new Vector2(0f, verticalMove.y*force),ForceMode2D.Force);
            if(Mathf.Abs(rb.velocity.y) >= Mathf.Abs(verticalMove.y*speed))
            {
                rb.velocity=new Vector2(rb.velocity.x,verticalMove.y*speed);
            }
        }
    }

    void Flip()
    {
        facing_right=!facing_right;
        transform.Rotate(0f, 180f, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="water4-1")
        {
            timeTextObject.SetActive(false);
            timeLeft=20;
            soundmanager.sound_instance.SwimAudio();
            anim.Play("smaller");
            Invoke("NextShip1",0.8f);
            Invoke("NextScene",1.5f);
        }
        if(collision.tag=="water4-2")
        {
            timeTextObject.SetActive(false);
            timeLeft=20;
            soundmanager.sound_instance.SwimAudio();
            anim.Play("smaller");
            Invoke("NextShip1",0.8f);
            door.SetActive(true);
        }
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

    void NextScene()
    {
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadNextLevel();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="wall")
        {
            soundmanager.sound_instance.CrackAudio();
            if (collision.contacts[0].normal.y == -1)//从上方碰撞
            {
                Debug.Log("up");
                rb.AddForce(new Vector2(0f,-hurtforce),ForceMode2D.Impulse);
            }
            else if(collision.contacts[0].normal.y == 1)//从下方碰撞
            {
                Debug.Log("down");
                rb.AddForce(new Vector2(0f,hurtforce),ForceMode2D.Impulse);
            }
            else if (collision.contacts[0].normal.x == -1)//左边碰撞
            {
                Debug.Log("right");
                rb.AddForce(new Vector2(-hurtforce,0f),ForceMode2D.Impulse);
            }
            else if (collision.contacts[0].normal.x == 1)//右边碰撞
            {
                Debug.Log("left");
                rb.AddForce(new Vector2(hurtforce,0f),ForceMode2D.Impulse);
            }
        }
    }

    void Bigger()
    {
        if(timeLeft>0f)
        {
            if(timeLeft<=15f&&timeLeft>=14.5f)
            {
                soundmanager.sound_instance.BiggerAudio();
                anim.Play("bigger");
                Invoke("NextShip2",0.8f);
            }
            else if(timeLeft<=10f&&timeLeft>=9.5f)
            {
                soundmanager.sound_instance.BiggerAudio();
                anim.Play("bigger2");
                Invoke("NextShip3",0.8f);
            }
            else if(timeLeft<=5f&&timeLeft>=4.5f)
            {
                soundmanager.sound_instance.BiggerAudio();
                anim.Play("bigger3");
                Invoke("NextShip4",0.8f);
            }
        }
    }
    void NextShip1()
    {
        anim.Play("idle");
    }
    void NextShip2()
    {
        anim.Play("idle2");
    }
    void NextShip3()
    {
        anim.Play("idle3");
    }
    void NextShip4()
    {
        anim.Play("idle4");
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