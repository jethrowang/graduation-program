using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerSpare : MonoBehaviour
{
    private Cinemachine.CinemachineCollisionImpulseSource MyInpulse;
    private Rigidbody2D rb;
    private Animator anim;
    [Header("狀態")]
    public bool facing_right=true;
    public bool ishurt;
    public bool iscrouch;
    public bool isGround;
    public bool isjump;
    public bool jumpPressed,jumpHeld,crouchHeld;
    [Header("環境")]
    public Transform ceilingCheck,groundCheck;
    public LayerMask ground;
    public LayerMask acid;
    [Header("移動")]
    private float horizontalmove;
    public float speed;
    public float crouchspeedDivisor;
    [Header("跳躍")]
    public float jumpforce;
    public float jumpholdforce;
    public float jumptime;
    public float jumpholdduration;
    public float crouchjumpboost;
    public float bumpforce;
    public int extraJump;
    public float hurtforce;
    [Header("收集")]
    public int collections;
    public Text collectionsnum;
    [Header("子彈")]
    public GameObject bullet;
    public Transform firepoint;
    public float firerate; //firerate秒實例化一個子彈
    public float nextfire;
    [Header("血條")]
    public int hp;
    public int max_hp;
    public GameObject hp1_red,hp1,hp2,hp3,hp4,hp5;
    public GameObject display1,display2;
    [Header("無敵")]
    public float invincibleTime;
    public float invincibleTimer;
    static public bool isInvincible;
    private float shake;
    private SpriteRenderer BoxColliderClick;
    [Header("材質")]
    public PhysicsMaterial2D slopePhysicsMaterial2D;
    public PhysicsMaterial2D nofrictionPhysicsMaterial2D;
    [Header("物件")]
    public GameObject loseDialog;
    public GameObject[] bossFloor;
    public GameObject lastEnemy;
    [Header("Dash參數")]
    public float dashTime;//dash時長
    private float dashTimeLeft;//衝鋒剩餘時間
    private float lastDash=-10f;//上一次dash時間點
    public float dashCoolDown;
    public float dashSpeed;
    public bool isDashing;
    [Header("CD的UI組件")]
    public Image cdImage;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        MyInpulse = GetComponent<Cinemachine.CinemachineCollisionImpulseSource>();
        hp = max_hp;
        BoxColliderClick=GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        isGround=Physics2D.OverlapCircle(groundCheck.position,0.2f,ground)||Physics2D.OverlapCircle(groundCheck.position,0.2f,acid);
        if(!ishurt)
        {
            Throw();
        }
        Dash();
        if(isDashing)
        {
            return;
        }
        Movement();
        Jump2();
        Crouch();
        SwitchAnim();
    }

    void Update()
    {
        jumpPressed=Input.GetButtonDown("Jump");
        jumpHeld=Input.GetButton("Jump");
        crouchHeld=Input.GetButton("Crouch");
        if(Input.GetKeyDown(KeyCode.J))
        {
            if(Time.time>=lastDash+dashCoolDown)
            {
                ReadyToDash();//可執行dash
            }
        }
        cdImage.fillAmount-=1.0f/dashCoolDown*Time.deltaTime;
        Hpfunction();
        Collectionsnum();
        Invinsible();
    }

    //移動
    void Movement()
    {
        horizontalmove=Input.GetAxis("Horizontal");
        float facedirection=Input.GetAxisRaw("Horizontal");
        //移動
        if(horizontalmove!=0f)
        {
            if(iscrouch)
            {
                horizontalmove/=crouchspeedDivisor;
                rb.velocity=new Vector2(horizontalmove*speed*Time.fixedDeltaTime,rb.velocity.y);
                anim.SetFloat("running",Mathf.Abs(facedirection));
            }else
            {
                rb.velocity=new Vector2(horizontalmove*speed*Time.fixedDeltaTime,rb.velocity.y);
                anim.SetFloat("running",Mathf.Abs(facedirection));
            }
        }
        RaycastHit2D hit=Physics2D.Raycast(transform.position,Vector2.down,1f,ground);
        if(isGround&&horizontalmove==0f)
        {
            rb.sharedMaterial=slopePhysicsMaterial2D;
        }else
        {
            rb.sharedMaterial=nofrictionPhysicsMaterial2D;
        }
        //方向
        if(facedirection>0f&&!facing_right)
        {
            Flip();
        }else if(facedirection<0f&&facing_right)
        {
            Flip();
        }
    }

    //蹲下
    void Crouch()
    {
        if(!Physics2D.OverlapCircle(ceilingCheck.position,0.2f,ground))
        {
            if(Input.GetButton("Crouch"))
            {
                iscrouch=true;
                GetComponent<CapsuleCollider2D>().offset = new Vector2(0.1f, -1.21f);
                GetComponent<CapsuleCollider2D>().size = new Vector2(2f, 6.07f);
                anim.SetBool("crouching",true);
            }else
            {
                iscrouch=false;
                GetComponent<CapsuleCollider2D>().offset = new Vector2(0.09f, -0.26f);
                GetComponent<CapsuleCollider2D>().size = new Vector2(2f, 7.98f);
                anim.SetBool("crouching",false);
            }
        }
    }

    //跳躍(二段)
    void Jump1()
    {
        if(isGround)
        {
            extraJump=1;
        }
        if(Input.GetButtonDown("Jump")&&extraJump>0)
        {
            rb.velocity=Vector2.up*jumpforce;
            extraJump--;
            soundmanager.sound_instance.Jumpaudio();
            anim.SetBool("jumping",true);
        }
        if(Input.GetButtonDown("Jump")&&extraJump==0&&isGround)
        {
            rb.velocity=Vector2.up*jumpforce;
            soundmanager.sound_instance.Jumpaudio();
            anim.SetBool("jumping",true);
        }
    }

    //跳躍(長按)
    void Jump2()
    {
        if(jumpPressed&&isGround&&!isjump)
        {
            if(iscrouch&&isGround)
            {
                soundmanager.sound_instance.Jumpaudio();
                anim.SetBool("jumping",true);
                rb.AddForce(new Vector2(0f,crouchjumpboost),ForceMode2D.Impulse);
            }
            isGround=false;
            isjump=true;
            jumptime=Time.time+jumpholdduration;
            soundmanager.sound_instance.Jumpaudio();
            anim.SetBool("jumping",true);
            rb.AddForce(new Vector2(0f,jumpforce),ForceMode2D.Impulse);
        }else if(isjump)
        {
            if(jumpHeld)
            {
                rb.AddForce(new Vector2(0f,jumpholdforce),ForceMode2D.Impulse);
            }
            if(jumptime<Time.time)
            {
                isjump=false;
            }
        }
    }

    //翻轉
    void Flip()
    {
        facing_right=!facing_right;
        transform.Rotate(0f, 180f, 0f);
    }
    
    //切換動畫
    void SwitchAnim()
    {
        if(rb.velocity.y<0.1f&&!isGround)
        {
            anim.SetBool("falling",true);
        }
        if(anim.GetBool("jumping"))
        {
            if(rb.velocity.y<0f)
            {
                anim.SetBool("jumping",false);
                anim.SetBool("falling",true);
            }
        }else if(ishurt)
        {
            anim.SetBool("hurt",true);
            anim.SetFloat("running",0f);
            if(Mathf.Abs(rb.velocity.x)<0.5f)
            {
                anim.SetBool("hurt",false);
                ishurt=false;
                anim.SetBool("idle",true);
            }
        }else if(isGround)
        {
            anim.SetBool("falling",false);
            anim.SetBool("idle",true);
        }
    }

    //觸發
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="bosscorner")
        {
            bgmmanager.bgm_instance.Bossaudio();
            collision.GetComponent<BoxCollider2D>().offset = new Vector2(5.32f, 8.36f);
            collision.GetComponent<BoxCollider2D>().size = new Vector2(104.39f, 82.57f);
            Destroy(lastEnemy);
            Invoke("BossFloor",1f);
        }
        if(collision.tag=="door")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        if(collision.tag=="heart")
        {
            if(hp<=4)
            {
                hp+=1;
                Destroy(collision.gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }

    void BossFloor()
    {
        for(int i=0;i<=bossFloor.Length-1;i++)
            {
                bossFloor[i].SetActive(true);
            }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag=="bosscorner")
        {
            if(collections==0)
            {
                if(caker.hp>=3)
                {
                    Invoke("Lose",1.5f);
                }
            }
        }
    }

    void Lose()
    {
        loseDialog.SetActive(true);
        Time.timeScale=0f;
    }

    //碰撞敵人
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="saliva")
        {
            ishurt=true;
            Hurt();
        }
        if(collision.gameObject.tag=="enemy")
        {
            // if(virus.hp==1||bacterial.hp==1)
            {
                enemy enemy=collision.gameObject.GetComponent<enemy>();
                if(collision.contacts[0].normal.y == 1)//从下方碰撞
                {
                    enemy.JumpOn();
                    rb.velocity=new Vector2(rb.velocity.x,bumpforce*Time.deltaTime);
                    anim.SetBool("jumping",true);
                    Display1();
                }else if(transform.position.x<collision.transform.position.x)
                {
                    Hurt();
                    rb.AddForce(new Vector2(-hurtforce,0f),ForceMode2D.Impulse);
                }else if(transform.position.x>collision.transform.position.x)
                {
                    Hurt();
                    rb.AddForce(new Vector2(hurtforce,0f),ForceMode2D.Impulse);
                }
            }
        }
        if(collision.gameObject.tag=="boss")
        {
            if(transform.position.x<=collision.transform.position.x)
            {
                Hurt();
                rb.AddForce(new Vector2(-hurtforce,0f),ForceMode2D.Impulse);
            }else if(transform.position.x>collision.transform.position.x)
            {
                Hurt();
                rb.AddForce(new Vector2(hurtforce,0f),ForceMode2D.Impulse);
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="acid")
        {
            Hurt();
        }
    }

    //受傷
    void Hurt()
    {
        if(!isInvincible)
        {
            ishurt=true;
            soundmanager.sound_instance.Hurtaudio();
            // MyInpulse.GenerateImpulse();
            hp -= 1;
        }
        Startinvincible();
    }

    //血量
    void Hpfunction()
    {
        if(hp>=5)
        {
            hp=5;
            hp5.SetActive(true);
            hp4.SetActive(true);
            hp3.SetActive(true);
            hp2.SetActive(true);
            hp1.SetActive(true);
        }
        if(hp==4)
        {
            hp5.SetActive(false);
            hp4.SetActive(true);
            hp3.SetActive(true);
            hp2.SetActive(true);
            hp1.SetActive(true);
        }
        if(hp==3)
        {
            hp5.SetActive(false);
            hp4.SetActive(false);
            hp3.SetActive(true);
            hp2.SetActive(true);
            hp1.SetActive(true);
        }
        if(hp==2)
        {
            hp5.SetActive(false);
            hp4.SetActive(false);
            hp3.SetActive(false);
            hp2.SetActive(true);
            hp1.SetActive(true);
        }
        if(hp==1)
        {
            hp5.SetActive(false);
            hp4.SetActive(false);
            hp3.SetActive(false);
            hp2.SetActive(false);
            hp1.SetActive(false);
            hp1_red.SetActive(true);
        }
        if(hp<=0)
        {
            hp5.SetActive(false);
            hp4.SetActive(false);
            hp3.SetActive(false);
            hp2.SetActive(false);
            hp1.SetActive(false);
            hp1_red.SetActive(false);
            Invoke("Lose",1.5f);
        }
    }

    //射擊
    public void Throw()
    {
        if(collections>=1)
        {
            if (Input.GetButtonDown("Throw"))
            {
                if(Time.time>nextfire) //讓子彈發射有間隔
                {
                    anim.SetTrigger("throw");
                    nextfire = Time.time + firerate; //Time.time表示從遊戲開發到現在的時間，會隨着遊戲的暫停而停止計算
                    Invoke("Bulletinstantiate",0.5f);
                    collections--;
                    anim.SetBool("idle",true);
                }
            }
        }
    }

    //子彈生成
    void Bulletinstantiate()
    {
        Instantiate(bullet, firepoint.transform.position, firepoint.rotation);
        soundmanager.sound_instance.Throwaudio();
    }

    //收集計算
    public void Collectionscount1()
    {
        collections+=1;
    }
    public void Collectionscount2()
    {
        collections+=2;
    }

    //收集計數
    void Collectionsnum()
    {
        collectionsnum.text=collections.ToString();
    }

    //顯示消化液增加
    void Display1()
    {
        display1.SetActive(true);
        Invoke("dontdisplay",1f);
    }
    public void Display2()
    {
        display2.SetActive(true);
        Invoke("dontdisplay",1f);
    }
    void dontdisplay()
    {
        display1.SetActive(false);
        display2.SetActive(false);
    }

    //無敵
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
        //Debug.Log(shake);
        //取余运算，结果是0到被除数之间的值
        //如果除数是1 1.1 1.2 1.3 1.4 1.5 1.6 
        //那么余数是0 0.1 0.2 0.3 0.4 0.5 0.6
        if (shake%0.2f>0.1f)
        {
            BoxColliderClick.enabled=true;
        }
        else
        {
            BoxColliderClick.enabled=false;
        }
    }

    //防止斜坡滑動
    void Slope()
    {
        RaycastHit2D hit=Physics2D.Raycast(transform.position,Vector2.down,1f,ground);
        if(hit.normal.x!=0f)
        {
            rb.AddForce(hit.normal*Vector2.Dot(Physics2D.gravity,hit.normal)-Physics2D.gravity);
        }
    }

    void ReadyToDash()
    {
        isDashing=true;
        dashTimeLeft=dashTime;
        lastDash=Time.time;
        cdImage.fillAmount=1;
    }

    void Dash()
    {
        if(isDashing)
        {
            if(dashTimeLeft>0)
            {
                if(rb.velocity.y>0&&!isGround)
                {
                    rb.velocity=new Vector2(dashSpeed*horizontalmove,jumpforce);
                }
                rb.velocity=new Vector2(dashSpeed*horizontalmove,rb.velocity.y);
                dashTimeLeft-=Time.deltaTime;
                shadowPool.instance.GetFromPool();
            }
            if(dashTimeLeft<=0)
            {
                isDashing=false;
                if(!isGround)
                {
                    rb.velocity=new Vector2(dashSpeed*horizontalmove,jumpforce);
                }
            }
        }
    }
}