using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image imageFill;
    [SerializeField] Vector3 offset;
    float hp;
    float maxHp;
    Transform target;
    void Update() 
    {
        imageFill.fillAmount =Mathf.Lerp(imageFill.fillAmount, hp/maxHp, Time.deltaTime*5f); //Toc do lop, ham tuyen tinh fillAmount theo hp/maxHp theo thoi gian
        transform.position= target.position + offset;
    }
    //Khoi tao heathbar
    public void OnInit(float maxHp, Transform target)
    {
        this.target= target; //set vi tri
        this.maxHp= maxHp; //set maxHp
        hp= maxHp;
        imageFill.fillAmount= 1; //Hien thi day mau

    }
    public void SetNewHp( float hp) //set hp moi
    {
        this.hp = hp;
        // imageFill.fillAmount =hp/maxHp;
    }

    public float getHp()
    {
        return this.hp;
    }
}
