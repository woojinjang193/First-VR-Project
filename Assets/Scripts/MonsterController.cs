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

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CastleGate"))
        {
            hasReachedToGate = true;
            agent.isStopped = true;  //���� ���޽� ����
            //Debug.Log("���� ���� ����޽� ����");
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
            Debug.Log("���� ü��:" + curHp);

            if (curHp <= 0)
            {
                MonsterDie();
            }
        }
    }

    private void MonsterDie()
    {
        Debug.Log("�������");
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}
