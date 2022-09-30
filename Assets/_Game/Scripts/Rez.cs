using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rez : MonoBehaviour
{
    [SerializeField] HealthBar healthPlayer;
    [SerializeField] HealthBar[] healthBarEnemies;
    
    private float numberPlay=3;
   
    void Update()
    {
       foreach(HealthBar healthBarEnemy in healthBarEnemies)
       {
        if(healthBarEnemy.getHp()<=0)
            {
                healthPlayer.OnInit(100, transform);
            }
       }
        
    }

    
}
