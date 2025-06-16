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
            //Debug.Log("�����浹:" + collision.gameObject.name);
            audioSource.Play();
    
            rigid.isKinematic = true;  //�浹�� ����
            trail.emitting = false; //���Ϸ��� ��
            //ArrowColliderFalse();
            Invoke("ArrowColliderFalse",0.2f);

            transform.SetParent(collision.collider.transform, worldPositionStays: true);
            //transform.SetParent(collision.transform, worldPositionStays: true);// �浹�ѹ�ü�� �θ�� ����, ���彺���� ���� (ȭ���� �پ��ְ� �ϱ�����)
    
            Invoke("ArrowReturnPool", returnTime);  //�浹�� �����ð� �� ����
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
            //Debug.Log("���ϵ�");
            return;
        }

        isReturned = true;
        

        if (CompareTag("FireArrow"))  //�߻���̸� �߻�� Ǯ�� ����
        {
            //ObjectPoolManager.instance.ArrowReset(this.gameObject);
            ObjectPoolManager.instance.ReturnFireArrow(this.gameObject);
            //Debug.Log("�߻�� Ǯ�� ���ϵ�");
        }
        else if (CompareTag("LoadArrow")) //�������̸� �߻�� Ǯ�� ����
        {
            //ObjectPoolManager.instance.ArrowReset(this.gameObject);
            ObjectPoolManager.instance.ReturnReloadArrow(this.gameObject);
            //Debug.Log("������ Ǯ�� ���ϵ�");
        }

        
    }

    private void ArrowColliderFalse()
    {
        foreach (Collider collider in GetComponentsInChildren<Collider>()) // �浹�� ����� �ݶ��̴� ��Ȱ��ȭ (�ڽ����� ���� ��)
        {
            collider.enabled = false;
        }
    }





}
