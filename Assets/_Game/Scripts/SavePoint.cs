using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Luu lai diem chet truoc
public class SavePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)  //Xet va cham voi Player
    {
        if(collider.tag=="Player")

        {
            collider.GetComponent<Player>().SavePoint();
        }
        else   
            return;
    }
}
