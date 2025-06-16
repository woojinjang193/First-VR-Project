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
        Debug.Log("성문 체력: " + curHP);

        if (curHP <= 0)
        {
            GateBrokeDown();
        }
        else
        {
            audioSource.clip = gateGetDamaged; //게이트 공격받는소리
            audioSource.Play();
        }

    }

    private void GateBrokeDown()
    {
        audioSource.clip = gateBrokeDown; //게이트 부셔지는소리
        audioSource.Play();

        Debug.Log("게이트 부셔짐");
        GameManager.instance.GameOver();
    }

}
