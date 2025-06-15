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

    private bool isChasingPlayer;
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
        if(hasReachedToGate || agent.isStopped)  //��������������, ����޽���ž�϶�
        {
            return;
        }

        else
        { 
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);  //�÷��̾�� �Ÿ� ���
        
        if (distanceToPlayer <= detectionRange && !hasReachedToGate)  //�÷��̾ �������� ������
        {
            if (!isChasingPlayer)  //�÷��̾ �̹� ���󰡴����� �ƴҶ�
            {
                isChasingPlayer = true;
                //Debug.Log("�÷��̾� ����");
            }
            agent.SetDestination(player.position);  //�÷��̾� �߰� ����
            //Debug.Log("�÷��̾�� Ÿ�� ����");
        }

        else

        agent.SetDestination(castleGate.position); //�÷��̾� ���� �ȵǸ� �ٽ� ��������
        }

        if(curHp <= 0) 
        {
            MonsterDie();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CastleGate"))
        {
            hasReachedToGate = true;
            agent.isStopped = true;  //���� ���޽� ����
            //Debug.Log("���� ���� ����޽� ����");
        }

        if( other.CompareTag("FireArrowTip"))  //�浹ü�� ȭ�����϶�
        {
            curHp -= takeDamage;
            Debug.Log("���� ü��:" + curHp);
        }

        if (other.CompareTag("LoadArrowTip"))  //�浹ü�� ȭ�����϶�
        {
            curHp -= 1;
            Debug.Log("���� ü��:" + curHp);
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
