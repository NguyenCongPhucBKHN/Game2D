using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float randomTime;
    float timer;
    //Bat dau vao State
    public void OnEnter(Enemy enemy)
    {   
        timer =0;
        randomTime = Random.Range(3f, 6f);
    }
    // Xu ly trong state
    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if(enemy.Target != null)
        {   
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x); //Huong Enemy theo player
            if(enemy.IsTargetInRange()) //Neu player trong khoang muc tieu
            {
                enemy.ChangeState(new AttackState()); //Chuyen sang state tan cong
            }
            else
            {
                enemy.Moving(); //Di chuyen
            }
            
        }
        else
        {
            if(timer < randomTime)
            {
                enemy.Moving(); //Di chuyen
            }
            else
            {
                enemy.ChangeState(new IdleState()); //Dung
            }
        }  
    }

    public void OnExit(Enemy enemy) //Thoat trang thai
    {
    }
}
