using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Arrow : MonoBehaviour
{
    public IObjectPool<GameObject> Pool { get; set; }
    [SerializeField] Rigidbody rigid;
    [SerializeField]  private float returnTime = 5f;


    public bool isLoaded = false;
    private bool isReturned = false;

    private void OnEnable()
    {
        isReturned = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log($"충돌한 태그: {collision.collider.tag}");

        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Arrow") || collision.collider.CompareTag("ArrowTip"))
        {
            return;
        }

        else
        {
            rigid.isKinematic = true;
            Invoke("ArrowReturnPool", returnTime);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (isLoaded)
        {
            return;
        }

        if (other.CompareTag("PullingPoint"))
        {
            Debug.Log("장전");
            isLoaded = true;
            GameManager.instance.ArrowLoad();
            ArrowReturnPool();
        }

        
    }

    public void ArrowReturnPool()
    {
        if (isReturned)
        {
            return;
        }

        isReturned = true;

        if (Pool != null && gameObject != null)
        {
            Pool.Release(this.gameObject);
        }
    }
}
