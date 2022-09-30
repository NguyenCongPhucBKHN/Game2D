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
            Debug.Log("Dan cham dat");
            Destroy(gameObject);
        }
        
        if(collision.tag == "Player")
        {  
            Debug.Log("Dan cham player");
            GameObject hitFX = Instantiate(hitVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
