using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleGateStatus : MonoBehaviour
{
    [SerializeField] private int maxHP;
    private int curHP;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gateGetDamaged;
    [SerializeField] private AudioClip gateBrokeDown;

    private void Start()
    {
        curHP = maxHP;
    }


    public void TakeDamage(int damage)
    {
        curHP -= damage;
        Debug.Log("���� ü��: " + curHP);

        if (curHP <= 0)
        {
            GateBrokeDown();
        }
        else
        {
            audioSource.clip = gateGetDamaged; //����Ʈ ���ݹ޴¼Ҹ�
            audioSource.Play();
        }

    }

    private void GateBrokeDown()
    {
        audioSource.clip = gateBrokeDown; //����Ʈ �μ����¼Ҹ�
        audioSource.Play();

        Debug.Log("����Ʈ �μ���");
        GameManager.instance.GameOver();
    }

}
