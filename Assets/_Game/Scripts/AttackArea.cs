using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{

    private float timer=0;
    private float maxTime=3;

    private void OnTriggerEnter2D(Collider2D other)  //Phat hien va cham giua vung tan cong voi Player va Enemy
    {

        if(other.tag == "Player"|| other.tag== "Enemy") //Khi vung attack va cham voi player va Enemy
        {
            other.GetComponent<Character>().OnHit(30f); //Giam Hp di 30
        }
    }
}
