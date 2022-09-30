using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;
    //Tao ra layer ground
    [SerializeField] private LayerMask groudLayer;
    [SerializeField] private float speed = 5 ;
    //Animator la thang dieu khien ani, Animation chi la clip
    // [SerializeField] private Animator animator;
    [SerializeField] private float jumpForce = 350;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;
    
    private bool isGrounded;
    private bool isJumpping = false;
    private bool isAttack= false;
    private bool isDeath =false;
    private float horizontal;
    private Vector3 savePoint;
    private int coin =0;
    private float timer=0;
    [SerializeField] private float maxTime=1;

    private void Awake() 
    {
        coin = PlayerPrefs.GetInt("coin",0);
    }

    //Ham de khoi tao khac tham so cho Player
    public override void OnInit()
    {   
        base.OnInit(); //Ke thua tu lop cha Character
        isJumpping = false;
        isDeath =false; 
        isAttack = false;
        transform.position = savePoint;
        ChangeAnim("idle"); //Set trang thai Idle
        DeActivateAttack(); //Deactivate box vung tan cong
        SavePoint(); //Khoi tao 
        UIManager.instance.SetCoin(coin); //Hien thi gia tri vao UI Text
    }

    //Ghi de ham OnDespawn cua lop cha Character va goi ham OnInit: Khoi tao lai cac tham so
    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }
    //Ham goi khi Player chet: An heathbar, chuyen sang animation die, huy object sau 2s
    protected override void OnDeath(){

        base.OnDeath();
    }

    // Update is called once per frame
    void Update()
    {
        //Reset mau sau khoang thoi gian maxTime
        timer+=Time.deltaTime;  //Timing reset mau
        if(timer>maxTime) 
        {
            healthBar.OnInit(100, transform); //Reset mau
            timer=0;
        }
        
        //Neu Player chet thi khong lam gi
        if(IsDead)
        {
            return;
        }
        //Kiem tra trang thai cham dat cua Player
        isGrounded = CheckGrounded();

        // horizontal = Input.GetAxisRaw("Horizontal"); // value between -1 and 1, is 0 when button isnt pressed
        //verticle = Input.GetAxisRaw("Vertical");

        //Attack thi Player dung lai
        if(isAttack)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        //Cac hanh dong tren mat dat
        if(isGrounded)
        {
            //Dang nhay thi dung lai
            if(isJumpping)
            {
                return;
            }
            //Jump: Neu nhan phim cach, chuyen amination jump va set vector force de Player nhay len
            if(Input.GetKeyDown(KeyCode.Space) )
            {   
                ChangeAnim("jump"); //chuyen amination jump
                Jump(); //Nhay
            }
            //Run
            if(Mathf.Abs(horizontal)>0.01f)
            {
                ChangeAnim("run"); //chuyen amination run
            }    
            //attack
            if(Input.GetKeyDown(KeyCode.C) && isGrounded)
            {   
                Attack();
                ActivateAttack();
            }
            //throw;
            if(Input.GetKeyDown(KeyCode.V) && isGrounded)
            {
                Throw();
            }
        }

        //check falling: khi khong cham dat va van toc chieu y < 0
        if(!isGrounded && rb.velocity.y < 0)
            {
                ChangeAnim("fall");
                isJumpping=false;
            }

        //Moving
        if(Mathf.Abs(horizontal)>0.1f)
        {
            //Huong * speed * deltatime
            rb.velocity = new Vector2(horizontal*speed, rb.velocity.y);
            //Horizontal >0 => Tra ve 0, neu Horizontal <=0 => Tra ve 180
            transform.rotation= Quaternion.Euler(new Vector3(0,horizontal>0 ? 0: 180,0)); 
            // transform.localScale = new Vector3(horizontal, 1, 1);        
        } 
        //Khi khong o cac trang thai Jump, Attack, Death va dang tren mat dat thi la trang thai Idle
        else if(isGrounded && !isJumpping && !isAttack && !IsDead && !isDeath)
        {   
            ChangeAnim("idle");
            rb.velocity= Vector2.zero;
        }
    }
    //Set vi tri savePoint, luu vi tri da chet truoc
    internal void SavePoint()
    {
        savePoint = transform.position;
    }

    

    //Ham kiem tra Raycast, ban 1 tia tu nhan vat, check xem recap co cham vao box cua ground khong
    private bool CheckGrounded()
    {    
        /*Ham Raycast nhan dau vao:
            + origin: Noi bat dau, vi tri cua nhan vat
            + direction: Huong, 
            + distance: Do dai
            + LayerMask: Muc tieu ban den
        Thuc hien chuc nang ban tia raycast tu vi tri origin, bat theo huong direction, voi do dai tia la distance va ban den trung layerMask
        */
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groudLayer);
        //Hit luon luon khac null, check xem no co trung collider nao ko
        // Debug.Log("Hit collider: " + hit.collider);
        // Debug.Log(hit.collider != null);
        return hit.collider != null;

    }
    //Attack: Chuyen Amination, set bien co isAttack va goi ham ResetAttack sau 1s

    public void Attack()
    {   
        ChangeAnim("attack");
        isAttack= true;
        Invoke(nameof(ResetAttack), 1);
    }
    // Throw: Chuyen Amination, set bien co isAttack va goi ham ResetAttack sau 1s, tao cac vien dan den ban ra tu vi tri throwPoint

    public void Throw() 
    {
        ChangeAnim("throw");
        isAttack= true;
        Invoke(nameof(ResetAttack), 1);
        Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation);
    }

    //Jump
    public void Jump() 
    {   
        isJumpping =true;
        ChangeAnim("jump");
        rb.AddForce(jumpForce*Vector2.up);
        
    }
    
    //ResetAttack
    private void ResetAttack()
    {   
        ChangeAnim("idle");
        Invoke(nameof(DeActivateAttack),1f);
        isAttack = false;
    }

    //Phat hien va cham cua Player va coin, DeathZone, Projectile
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Coin")
        {   
            coin++;
            PlayerPrefs.SetInt("coin", coin);
            UIManager.instance.SetCoin(coin);
            Destroy(collision.gameObject);
        }
        if(collision.tag=="DeathZone"||collision.tag == "Projectile" )
        {   
            hp=0;
            base.OnDeath();
        }
        
    }

    //Acitvate vung tan cong
    private void ActivateAttack()
    {
        attackArea.SetActive(true);
    }
    //DeAcitvate vung tan cong
    private void DeActivateAttack()
    {
        attackArea.SetActive(false);
    }

    //Ham Move de set cho button
    public void SetMove (float horizontal) 
    {
        this.horizontal = horizontal;
    }
}
