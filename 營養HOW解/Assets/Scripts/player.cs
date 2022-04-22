using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim,collectionAnim,hpAnim,displayAnim,floorBossAnim,heartEffectAnim,winAnim;
    public GameObject floorBoss,win;
    public bool isHurt,isCrouch,isGround,isJump,isClimb;
    public bool facing_right=true;
    public Transform ceilingCheck,groundCheck;
    public float checkRadius;
    public LayerMask ground,acid,ladder;
    private Vector2 horizontalMove,verticalMove;
    public float hSpeed,vSpeed;
    public float hForce,jumpForce,bumpForce,hurtForce;
    public float fallMultiplier,lowJumpMultiplier,jumpMultiplier;
    public int collections;
    public Text collectionsNum;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate,nextFire;
    public int maxHp;
    static public int hp;
    public float invincibleTime,invincibleTimer;
    static public bool isInvincible;
    private float shake;
    private SpriteRenderer BoxColliderClick;
    public PhysicsMaterial2D slopePhysicsMaterial2D;
    public PhysicsMaterial2D nofrictionPhysicsMaterial2D;
    public GameObject loseDialog;
    public GameObject lastEnemy;
    public GameObject acidObject;
    public GameObject playerInstantiate;
    public float dashTime;
    private float dashTimeLeft;
    private float lastDash=-10f;
    public float dashCoolDown,dashSpeed;
    public bool isDashing;
    public Image cdImage;
    public bool passLevel;
    private PlayerInputActions controls;
    private void Awake()
    {
        controls = new PlayerInputActions();
        controls.Player.Move.performed += ctx => horizontalMove = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => horizontalMove = Vector2.zero;
        controls.Player.Move.performed += ctx => verticalMove = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => verticalMove = Vector2.zero;
        controls.Player.Crouch.performed += ctx => Crouch();
        controls.Player.Crouch.canceled += ctx => CrouchCancel();
        controls.Player.Jump.performed += ctx => JumpMario();
        controls.Player.Jump.canceled += ctx => JumpMarioCancel();
        controls.Player.Throw.started += ctx => Throw();
        controls.Player.Sprint.started += ctx => Sprint();
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
        coll=GetComponent<Collider2D>();
        anim=GetComponent<Animator>();
        hp=maxHp;
        BoxColliderClick=GetComponent<SpriteRenderer>();
        collections=0;
        collectionAnim=GameObject.Find("collectionsnum").GetComponent<Animator>();
        hpAnim=GameObject.Find("hpbar_bottom").GetComponent<Animator>();
        displayAnim=GameObject.Find("display").GetComponent<Animator>();
        if(floorBoss)
        {
            floorBossAnim=GameObject.Find("floorBoss").GetComponent<Animator>();
        }
        heartEffectAnim=GameObject.Find("heartEffect").GetComponent<Animator>();
        if(win)
        {
            winAnim=GameObject.Find("win").GetComponent<Animator>();
        }
    }

    void Update()
    {
        Hpfunction();
        Collectionsnum();
        Invinsible();
        if(hp>=1 && caker.hp==0)
        {
            winAnim.Play("win");
        }
    }

    void FixedUpdate()
    {
        isGround=Physics2D.OverlapCircle(groundCheck.position,checkRadius,ground)||Physics2D.OverlapCircle(groundCheck.position,checkRadius,acid);
        if(!isHurt && hp>0)
        {
            Movement();
        }
        Dash();
        Slope();
        if(isDashing)
        {
            return;
        }
        SwitchAnim();
    }

    void GoGo()
    {
        hp=maxHp;
        caker.hp=2;
        collectionAnim.SetTrigger("plus");
        collections+=20;
        this.gameObject.transform.position=new Vector2(playerInstantiate.transform.position.x,playerInstantiate.transform.position.y);
    }

    //移動
    void Movement()
    {
        // 左右移動
        if(isCrouch)
        {
            rb.velocity=new Vector2(0f,rb.velocity.y);
        }else
        {
            rb.AddForce(new Vector2(horizontalMove.x*hForce, 0f),ForceMode2D.Force);
            if(Mathf.Abs(rb.velocity.x) >= Mathf.Abs(horizontalMove.x*hSpeed))
            {
                rb.velocity=new Vector2(horizontalMove.x*hSpeed,rb.velocity.y);
            }
            anim.SetFloat("running",Mathf.Abs(horizontalMove.x));
        }
        if(isJump || !isGround)
        {
            rb.sharedMaterial=nofrictionPhysicsMaterial2D;
        }else
        {
            rb.sharedMaterial=slopePhysicsMaterial2D;
        }
        // 上下移動
        RaycastHit2D hitInfo=Physics2D.Raycast(transform.position,Vector2.up,2,ladder);
        if(hitInfo.collider!=null)
        {
            if(verticalMove.y>0)
            {
                isClimb=true;
            }
        }else
        {
            isClimb=false;
        }
        if(isClimb)
        {
            rb.velocity=new Vector2(rb.velocity.x,verticalMove.y*vSpeed);
            rb.gravityScale=0;
        }else
        {
            rb.gravityScale=4;
        }
        // 方向
        if(horizontalMove.x>0f&&!facing_right)
        {
            Flip();
        }else if(horizontalMove.x<0f&&facing_right)
        {
            Flip();
        }
    }

    //翻轉
    void Flip()
    {
        facing_right=!facing_right;
        transform.Rotate(0f, 180f, 0f);
    }

    void Sprint()
    {
        if(Time.time>=lastDash+dashCoolDown)
        {
            ReadyToDash();//可執行dash
        }
    }

    //跳躍
    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(isGround)
            {
                isJump=true;
                anim.SetBool("jumping",true);
                soundmanager.sound_instance.Jumpaudio();
                rb.velocity = Vector2.up*jumpForce;
            }
        }
        if(rb.velocity.y<0f)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier-1) * Time.deltaTime;
        }else if(rb.velocity.y>0f && !Input.GetButton("Jump"))
        {
            isJump=false;
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier-1) * Time.deltaTime;
        }
        if(Input.GetButtonUp("Jump"))
        {
            isJump=false;
        }
    }
    void JumpMario()
    {
        if(isGround && !isCrouch)
        {
            anim.SetBool("jumping",true);
            soundmanager.sound_instance.Jumpaudio();
            isJump=true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    void JumpMarioCancel()
    {
        isJump=false;
        if(rb.velocity.y>0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*jumpMultiplier);
        }
    }

    //蹲下
    void Crouch()
    {
        if(!Physics2D.OverlapCircle(ceilingCheck.position,checkRadius,ground))
        {
            isCrouch=true;
            GetComponent<CapsuleCollider2D>().offset = new Vector2(0.1f, -1.21f);
            GetComponent<CapsuleCollider2D>().size = new Vector2(2f, 6.07f);
            anim.SetBool("crouching",true);
        }
    }
    void CrouchCancel()
    {
        isCrouch=false;
        GetComponent<CapsuleCollider2D>().offset = new Vector2(0.09f, -0.26f);
        GetComponent<CapsuleCollider2D>().size = new Vector2(2f, 7.98f);
        anim.SetBool("crouching",false);
    }

    //切換動畫
    void SwitchAnim()
    {
        anim.SetBool("idle",false);
        if(anim.GetBool("jumping"))
        {
            if(rb.velocity.y<0f)
            {
                anim.SetBool("jumping",false);
                anim.SetBool("falling",true);
            }
        }
        else if(isHurt)
        {
            anim.SetBool("hurt",true);
            anim.SetFloat("running",0);
            if(Mathf.Abs(rb.velocity.x)<0.2f)
            {
                anim.SetBool("hurt",false);
                anim.SetBool("idle",true);
                isHurt=false;
            }
        }
        else if(isGround)
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
            collision.GetComponent<BoxCollider2D>().offset=new Vector2(5.32f, 8.36f);
            collision.GetComponent<BoxCollider2D>().size=new Vector2(104.39f, 82.57f);
            Destroy(acidObject,0.5f);
            Destroy(lastEnemy);
            BossFloor();
        }
        if(collision.tag=="heart")
        {
            soundmanager.sound_instance.HeartAudio();
            if(hp<=4)
            {
                heartEffectAnim.Play("heartEffect");
                hp+=1;
                Destroy(collision.gameObject);
            }
            else
            {
                heartEffectAnim.Play("heartEffect");
                Destroy(collision.gameObject);
            }
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

    void BossFloor()
    {
        floorBossAnim.Play("floorBoss");
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag=="bosscorner")
        {
            if(collections+1<caker.hp)
            {
                Invoke("Lose",1.5f);
            }
        }
        if(collision.tag=="acid")
        {
            Hurt();
        }
        if(collision.tag=="door")
        {
            if(isGround && hp>=1)
            {
                soundmanager.sound_instance.DoorAudio();
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
                passLevel=true;
                anim.Play("fadeout");
                Invoke("GotoMenu",0.8f);
            }
        }
        if(collision.tag=="ship")
        {
            if(isGround && hp>=1)
            {
                soundmanager.sound_instance.DoorAudio();
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
                anim.Play("fadeout");
                Invoke("NextScene",0.5f);
            }
        }
    }

    void Lose()
    {
        hp=0;
        loseDialog.SetActive(true);
    }

    //碰撞敵人
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="saliva")
        {
            if(transform.position.x<collision.gameObject.transform.position.x)
            {
                Hurt();
                rb.AddForce(-Vector2.right*hurtForce,ForceMode2D.Impulse);
            }else if(transform.position.x>collision.gameObject.transform.position.x)
            {
                Hurt();
                rb.AddForce(Vector2.right*hurtForce,ForceMode2D.Impulse);
            }
        }
        if(collision.gameObject.tag=="enemy")
        {
            enemy enemy=collision.gameObject.GetComponent<enemy>();
            if(collision.contacts[0].normal.y == 1)//从下方碰撞
            {
                enemy.JumpOn();
                rb.velocity=new Vector2(rb.velocity.x,bumpForce);
                anim.SetBool("jumping",true);
                Display1();
            }else if(transform.position.x<collision.transform.position.x)
            {
                Hurt();
                rb.AddForce(Vector2.right*hurtForce,ForceMode2D.Impulse);
            }else if(transform.position.x>collision.transform.position.x)
            {
                Hurt();
                rb.AddForce(-Vector2.right*hurtForce,ForceMode2D.Impulse);
            }
        }
        if(collision.gameObject.tag=="boss")
        {
            if(transform.position.x<collision.gameObject.transform.position.x)
            {
                Hurt();
                rb.AddForce(-Vector2.right*hurtForce,ForceMode2D.Impulse);
            }else if(transform.position.x>collision.gameObject.transform.position.x)
            {
                Hurt();
                rb.AddForce(Vector2.right*hurtForce,ForceMode2D.Impulse);
            }
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="boss")
        {
            if(transform.position.x<collision.gameObject.transform.position.x)
            {
                Hurt();
                rb.AddForce(-Vector2.right*hurtForce,ForceMode2D.Impulse);
            }else if(transform.position.x>collision.gameObject.transform.position.x)
            {
                Hurt();
                rb.AddForce(Vector2.right*hurtForce,ForceMode2D.Impulse);
            }
        }
    }

    //受傷
    void Hurt()
    {
        if(!isInvincible)
        {
            isHurt=true;
            soundmanager.sound_instance.Hurtaudio();
            hp-=1;
        }
        Startinvincible();
    }

    //血量
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

    //射擊
    public void Throw()
    {
        if(collections>=1)
        {
            if(Time.time>nextFire) //讓子彈發射有間隔
            {
                anim.SetTrigger("throw");
                nextFire = Time.time + fireRate; //Time.time表示從遊戲開發到現在的時間，會隨着遊戲的暫停而停止計算
                Invoke("Bulletinstantiate",0.3f);
                collections--;
                anim.SetBool("idle",true);
            }
        }
    }

    //子彈生成
    void Bulletinstantiate()
    {
        Instantiate(bullet, firePoint.transform.position, firePoint.rotation);
        soundmanager.sound_instance.Throwaudio();
    }

    //收集計算
    public void Collectionscount1()
    {
        collectionAnim.SetTrigger("plus");
        collections+=1;
    }
    public void Collectionscount2()
    {
        collectionAnim.SetTrigger("plus");
        collections+=2;
    }

    //收集計數
    void Collectionsnum()
    {
        collectionsNum.text=collections.ToString();
    }

    //顯示消化液增加
    void Display1()
    {
        displayAnim.Play("+1");
    }
    public void Display2()
    {
        displayAnim.Play("+2");
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
        cdImage.fillAmount-=1.0f/dashCoolDown*Time.deltaTime;
        if(isDashing)
        {
            if(dashTimeLeft>0)
            {
                if(rb.velocity.y>0&&!isGround)
                {
                    rb.velocity=new Vector2(dashSpeed*horizontalMove.x,jumpForce*0.5f);
                }else
                {
                    rb.velocity=new Vector2(dashSpeed*horizontalMove.x,rb.velocity.y);
                }
                dashTimeLeft-=Time.deltaTime;
                shadowPool.instance.GetFromPool();
            }
            if(dashTimeLeft<=0)
            {
                isDashing=false;
                if(!isGround)
                {
                    rb.velocity=new Vector2(dashSpeed*horizontalMove.x,jumpForce*0.5f);
                }
            }
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