using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class MonsterController : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField] private Transform castleGate;
    [SerializeField] private Transform player;
    [SerializeField] private float detectionRange = 15f;

    [SerializeField] private int maxHp;
    private int curHp;
    [SerializeField] private int takeDamage;
    [SerializeField] private float attackDelay;
    private bool isDead = false;

    private bool hasReachedToGate = false;
    private bool hasFoundPlayer = false;
    private bool hasAttacked = false;

    private Transform currentTarget;

    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(castleGate.position);
    }

    private void Start()
    {
        curHp = maxHp;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!hasReachedToGate && !isDead)  //���� �����ϱ� ���̳� �����ʾ�����
        {
            
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);  //�÷��̾�� �Ÿ� ���

            animator.SetBool("isWalking", agent.velocity.magnitude > 0.1f); //�ȱ� �ִϸ��̼�, �ӵ� 0.1 �̻��϶� True

            if (distanceToPlayer <= detectionRange && !hasReachedToGate)  //�÷��̾ �������� ������
            {
                if (!hasFoundPlayer)
                {
                    animator.SetTrigger("FoundPlayer");
                    hasFoundPlayer = true;
                }
                
                agent.SetDestination(player.position);  //�÷��̾� �߰� ����
                //Debug.Log("�÷��̾�� Ÿ�� ����");

                if (distanceToPlayer <= 3f)   
                {
                    agent.isStopped = true;
                    agent.velocity = Vector3.zero;
                    //Debug.Log("�÷��̾� �տ��� ����");

                    if (!hasAttacked)  //�÷��̾� ����
                    {
                        currentTarget = player;
                        animator.SetTrigger("Attack");
                        hasAttacked = true;
                        Invoke("AttackCoolDown", attackDelay);
                    }
                }
         
            }
            else
                {
                    hasFoundPlayer = false;
                    agent.isStopped = false;
                    //isChasingPlayer = false;
                    agent.SetDestination(castleGate.position); //�÷��̾� ���� �ȵǸ� �ٽ� ��������
                }
            }

        if (hasReachedToGate)  //��������������
        {
            if (!hasAttacked)
            {
                currentTarget = castleGate;
                animator.SetTrigger("Attack");
                hasAttacked = true;
                Invoke("AttackCoolDown", attackDelay);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("CastleGate"))
        {
            Debug.Log("���� ���� ����޽� ����");
            hasReachedToGate = true;
            agent.isStopped = true;  //���� ���޽� ����
            agent.velocity = Vector3.zero;
        }

        if (other.CompareTag("FireArrowTip"))
        {
            curHp -= takeDamage;
            //Debug.Log("���� ü��:" + curHp);


            if (curHp <= 0)
            {
                MonsterDie();
            }
        }

        if (other.CompareTag("LoadArrowTip")) //�ι�ȣ��Ǵ� ���� ã�ƾ���
        {
            curHp -= 1;
            //Debug.Log("�浹:" + other.name);
            //Debug.Log("���� ü��:" + curHp);  

            if (curHp <= 0)
            {
                MonsterDie();
            }
        }
        
    }

    public void MonsterHit()  //�ִϸ��̼� �̺�Ʈ�� ����
    {
        //if (currentTarget == null) return;

        if (currentTarget.CompareTag("Player"))
        {
            Debug.Log("�÷��̾�� ������!");
            
        }
        else if (currentTarget.CompareTag("CastleGate"))
        {
            Debug.Log("�������� ������!");

        }

        currentTarget = null; // ���� ���� �� �ʱ�ȭ
    }


    private void AttackCoolDown()
    {
        hasAttacked = false;
    }

    private void MonsterDie()
    {
        isDead = true;
        //Debug.Log("�������");
        animator.SetTrigger("Die");  //�״� ���
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        foreach (Collider collider in GetComponentsInChildren<Collider>())  //���;��� �ݶ��̴��� ��� ã��
        {
            collider.enabled = false;  //������ �浹�ȵǰ� �ݶ��̴� ��Ȱ��ȭ
        }

        Invoke("MonsterBodyFalse", 5f);
    }


    private void MonsterBodyFalse()
    {
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}
