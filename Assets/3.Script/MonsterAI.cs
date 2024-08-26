using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player;
    public float chaseRadius = 10f;    // �÷��̾ �Ѿư� �ݰ�
    public float idleTime = 2f;        // Idle ���� ���� �ð�
    public float walkSpeed = 2f;
    public float runSpeed = 6f;
    public Transform[] walkPoints;     // ���Ͱ� ���� �� �ִ� ������

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
            // �÷��̾ ���� �ݰ� ���� ������ ����
            ChasePlayer();
        }
        else
        {
            // �����ο� �ൿ ����
            RandomBehavior();
        }
    }

    void ChasePlayer()
    {
        currentState = 2;  // Run ����
        agent.speed = runSpeed;
        agent.SetDestination(player.position);
        animator.SetInteger("State", 2);  // �ִϸ��̼� ���� ����
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
            // Walk ���¿��� ��ǥ ������ �����ϸ� Idle ���·� ��ȯ
            Idle();
        }
    }

    void Idle()
    {
        agent.ResetPath();
        animator.SetInteger("State", 0);  // Idle �ִϸ��̼� ��� //�Ķ���� �̸� 
    }

    void Walk()
    {
        agent.speed = walkSpeed;
        int randomPoint = Random.Range(0, walkPoints.Length);
        agent.SetDestination(walkPoints[randomPoint].position);
        animator.SetInteger("State", 1);  // Walk �ִϸ��̼� ���
    }
}
