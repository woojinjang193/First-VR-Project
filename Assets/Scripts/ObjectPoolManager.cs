using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{

    public static ObjectPoolManager instance;

    public int defaultCapacity = 10;
    public int maxPoolSize = 15;
    public GameObject Prefab;


    public IObjectPool<GameObject> Pool { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);


        Init();
    }

    private void Init()
    {
        Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);

        for (int i = 0; i < defaultCapacity; i++)
        {
            GameObject arrow = CreatePooledItem(); 
            Pool.Release(arrow);
        }
    }

    // ����
    private GameObject CreatePooledItem()
    {
        GameObject poolGo = Instantiate(Prefab);
        return poolGo;
    }

    // ���
    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
        poolGo.transform.localScale = Vector3.one;  //Ǯ���� ������ ������ �ʱ�ȭ
        Rigidbody rigid = poolGo.GetComponent<Rigidbody>();  // Ǯ���� ������ Ű�׸�ƽ ����
        rigid.isKinematic = false;
        
    }

    // ��ȯ
    private void OnReturnedToPool(GameObject poolGo)
    {

        poolGo.SetActive(false);
    }

    // ����
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }

    public GameObject GetFromPool()
    {
        return Pool.Get();
    }

    public void ReturnToPool(GameObject obj)
    {
        Pool.Release(obj);
    }

}