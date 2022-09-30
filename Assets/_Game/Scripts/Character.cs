using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Cha cua Player va Enemy
public class Character : MonoBehaviour
{   
    [SerializeField] private Animator animator; 
    [SerializeField] public HealthBar  healthBar;
    [SerializeField] private CombatText combatTextPrefab;
    protected float hp;
    public bool IsDead => hp<=0; //=> return
    private string currentAnim;

    // Start is called before the first frame update
    void Start()
    {
     OnInit();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Ham khoi tao cho Character: Set day mau
    public virtual void OnInit()
    {
        hp =100;
        healthBar.gameObject.SetActive(true);
        healthBar.OnInit(100, transform);
    }

    //Ham huy character
    public virtual void OnDespawn()
    {

    }
    
    //Ham xu ly khi character die: deactivate healthBar, chuyen sang trang thai die, goi lam huy sau 2s
    protected virtual void OnDeath()
    {
        healthBar.gameObject.SetActive(false);
        ChangeAnim("die");
        Invoke(nameof(OnDespawn), 2f);
    }

    //Ham an dan: Neu chua chet thi hp giam, neu du dieu kien chet thi chet, hien thi so luong hp bi giam
    public void OnHit(float damage)
    {
        if(!IsDead)
        {
            hp-=damage;
            if(IsDead)
            {   
                hp=0;
                OnDeath();
            }
            healthBar.SetNewHp(hp);
            //Tao text so luong Hp bi giam bay len
            Instantiate(combatTextPrefab, transform.position+Vector3.up, Quaternion.identity).OnInit(damage); //identity: Goc xoay mac dinh (0,0,0) 
        }

    }

    //Ham thay doi Animation
    protected void ChangeAnim(string animName)
    {
        if(currentAnim != animName) //Animation hien tai khac cai mong muon
        {
            animator.ResetTrigger(currentAnim); //Reset lai currentAnim
            currentAnim = animName; //Set animName thanh currentAnim
            animator.SetTrigger(currentAnim); //SetTrigger currentAnim
        }  
        
    }
}
