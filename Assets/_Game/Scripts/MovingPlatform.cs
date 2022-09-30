using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Class cho vung dat di chuyen len xuong
public class MovingPlatform : MonoBehaviour
{   
    [SerializeField] private Transform aPoint, bPoint;
    [SerializeField] private float speed;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
     transform.position = aPoint.position; //Lay Transform de control hon 
     target= bPoint.position;  
    }

    // Update is called once per frame
    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime);
        if(Vector2.Distance(transform.position, aPoint.position)<0.1f)
        {
            target = bPoint.position;
        }
        else if (Vector2.Distance(transform.position, bPoint.position)<0.1f)
        {
            target = aPoint.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) //Va cham cung, ca hai ko co trigger
    {
        if(collision.gameObject.tag=="Player")
            {
                collision.transform.SetParent(transform); //Khi va cham, Player la con cua Square, Squaze di chuyen nhu nao thi Player di chuyen nhu the
            }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        collision.transform.SetParent(null);
    }
}
