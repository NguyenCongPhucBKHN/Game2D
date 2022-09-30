using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    void OnEnter(Enemy enemy); //Bat dau vao state
    void OnExecute(Enemy enemy); //Update state
    void OnExit(Enemy enemy); //Exit state
}
