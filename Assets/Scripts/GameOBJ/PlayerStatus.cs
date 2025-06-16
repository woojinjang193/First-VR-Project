using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private int maxHP;
    private int curHP;

    private void Start()
    {
        curHP = maxHP;
        hpBar.maxValue = maxHP;
        hpBar.value = curHP;
    }


    public void TakeDamage(int damage)
    {
        curHP -= damage;
        hpBar.value = curHP;
        Debug.Log("�÷��̾� ü��: " + curHP);
        
        if (curHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("�÷��̾� ���");
        GameManager.instance.GameOver();
    }
    public void PlayerHeal(int amount)
    {
        if (curHP >= maxHP)
        {
            return;
        }
        else
        {
            curHP += amount;
            hpBar.value = curHP;
        }
    }



}
