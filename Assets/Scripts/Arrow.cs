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
        //Debug.Log("�浹:" + {collision.gameObject.name});
        if (collision.collider.CompareTag("Monster") || collision.collider.CompareTag("GameOBJ"))
        {
            rigid.isKinematic = true;  //�浹�� ����
            //rigid.velocity = Vector3.zero;

            transform.SetParent(collision.transform, worldPositionStays: true);// �浹�ѹ�ü�� �θ�� ����, ���彺���� ���� (ȭ���� �پ��ְ� �ϱ�����)

            // transform.SetParent(collision.transform);  // �浹�ѹ�ü�� �θ�� ���� (ȭ���� �پ��ְ� �ϱ�����)
            // transform.localScale = new Vector3(1f, 1f, 1f); //ũ�� ����
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
        if (isReturned)  //�ߺ����� ����
        {
            Debug.Log("���ϵ�");
            return;
        }

        isReturned = true;
        

        if (CompareTag("FireArrow"))  //�߻���̸� �߻�� Ǯ�� ����
        {
            ObjectPoolManager.instance.ArrowReset(this.gameObject);
            ObjectPoolManager.instance.ReturnFireArrow(this.gameObject);
            //Debug.Log("�߻�� Ǯ�� ���ϵ�");
        }
        else if (CompareTag("LoadArrow")) //�������̸� �߻�� Ǯ�� ����
        {
            ObjectPoolManager.instance.ArrowReset(this.gameObject);
            ObjectPoolManager.instance.ReturnReloadArrow(this.gameObject);
            //Debug.Log("������ Ǯ�� ���ϵ�");
        }

        
    }





}
