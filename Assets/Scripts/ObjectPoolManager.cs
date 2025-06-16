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
    
    // 생성
    private GameObject CreateReloadArrow()   //장전용화살 
    {
        return Instantiate(reloadArrowPrefab);
    }

    private GameObject CreateFireArrow()    //발사용화살
    {
        return Instantiate(fireArrowPrefab);
    }

    // 사용
    private void OnTakeFromPool(GameObject poolGo)
    {
        ArrowReset(poolGo);
        poolGo.SetActive(true);
    }
    
    // 반환
    private void OnReturnedToPool(GameObject poolGo)
    {
        ArrowReset(poolGo);
        poolGo.SetActive(false);
    }
    
    // 삭제
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


   public void ArrowReset(GameObject arrow)  //화살 리셋
   {
       if (arrow == null) return;
   
       arrow.transform.SetParent(null);  //부모 해제
       arrow.transform.localScale = Vector3.one;  // 스케일 초기화
       arrow.transform.localRotation = Quaternion.identity;
       Rigidbody rigid = arrow.GetComponent<Rigidbody>();
       rigid.isKinematic = false; // 기네마틱 끔
       rigid.velocity = Vector3.zero; //이동 속도 초기화
       rigid.angularVelocity = Vector3.zero; // 회전 속도 초기화

        foreach (Collider collider in arrow.GetComponentsInChildren<Collider>())
        {
            collider.enabled = true;
        }

        //Collider arrowCollider = arrow.GetComponent<Collider>();
        //arrowCollider.enabled = true; // 콜라이더 다시 켜줌

        //Debug.Log($"화살 초기화 완료");


    }

}