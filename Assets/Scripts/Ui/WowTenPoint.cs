using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WowTenPoint : MonoBehaviour
{
    //���ӽ�ŸƮ ���� 10���� ���߸� ��ƼŬ ���
    [SerializeField] GameObject tenPointParticle;
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(tenPointParticle, transform.position, transform.rotation);
    }
}
