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
        if(hasReachedToGate || agent.isStopped)  //성문도달했을때, 내브메쉬스탑일때
        {
            return;
        }

        else
        { 
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);  //플레이어와 거리 계산
        
        if (distanceToPlayer <= detectionRange && !hasReachedToGate)  //플레이어가 범위내에 들어오면
        {
            if (!isChasingPlayer)  //플레이어를 이미 따라가는중이 아닐때
            {
                isChasingPlayer = true;
                //Debug.Log("플레이어 감지");
            }
            agent.SetDestination(player.position);  //플레이어 추격 시작
            //Debug.Log("플레이어로 타겟 변경");
        }

        else

        agent.SetDestination(castleGate.position); //플레이어 감지 안되면 다시 성문으로
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
            agent.isStopped = true;  //성문 도달시 멈춤
            //Debug.Log("성문 도착 내브메쉬 멈춤");
        }

        if( other.CompareTag("FireArrowTip"))  //충돌체가 화살촉일때
        {
            curHp -= takeDamage;
            Debug.Log("몬스터 체력:" + curHp);
        }

        if (other.CompareTag("LoadArrowTip"))  //충돌체가 화살촉일때
        {
            curHp -= 1;
            Debug.Log("몬스터 체력:" + curHp);
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
