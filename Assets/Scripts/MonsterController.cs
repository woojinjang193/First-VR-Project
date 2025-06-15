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
        if(!hasReachedToGate && !isDead)  //성문 도달하기 전이나 죽지않았을때
        {
            
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);  //플레이어와 거리 계산

            animator.SetBool("isWalking", agent.velocity.magnitude > 0.1f); //걷기 애니메이션, 속도 0.1 이상일때 True

            if (distanceToPlayer <= detectionRange && !hasReachedToGate)  //플레이어가 범위내에 들어오면
            {
                if (!hasFoundPlayer)
                {
                    animator.SetTrigger("FoundPlayer");
                    hasFoundPlayer = true;
                }
                
                agent.SetDestination(player.position);  //플레이어 추격 시작
                //Debug.Log("플레이어로 타겟 변경");

                if (distanceToPlayer <= 3f)   
                {
                    agent.isStopped = true;
                    agent.velocity = Vector3.zero;
                    //Debug.Log("플레이어 앞에서 멈춤");

                    if (!hasAttacked)  //플레이어 공격
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
                    agent.SetDestination(castleGate.position); //플레이어 감지 안되면 다시 성문으로
                }
            }

        if (hasReachedToGate)  //성문도달했을때
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
            Debug.Log("성문 도착 내브메쉬 멈춤");
            hasReachedToGate = true;
            agent.isStopped = true;  //성문 도달시 멈춤
            agent.velocity = Vector3.zero;
        }

        if (other.CompareTag("FireArrowTip"))
        {
            curHp -= takeDamage;
            //Debug.Log("몬스터 체력:" + curHp);


            if (curHp <= 0)
            {
                MonsterDie();
            }
        }

        if (other.CompareTag("LoadArrowTip")) //두번호출되는 이유 찾아야함
        {
            curHp -= 1;
            //Debug.Log("충돌:" + other.name);
            //Debug.Log("몬스터 체력:" + curHp);  

            if (curHp <= 0)
            {
                MonsterDie();
            }
        }
        
    }

    public void MonsterHit()  //애니메이션 이벤트로 실행
    {
        //if (currentTarget == null) return;

        if (currentTarget.CompareTag("Player"))
        {
            Debug.Log("플레이어에게 데미지!");
            
        }
        else if (currentTarget.CompareTag("CastleGate"))
        {
            Debug.Log("성문에게 데미지!");

        }

        currentTarget = null; // 공격 종료 후 초기화
    }


    private void AttackCoolDown()
    {
        hasAttacked = false;
    }

    private void MonsterDie()
    {
        isDead = true;
        //Debug.Log("몬스터쥬금");
        animator.SetTrigger("Die");  //죽는 모션
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        foreach (Collider collider in GetComponentsInChildren<Collider>())  //몬스터안의 콜라이더를 모두 찾음
        {
            collider.enabled = false;  //죽은후 충돌안되게 콜라이더 비활성화
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
