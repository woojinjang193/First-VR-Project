using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int maxHP;
    private int curHP;

    private void Start()
    {
        curHP = maxHP;
    }


    public void TakeDamage(int damage)
    {
        curHP -= damage;
        Debug.Log("플레이어 체력: " + curHP);
        
        if (curHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("플레이어 사망");
        GameManager.instance.GameOver();
    }



}
