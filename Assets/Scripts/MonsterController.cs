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
        if(hasReachedToGate)  //성문도달했을때
        {
            return;
        }

        else  //성문 도달하기전
        {
            
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);  //플레이어와 거리 계산
        
            if (distanceToPlayer <= detectionRange && !hasReachedToGate)  //플레이어가 범위내에 들어오면
            {
                   agent.SetDestination(player.position);  //플레이어 추격 시작
                   Debug.Log("플레이어로 타겟 변경");

                if (distanceToPlayer <= 2f)
                {
                    agent.isStopped = true;
                    agent.velocity = Vector3.zero;
                    Debug.Log("플레이어 앞에서 멈춤");

                }
            }
            else
                {
                    agent.isStopped = false;
                    //isChasingPlayer = false;
                    agent.SetDestination(castleGate.position); //플레이어 감지 안되면 다시 성문으로
                }
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("CastleGate"))
        {
            Debug.Log("성문 도착 내브메쉬 멈춤");
            hasReachedToGate = true;
            agent.isStopped = true;  //성문 도달시 멈춤
            agent.velocity = Vector3.zero;
            
        }

        if (other.CompareTag("FireArrowTip"))
        {
            curHp -= takeDamage;
            Debug.Log("몬스터 체력:" + curHp);

            if (curHp <= 0)
            {
                MonsterDie();
            }
        }

        if (other.CompareTag("LoadArrowTip"))
        {
            curHp -= 1;
            //Debug.Log("충돌:" + other.name);
            Debug.Log("몬스터 체력:" + curHp);  //두번호출되는 이유 찾아야함

            if (curHp <= 0)
            {
                MonsterDie();
            }
        }
        
    }

    private void MonsterDie()
    {
        Debug.Log("몬스터쥬금");
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

}
