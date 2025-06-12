
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] List<PooledObject> pool = new List<PooledObject>();
    [SerializeField] PooledObject prefab;
    [SerializeField] int size;
    [SerializeField] Transform returnPosition;

    private void Awake()
    {
        for (int i = 0; i < size; i++)
        {
            PooledObject instance = Instantiate(prefab);
            instance.returnPool = this;
            instance.returnPosition = returnPosition;
            //instance.rigid.isKinematic = true;
            instance.gameObject.SetActive(true);

            pool.Add(instance);
        }
    }


    public PooledObject GetPool(Vector3 position, Quaternion rotation)
    {
        if (pool.Count == 0)
        {
            PooledObject newInstance = Instantiate(prefab, position, rotation);
            newInstance.returnPool = this;
            newInstance.returnPosition = returnPosition;
            newInstance.rigid = newInstance.GetComponent<Rigidbody>();
            //newInstance.rigid.isKinematic = false;
            newInstance.gameObject.SetActive(true);
            return newInstance;
        }

        PooledObject instance = pool[pool.Count - 1];
        pool.RemoveAt(pool.Count - 1);

        instance.returnPool = this;
        instance.transform.position = position;
        instance.transform.rotation = rotation;
        instance.gameObject.SetActive(true);

        return instance;
    }


    public void ReturnPool(PooledObject instance)
    {
        Debug.Log("리턴풀 호출됨");
        //instance.rigid.isKinematic = true;

        if (instance.returnPosition != null)
        {
            instance.transform.position = instance.returnPosition.position;
            instance.transform.rotation = instance.returnPosition.rotation;
        }

        pool.Add(instance);
    }


}
