using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float timer;
    float randomTime;
    
    //Bat dau vao state Idle: Dung di chuyen
    public void OnEnter(Enemy enemy)
    {   
        enemy.StopMoving();
        timer = 0;
        randomTime = Random.Range(2.5f,4f);
    }
    
    //  Xu khi khi o trong state: timer > randomTime, chuyen sang trang thai PatrolState
    public void OnExecute(Enemy enemy)
    {
        if(timer>randomTime)
        {
            enemy.ChangeState(new PatrolState());
        }
        timer+=Time.deltaTime;
    }


    public void OnExit(Enemy enemy)
    {
        
    }
}
