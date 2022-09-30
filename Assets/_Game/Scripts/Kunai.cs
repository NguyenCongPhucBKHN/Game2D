using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Dan cua Player
public class Kunai : MonoBehaviour
{
    public GameObject hitVFX;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Update()
    {
        OnInit();
    }
    //Khoi tao dan
    public void OnInit()
    {
        rb.velocity = transform.right *5f; //velocity di chuyen
        Invoke(nameof(OnDespawn), 4f); //Destroy dan
    }

    //Ham huy dans
    public void OnDespawn()
    {
        Destroy(gameObject);
    }

    //Va cham voi enemy
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Character>().OnHit(30f); //Giam mau
            Instantiate(hitVFX, transform.position, transform.rotation); // Tao hitVFX
            OnDespawn();
        }
    }

}
