using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : Character
{
    /*
    ==================================================CAC HAM TRONG CHUONG TRINH==========================================
    
    I/ API UNITY:
        + Physics2D.Raycast(Vector2 origin, Vector2 direction, float distance, layerMask layerMask)
            Thuc hien chuc nang ban tia raycast tu vi tri origin, bat theo huong direction, voi do dai tia la distance va ban den trung layerMask
        + Input.GetAxisRaw(string keyword)
            Get Inputkeyboard return float value between -1 and 1, equal 0 when button isn't press
        + Invoke(string callFunction, float time): Goi ham callFunction sau khoang time
    II/ Custom Funtion
        + bool CheckGrounded(): Check Player co cham dat khong
        + void ChangeAnim(string animName): Set animator la animName
        + void ResetIdle(): Reset tat ca state ve state Idle
        + void Move(): Di chuyen player
        + void Attack(): Player attack
        + void Jump(): Player Jump
        + void Throw(): Player Throw
        + void Fall: Player Fall
    Chuong trinh thuc hien trong ham FixedUpdate():
        + Player cham dat: Co the thuc hien roi rac cac hanh dong: Move, Jump, Attack, Throw
        + Player khong cham dat va rb.velocity.y< 0 :Player o trang thai fall
    */
    [SerializeField] private Rigidbody2D rb;
    //Tao ra layer ground
    [SerializeField] private LayerMask groudLayer;
    [SerializeField] private float speed = 500 ;
    //Animator la thang dieu khien ani, Animation chi la clip

    // [SerializeField] private Animator animator;
    [SerializeField] private float jumpForce = 350;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;
    private bool isGrounded;
    private bool isJumpping ;
    private bool isAttack;
    private float horizontal;
    // private string currentAnim;
    private int coin =0;
    // private bool isDeath =false;
    private Vector3 savePoint; //Vi tri Player checkpoint
    
    private void Awake() 
    {
        // OnInit();
        coin = PlayerPrefs.GetInt("coin",0);
    }
    
    public override void OnInit()
    {   
        base.OnInit();
        // isDeath =false;
        isAttack = false;
        transform.position = savePoint;
        ChangeAnim("idle");
        DeActivateAttack();
        SavePoint();
        UIManager.instance.SetCoin(coin);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }
    protected override void OnDeath(){
        base.OnDeath();
    }

   
    void Update()
    {
        isGrounded= CheckGrounded();
        if(!IsDead && isGrounded)
        {
            Attack();
            Move();
            Jump();
            Throw();
            // FarAway();
        }
        else if(!IsDead && ! isGrounded)
        {   
            Move();
            Fall();
        }
        else if(IsDead)
        {
            return;
        }
    }

    
    private bool CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groudLayer);
        return hit.collider != null;
    }

    //ResetIdle
    private void ResetIdle()
    {   
        isAttack= false;
        ChangeAnim("idle");
    }
    
    void Attack()
    {
        // if(Input.GetKeyDown(KeyCode.C))
            {
                ChangeAnim("attack");
                ActivateAttack();
                // Invoke(nameof(DeActivateAttack), 0.6f);
                // Invoke(nameof(ResetIdle), 0.6f);
            }
    }

    /*
    Thay doi Anim:
        + Kiem tra neu anim hien tai khac anim muc tieu:
            Xet animator ve anim muc tieu
        
    */
    // void ChangeAnim(string animName)
    // {
    //     if(currentAnim != animName)
    //     {
    //         animator.ResetTrigger(animName);
    //     }  
    //     currentAnim = animName;
    //     animator.SetTrigger(currentAnim);
    // }

   
    void Move()
    {
        // horizontal = Input.GetAxis("Horizontal");
        if(Mathf.Abs(horizontal)>0.1f)
        {   
            ChangeAnim("run");
            rb.velocity = new Vector2(horizontal*speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal>0?0:180, 0));
            
            Invoke(nameof(ResetIdle), 0.4f); 
        }
    }
    //Ham nhan vat nhay
    void Jump()
    {   
        Move();
        if(Input.GetKeyDown(KeyCode.Space))
        {   
            // rb.velocity= Vector2.zero;
            isGrounded= false;
            ChangeAnim("jump");
            rb.AddForce(jumpForce*Vector2.up);
            Invoke(nameof(ResetIdle), .5f);
        }
        
        
    }

    void FarAway()
    {   
        horizontal = Input.GetAxis("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(horizontal)>0.1f )
        {
            Jump();
            Move();
        }
    }
    
    //Ham throw
    void Throw()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            ChangeAnim("throw");
            Invoke(nameof(ResetIdle), 0.35f);
            if(throwPoint!= null)
            {
                Instantiate(kunaiPrefab,throwPoint.position, throwPoint.rotation); //vi tri va goc tu throwPoint
            }
        }
    }

    void Fall()
    {
        if(rb.velocity.y<0)
            {
                ChangeAnim("fall");
            }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag=="Coin")
        {   
            coin++;
            PlayerPrefs.SetInt("coin", coin);
            UIManager.instance.SetCoin(coin);
            Destroy(collision.gameObject);
        }
        if(collision.tag=="DeathZone")
        {   
            // isDeath= true;
            ChangeAnim("die");
            Invoke(nameof(OnInit), 1f); //Goi Ham Onit sau 1s
        }
    }

    internal void SavePoint()
    {
        savePoint = transform.position;
    }

    private void ActivateAttack()
    {
        attackArea.SetActive(true);
    }
    private void DeActivateAttack()
    {
        attackArea.SetActive(false);
    }
    
    public void SetMove(float horizontal)
    {
        this.horizontal = horizontal;

    }

    public void SetAttack(){
        Debug.Log("SetAttack");
        Attack();
    }

    public void StopAttack()
    {
        Invoke(nameof(DeActivateAttack), 0.6f);
        Invoke(nameof(ResetIdle), 0.6f);
    }
    
}
