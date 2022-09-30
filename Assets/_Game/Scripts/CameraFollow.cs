using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{   
    public Transform target;
    public Vector3 offset; //Vi tri tuong doi cua target va camera
    public float speed =20;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    //Di chuyen Player theo FixedUpdate nen Camera cung can di chuyen theo FixedUpdate
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position+ offset, Time.deltaTime*speed);
    }
}
