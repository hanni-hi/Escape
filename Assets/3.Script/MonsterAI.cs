using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player;
    public float chaseRadius = 10f;    // 플레이어를 쫓아갈 반경
    public float idleTime = 2f;        // Idle 상태 지속 시간
    public float walkSpeed = 2f;
    public float runSpeed = 6f;
    public Transform[] walkPoints;     // 몬스터가 걸을 수 있는 지점들

    private NavMeshAgent agent;
    private Animator animator;
    private int currentState;          // 0: Idle, 1: Walk, 2: Run
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentState = 0;
        timer = idleTime;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= chaseRadius)
        {
            // 플레이어가 일정 반경 내에 있으면 추적
            ChasePlayer();
        }
        else
        {
            // 자유로운 행동 패턴
            RandomBehavior();
        }
    }

    void ChasePlayer()
    {
        currentState = 2;  // Run 상태
        agent.speed = runSpeed;
        agent.SetDestination(player.position);
        animator.SetInteger("State", 2);  // 애니메이션 상태 변경
    }

    void RandomBehavior()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            currentState = Random.Range(0, 2); // 0: Idle, 1: Walk
            timer = idleTime;

            if (currentState == 0)
            {
                Idle();
            }
            else if (currentState == 1)
            {
                Walk();
            }
        }

        if (currentState == 1 && agent.remainingDistance <= agent.stoppingDistance)
        {
            // Walk 상태에서 목표 지점에 도착하면 Idle 상태로 전환
            Idle();
        }
    }

    void Idle()
    {
        agent.ResetPath();
        animator.SetInteger("State", 0);  // Idle 애니메이션 재생 //파라메터 이름 
    }

    void Walk()
    {
        agent.speed = walkSpeed;
        int randomPoint = Random.Range(0, walkPoints.Length);
        agent.SetDestination(walkPoints[randomPoint].position);
        animator.SetInteger("State", 1);  // Walk 애니메이션 재생
    }
}
