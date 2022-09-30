using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float range;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float attackRange;
    [SerializeField] private GameObject areaAttack;
    private IState currentState;
    private bool isRight= true; //Check huong sang phai
    private Character target;
    public Character Target => target;

    private void Update() {
        if(currentState != null && !IsDead)
        {
            currentState.OnExecute(this); //Update state lien tuc, khi doi state thi vao OnExit state cu va vao OnEnter state moi
        }
    }
    //Khoi tao Enemi: ke thua Oninit tu cha Character, set trang thai Idle, khong tan cong
    public override void OnInit(){
        base.OnInit();
        ChangeState(new IdleState());
        DeActivateAttack();
    }

    //Huy character
    public override void OnDespawn()
    {   
        // ChangeState(null);
        base.OnDespawn();
        Destroy(gameObject); //Neu Destroy(this) thi chi xoa script nay thoi
    }
    
    //Xu ly khi Enemy die
    protected override void OnDeath()
    {
        ChangeState(null);
        base.OnDeath();
    }

    //Chuyen state
    public void ChangeState(IState newState)
    {
        if(currentState != null) //State cu co bang null ko ?
            {
                currentState.OnExit(this);
            }
        currentState = newState;
        if(currentState != null) // currentState khac null thi Enter state moi
        {
            currentState.OnEnter(this);
        }

    }
    //Moving
    public void Moving()
    {
        ChangeAnim("run");
        rb.velocity = transform.right * moveSpeed;
    }
    //stop moving
    public void StopMoving()
    {   
        ChangeAnim("idle");
        rb.velocity= Vector3.zero;
    }
    //Attack
    public void Attack()
    {
        ChangeAnim("attack");
        ActivateAttack();
        Invoke(nameof(DeActivateAttack), 0.6f);
    }

    //Kiem tra muc tieu co trong tam danh ko
    public bool IsTargetInRange()
    {
        if(target!= null && Vector2.Distance(target.transform.position, transform.position)<=attackRange)
        {
            return  true;
        }
        else{
            return false;
        } 
    }
    
    //Set target cho player
    public void SetTarget( Character character)
    {
        this.target= character;
        if(IsTargetInRange()) //Neu trong pham vi tan cong thi chuyen sang trang thai tan cong
        {
            ChangeState(new AttackState());
        }
        else if(Target!=null)
        {
            ChangeState( new PatrolState());
        }
        else
        {
            ChangeState(new IdleState());
        }
    }

    //Chuyen huong tan cong
    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero): Quaternion.Euler(Vector3.up*180); //Vector3.up is (0, 1, 0)
    }

    //Xet va cham voi ememywal
    private void OnTriggerEnter2D(Collider2D collision)
     {
        if(collision.tag == "EnemyWall")
        {  
            ChangeDirection(!isRight);
        }
    }

    //acitvate vung tan cong
    private void ActivateAttack()
    {
        areaAttack.SetActive(true);
    }
    //deacitvate vung tan cong
    
    private void DeActivateAttack()
    {
        areaAttack.SetActive(false);
    }
}

