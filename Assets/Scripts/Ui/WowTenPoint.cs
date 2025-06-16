using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WowTenPoint : MonoBehaviour
{
    //게임스타트 과녁 10점을 맞추면 파티클 출력
    [SerializeField] GameObject tenPointParticle;
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(tenPointParticle, transform.position, transform.rotation);
    }
}
