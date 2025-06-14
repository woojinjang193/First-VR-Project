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

    // 생성
    private GameObject CreatePooledItem()
    {
        GameObject poolGo = Instantiate(Prefab);
        return poolGo;
    }

    // 사용
    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
        poolGo.transform.localScale = Vector3.one;  //풀에서 꺼낼때 스케일 초기화
        Rigidbody rigid = poolGo.GetComponent<Rigidbody>();  // 풀에서 꺼낼때 키네마틱 꺼줌
        rigid.isKinematic = false;
        
    }

    // 반환
    private void OnReturnedToPool(GameObject poolGo)
    {

        poolGo.SetActive(false);
    }

    // 삭제
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