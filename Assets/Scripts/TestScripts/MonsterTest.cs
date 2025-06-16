using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTest : MonoBehaviour
{
    [SerializeField] private int maxHp;
    private int curHp;
    [SerializeField] private int takeDamage;
    private bool hasDamaged = false;

    private void Start()
    {
        curHp = maxHp;
        Debug.Log("�׽�Ʈ����");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FireArrowTip"))
        {
            if(!hasDamaged)
            {
                curHp -= takeDamage;
                Debug.Log("FireArrow �浹:" + other.name);
                Debug.Log("���� ü��:" + curHp);
                hasDamaged = true;
                Invoke("MonsterDamageDelay", 0.5f);
                if (curHp <= 0)
                {
                    Debug.Log("���");
                }
            }
            else
            {
                return;
            }
            
        }

        if (other.CompareTag("LoadArrowTip")) 
        {
            if (!hasDamaged)
            {
                curHp -= takeDamage;
                Debug.Log("FireArrow �浹:" + other.name);
                Debug.Log("���� ü��:" + curHp);
                hasDamaged = true;
                Invoke("MonsterDamageDelay", 0.5f);
                if (curHp <= 0)
                {
                    Debug.Log("���");
                }
            }
            else
            {
                return;
            }
        }

    }

    private void MonsterDamageDelay()
    {
        hasDamaged = false;
    }

}
