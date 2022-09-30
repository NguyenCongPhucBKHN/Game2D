using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D projectile;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float minProjectile;
    [SerializeField] private float maxProjectile;
    
    private Rigidbody2D tile;
    void Update()
    {
        OnInit(); //Khoi tao dan
        Invoke(nameof(OnDespawn),1f); //Huy dan sau 1s
        
    }

    //Khoi tao so luong dan, set vi tri va van toc di chuyen
    void OnInit()
    {
        float numberProjectile = Random.Range(minProjectile, maxProjectile);
        for (int i =0; i<numberProjectile; i++)
        {   
            Vector3 pos = new Vector3 (Random.Range(0, Screen.height), Random.Range(0, Screen.height), 0);
            tile = Instantiate(projectile, pos, Quaternion.identity);
            tile.velocity = Vector2.down*fallSpeed;
        }
    }

    //Ham huy
    void OnDespawn()
    {
        Destroy(tile);
    }
    

}
