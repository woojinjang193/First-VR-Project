using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField] private Transform castleGate;
    [SerializeField] private Transform player;
    [SerializeField] private float detectionRange = 15f;

    [SerializeField] private int maxHp;
    private int curHp;
    [SerializeField] private int takeDamage;

    private bool hasReachedToGate = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(castleGate.position);
    }

    private void Start()
    {
        curHp = maxHp;
    }

    private void Update()
    {
        if(hasReachedToGate)  //��������������
        {
            return;
        }

        else  //���� �����ϱ���
        {
            
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);  //�÷��̾�� �Ÿ� ���
        
            if (distanceToPlayer <= detectionRange && !hasReachedToGate)  //�÷��̾ �������� ������
            {
                   agent.SetDestination(player.position);  //�÷��̾� �߰� ����
                   Debug.Log("�÷��̾�� Ÿ�� ����");

                if (distanceToPlayer <= 2f)
                {
                    agent.isStopped = true;
                    agent.velocity = Vector3.zero;
                    Debug.Log("�÷��̾� �տ��� ����");

                }
            }
            else
                {
                    agent.isStopped = false;
                    //isChasingPlayer = false;
                    agent.SetDestination(castleGate.position); //�÷��̾� ���� �ȵǸ� �ٽ� ��������
                }
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("CastleGate"))
        {
            Debug.Log("���� ���� ����޽� ����");
            hasReachedToGate = true;
            agent.isStopped = true;  //���� ���޽� ����
            agent.velocity = Vector3.zero;
            
        }

        if (other.CompareTag("FireArrowTip"))
        {
            curHp -= takeDamage;
            Debug.Log("���� ü��:" + curHp);

            if (curHp <= 0)
            {
                MonsterDie();
            }
        }

        if (other.CompareTag("LoadArrowTip"))
        {
            curHp -= 1;
            //Debug.Log("�浹:" + other.name);
            Debug.Log("���� ü��:" + curHp);  //�ι�ȣ��Ǵ� ���� ã�ƾ���

            if (curHp <= 0)
            {
                MonsterDie();
            }
        }
        
    }

    private void MonsterDie()
    {
        Debug.Log("�������");
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}
