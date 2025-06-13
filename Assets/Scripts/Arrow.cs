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
        //Debug.Log($"충돌한 태그: {collision.collider.tag}");
   
        if (collision.collider.CompareTag("Monster") || collision.collider.CompareTag("GameOBJ"))
        {
            rigid.isKinematic = true;
            //rigid.velocity = Vector3.zero;
            transform.SetParent(collision.transform);
            //transform.localScale = new Vector3(1f, 1f, 1f); //크기 고정
            Invoke("ArrowReturnPool", returnTime);
   
        }
        else
        {
            return;
        }
   
    }

    // private void Update()
    // {
    //  
    //       if (GameManager.instance.isLoaded)
    //      {
    //          Debug.Log("장전완료 리턴함");
    //          ArrowReturnPool();
    //          
    //      }
    // }

    private void Update()
    {
        if (rigid.velocity.magnitude > 1) 
        {
            transform.forward = rigid.velocity.normalized; 
        }
    }

    public void ArrowReturnPool()
    {
        if (isReturned)
        {
            Debug.Log("리턴됨");
            return;
            
        }
            isReturned = true;
            ObjectPoolManager.instance.ReturnToPool(this.gameObject);
            Debug.Log("풀로 리턴됨");
    }

    //화살 자연스럽게 날아가게하기


}
