using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class PooledObject : MonoBehaviour
{
    [SerializeField] public Transform returnPosition;
    [SerializeField] public ObjectPool returnPool;
    [SerializeField] public Rigidbody rigid;
    [SerializeField] float returnTime = 5f;

    private Vector3 arrowReturnPosition;

    private void Start()
    {
        if (returnPosition != null)
        {
            transform.position = returnPosition.position;
            transform.rotation = returnPosition.rotation;
        }
    }

    private void OnEnable()
    {

    }


    public void ReturnPool()
    {
        Debug.Log("ReturnPool 호출됨");

        if (returnPool == null)
        {
            Debug.LogWarning("리턴풀없어서 파괴");
            Destroy(gameObject);
        }
        else
        {
            returnPool.ReturnPool(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"충돌한 태그: {collision.collider.tag}");

        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Arrow") || collision.collider.CompareTag("ArrowTip"))
        {
            return;
        }

        else
        {
            //rigid.isKinematic = true;
            Invoke("ReturnAfterCollision", returnTime);

        }

    }

    private void ReturnAfterCollision()
    {
        ReturnPool();
    }



}
