using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState //Ke thua tu Interface State
{
    float timer;

    //Bat dau vao state tan cong: Chuyen huong sang player, dung di chuyen, tan cong
    public void OnEnter(Enemy enemy)
    {
        if(enemy.Target!=null) //Check target ton tai khong
        {   
            //Chuyen huong enemy toi player
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);
            
            enemy.StopMoving(); //stop moving
            enemy.Attack(); //Attack
        }
        timer=0;
    }

    //Xu ly khi vao trang thai: Neu timer >1.5f thi chuyen sang trang thai di vong quanh
    public void OnExecute(Enemy enemy)
    {
        timer+=Time.deltaTime;
        if(timer>=1.5f)
        {
            enemy.ChangeState(new PatrolState());
        }
    }
    
    //Thoat trang thai
    public void OnExit(Enemy enemy)
    {
    }
}
