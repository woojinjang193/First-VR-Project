using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements.Experimental;
using UnityEngine.XR.Interaction.Toolkit;

public class Arrow : MonoBehaviour
{
   // public IObjectPool<GameObject> Pool { get; set; }
    [SerializeField] Rigidbody rigid;
    [SerializeField]  private float returnTime = 5f;
    //public bool isFired = false;

    private bool isReturned = false;

    private void OnEnable()
    {
        isReturned = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("충돌:" + {collision.gameObject.name});
        if (collision.collider.CompareTag("Monster") || collision.collider.CompareTag("GameOBJ"))
        {
            rigid.isKinematic = true;  //충돌시 멈춤
            //rigid.velocity = Vector3.zero;

            transform.SetParent(collision.transform, worldPositionStays: true);// 충돌한물체를 부모로 설정, 월드스케일 유지 (화살이 붙어있게 하기위함)

            // transform.SetParent(collision.transform);  // 충돌한물체를 부모로 설정 (화살이 붙어있게 하기위함)
            // transform.localScale = new Vector3(1f, 1f, 1f); //크기 고정
            Invoke("ArrowReturnPool", returnTime);
   
        }
        else
        {
            return;
        }
   
    }

    private void Update()
    {
        if (rigid.velocity.magnitude > 1) 
        {
            transform.forward = rigid.velocity.normalized; 
        }
    }

    public void ArrowReturnPool()
    {
        if (isReturned)  //중복리턴 방지
        {
            Debug.Log("리턴됨");
            return;
        }

        isReturned = true;
        

        if (CompareTag("FireArrow"))  //발사용이면 발사용 풀로 리턴
        {
            ObjectPoolManager.instance.ArrowReset(this.gameObject);
            ObjectPoolManager.instance.ReturnFireArrow(this.gameObject);
            //Debug.Log("발사용 풀로 리턴됨");
        }
        else if (CompareTag("LoadArrow")) //장전용이면 발사용 풀로 리턴
        {
            ObjectPoolManager.instance.ArrowReset(this.gameObject);
            ObjectPoolManager.instance.ReturnReloadArrow(this.gameObject);
            //Debug.Log("장전용 풀로 리턴됨");
        }

        
    }





}
