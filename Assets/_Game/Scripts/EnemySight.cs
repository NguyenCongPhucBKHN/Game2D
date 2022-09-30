using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Box chua vung tan cong cua Enemy
public class EnemySight : MonoBehaviour

{
    public Enemy enemy;
    
    private void OnTriggerEnter2D(Collider2D collision) //Player trong vung tan cong
    {
        if(collision.tag== "Player")
        {
            enemy.SetTarget(collision.GetComponent<Character>());
        }
    }
    private void OnTriggerExit2D(Collider2D collider) //Player ra khoi vung tan cong
    {
        if(collider.tag=="Player")
        {
            enemy.SetTarget(null);
        }
    }

}
