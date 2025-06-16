using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class MonsterController : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField] private Transform castleGate;
    [SerializeField] private Transform player;
    [SerializeField] private float detectionRange = 15f;

    [SerializeField] private int maxHp;
    private int curHp;
    [SerializeField] int attackDamage;
    [SerializeField] private int takeDamage;
    [SerializeField] private float attackDelay;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip deathSound;

    [SerializeField] private Slider hpBar;


    private bool isDead = false;

    private bool hasReachedToGate = false;
    private bool hasFoundPlayer = false;
    private bool hasAttacked = false;
    private bool hasDamaged = false;

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
        hpBar.maxValue = maxHp;
        hpBar.value = curHp;
    }

    private void Update()
    {
        if(!hasReachedToGate && !isDead && !GameManager.instance.isGameOver)  //���� �����ϱ� ���̳� �����ʾҰų� ���ӿ����� �ƴҶ�
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

        else  //��������������
        {
            if (!hasAttacked)
            {
                currentTarget = castleGate;
                animator.SetTrigger("Attack");
                hasAttacked = true;
                Invoke("AttackCoolDown", attackDelay);
            }
        }

        if(GameManager.instance.isGameOver)
        {
            animator.SetBool("GameOver", true);
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("CastleGate") && !hasReachedToGate)
        {
            Debug.Log("���� ���� ����޽� ����");
            hasReachedToGate = true;
            agent.isStopped = true;  //���� ���޽� ����
            agent.velocity = Vector3.zero;
        }

        if (other.CompareTag("FireArrowTip"))
        {
            if (!hasDamaged)
            {
                
                curHp -= takeDamage;
                hpBar.value = curHp;
                //Debug.Log("FireArrow �浹:" + other.name);
                Debug.Log("���� ü��:" + curHp);
                hasDamaged = true;
                Invoke("MonsterDamageDelay", 0.5f); //���� ������ �޴� ������ �ð�  0.2���� ���� �����ؾ���

                if (curHp <= 0)
                {
                    Debug.Log("���");
                    MonsterDie();
                }
                else
                {
                    animator.SetTrigger("GetDamaged");
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
                hpBar.value = curHp;
                //Debug.Log("FireArrow �浹:" + other.name);
                Debug.Log("���� ü��:" + curHp);
                hasDamaged = true;
                Invoke("MonsterDamageDelay", 0.5f);

                if (curHp <= 0)
                {
                    Debug.Log("���");
                    MonsterDie();
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

   public void MonsterHit()  //�ִϸ��̼� �̺�Ʈ�� ����
   {
        if (currentTarget == null)
        {
            return;
        }

        audioSource.clip = attackSound;
        audioSource.Play();

        if (currentTarget.CompareTag("Player"))  //���� Ÿ���� �÷��̾��
       {
            currentTarget.GetComponent<PlayerStatus>().TakeDamage(attackDamage);  //PlayerStatus���� TakeDamageȣ�� 
            
            //Debug.Log("�÷��̾�� ������!");
        }
        else if (currentTarget.CompareTag("CastleGate"))
       {
            //Debug.Log("�������� ������!");
            currentTarget.GetComponent<CastleGateStatus>().TakeDamage(attackDamage); //���� ���� ����
            
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
        audioSource.clip = deathSound; //�״¼Ҹ� ���
        audioSource.Play();

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
