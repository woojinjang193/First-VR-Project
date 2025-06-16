using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
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
    [SerializeField] private TrailRenderer trail;

    [SerializeField] private AudioSource audioSource;
    private bool hasHit = false;

    private bool isReturned = false;

    private void OnEnable()
    {
        isReturned = false;
        hasHit = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit)
        {
            return;
        }
        hasHit = true;
    
        
        if (collision.collider.CompareTag("Monster") || collision.collider.CompareTag("GameOBJ"))
        {
            //Debug.Log("물리충돌:" + collision.gameObject.name);
            audioSource.Play();
    
            rigid.isKinematic = true;  //충돌시 멈춤
            trail.emitting = false; //테일렌더 끔
            //ArrowColliderFalse();
            Invoke("ArrowColliderFalse",0.2f);

            transform.SetParent(collision.collider.transform, worldPositionStays: true);
            //transform.SetParent(collision.transform, worldPositionStays: true);// 충돌한물체를 부모로 설정, 월드스케일 유지 (화살이 붙어있게 하기위함)
    
            Invoke("ArrowReturnPool", returnTime);  //충돌후 일정시간 후 리턴
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
            //Debug.Log("리턴됨");
            return;
        }

        isReturned = true;
        

        if (CompareTag("FireArrow"))  //발사용이면 발사용 풀로 리턴
        {
            //ObjectPoolManager.instance.ArrowReset(this.gameObject);
            ObjectPoolManager.instance.ReturnFireArrow(this.gameObject);
            //Debug.Log("발사용 풀로 리턴됨");
        }
        else if (CompareTag("LoadArrow")) //장전용이면 발사용 풀로 리턴
        {
            //ObjectPoolManager.instance.ArrowReset(this.gameObject);
            ObjectPoolManager.instance.ReturnReloadArrow(this.gameObject);
            //Debug.Log("장전용 풀로 리턴됨");
        }

        
    }

    private void ArrowColliderFalse()
    {
        foreach (Collider collider in GetComponentsInChildren<Collider>()) // 충돌후 못잡게 콜라이더 비활성화 (자식포함 전부 끔)
        {
            collider.enabled = false;
        }
    }





}
