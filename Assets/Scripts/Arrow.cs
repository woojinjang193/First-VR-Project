using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Arrow : MonoBehaviour
{
    public IObjectPool<GameObject> Pool { get; set; }
    [SerializeField] Rigidbody rigid;
    [SerializeField]  private float returnTime = 5f;


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
            Invoke("ReturnAfterCollision", returnTime);

        }

    }

    private void ReturnAfterCollision()
    {
        Pool.Release(this.gameObject);
    }
}
