using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject hitVFX; 
    
    //Xet va cham giua dan voi mat dat va player
    private void OnTriggerEnter2D(Collider2D collision)
     {
        if(collision.tag == "Ground")
        {  
            Destroy(gameObject);
        }
        
        if(collision.tag == "Player")
        {  
            GameObject hitFX = Instantiate(hitVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
