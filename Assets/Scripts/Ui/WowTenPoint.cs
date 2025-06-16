using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WowTenPoint : MonoBehaviour
{
    //���ӽ�ŸƮ ���� 10���� ���߸� ��ƼŬ ���
    [SerializeField] GameObject tenPointParticle;
    private void OnTriggerEnter(Collider other)
    {
        tenPointParticle.SetActive(true);
        Invoke("ParticleFalse", 2f);
    }

    private void ParticleFalse()
    {
        tenPointParticle.SetActive(false);
    }
}
