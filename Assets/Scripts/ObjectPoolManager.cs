using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    public GameObject reloadArrowPrefab;
    public GameObject fireArrowPrefab;

    public IObjectPool<GameObject> ReloadArrowPool { get; private set; }
    public IObjectPool<GameObject> FireArrowPool { get; private set; }

    public int defaultCapacity = 10;
    public int maxPoolSize = 15;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        Init();
    }
    
    private void Init()
    {
        ReloadArrowPool = new ObjectPool<GameObject>(CreateReloadArrow, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);

        FireArrowPool = new ObjectPool<GameObject>(CreateFireArrow, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);
    

        for (int i = 0; i < defaultCapacity; i++)
        {
            GameObject reloadArrow = CreateReloadArrow();
            ReloadArrowPool.Release(reloadArrow);

            GameObject fireArrow = CreateFireArrow();
            FireArrowPool.Release(fireArrow);
        }
    }
    
    // ����
    private GameObject CreateReloadArrow()   //������ȭ�� 
    {
        return Instantiate(reloadArrowPrefab);
    }

    private GameObject CreateFireArrow()    //�߻��ȭ��
    {
        return Instantiate(fireArrowPrefab);
    }

    // ���
    private void OnTakeFromPool(GameObject poolGo)
    {
        ArrowReset(poolGo);
        poolGo.SetActive(true);
    }
    
    // ��ȯ
    private void OnReturnedToPool(GameObject poolGo)
    {
        ArrowReset(poolGo);
        poolGo.SetActive(false);
    }
    
    // ����
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }

    public GameObject GetReloadArrow()
    {
        return ReloadArrowPool.Get();
    }

    public GameObject GetFireArrow()
    {
        return FireArrowPool.Get();
    }

    public void ReturnReloadArrow(GameObject obj)
    {
        ReloadArrowPool.Release(obj);
    }

    public void ReturnFireArrow(GameObject obj)
    {
        FireArrowPool.Release(obj);
    }


   public void ArrowReset(GameObject arrow)  //ȭ�� ����
   {
       if (arrow == null) return;
   
       arrow.transform.SetParent(null);  //�θ� ����
       arrow.transform.localScale = Vector3.one;  // ������ �ʱ�ȭ
       arrow.transform.localRotation = Quaternion.identity;
       Rigidbody rigid = arrow.GetComponent<Rigidbody>();
       rigid.isKinematic = false; // ��׸�ƽ ��
       rigid.velocity = Vector3.zero; //�̵� �ӵ� �ʱ�ȭ
       rigid.angularVelocity = Vector3.zero; // ȸ�� �ӵ� �ʱ�ȭ

        foreach (Collider collider in arrow.GetComponentsInChildren<Collider>())
        {
            collider.enabled = true;
        }

        //Collider arrowCollider = arrow.GetComponent<Collider>();
        //arrowCollider.enabled = true; // �ݶ��̴� �ٽ� ����

        //Debug.Log($"ȭ�� �ʱ�ȭ �Ϸ�");


    }

}