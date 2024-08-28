using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player;
    public float chaseRadius = 10f;    // �÷��̾ �Ѿư� �ݰ�
    public float idleTime = 5f;        // Idle ���� ���� �ð�
    public float walkSpeed = 2f;
    public float runSpeed = 3f;

    private float walkRadius = 5f;    // ȥ�� ���ƴٴ� �ݰ� 
    private Transform[] walkPoints;     // ���Ͱ� ���� �� �ִ� ������
    private NavMeshAgent agent;
    private Animator animator;
    private int currentState;          // 0: Idle, 1: Walk, 2: Run
    private float timer;

    public float rayLength = 5f; //�����ɽ�Ʈ ����
    public int rayCount = 8; //�����ɽ�Ʈ�� �߻��� ����

    private Coroutine chaseCoroutine;  // �ڷ�ƾ�� �����ϱ� ���� ����

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

        // �����ο� �ൿ ����
        RandomBehavior();

        //�����ɽ�Ʈ�� �� �ν�
        CastRays();
    }

    IEnumerator ChasePlayer()
    {
        Debug.Log("�� CHASE �̾�");
        animator.SetInteger("Run", 2);  // �ִϸ��̼� ���� ����
        currentState = 2;  // Run ���� 
        agent.speed = runSpeed;

        float chaseDuration = 6f; // ���� ���� �ð�
        float chaseTimer = 0f;

        while (chaseTimer < chaseDuration)
        {
            agent.SetDestination(player.position);
            chaseTimer += Time.deltaTime;
            yield return null;
        }

        // 6�� �Ŀ� �ٸ� ���·� ���ư��� (��: Idle ����)
        currentState = 0;
        RandomBehavior();
    }

    void RandomBehavior()
    {
        // timer -= Time.deltaTime;

      //  if (timer <= 0)
      //  {
            currentState = Random.Range(0, 2); // 0: Idle, 1: Walk
           //  timer = idleTime;

            if (currentState == 0)
            {
                // Walk ���� ���� �� Idle ���·� ��ȯ
               //  timer = idleTime; // Idle ���� ���� �ð�
                Idle();
            }
            else if (currentState == 1)
            {
                // Idle ���� ���� �� Walk ���·� ��ȯ
               //  timer = Random.Range(3f, 7f); // Walk ���� ���� �ð�
                Walk();
            }
       // }

        if (currentState == 1 && agent.remainingDistance <= agent.stoppingDistance)
        {
            // Walk ���¿��� ��ǥ ������ �����ϸ� Idle ���·� ��ȯ
         //   timer = idleTime;
            Idle();
        }
    }

    void Idle()
    {

        Debug.Log("�� IDLE �̾�");
        animator.SetInteger("Idle", 0);  // Idle �ִϸ��̼� ��� //�Ķ���� �̸� 
    }

    void Walk()
    {
        Debug.Log("�� WALK �̾�");
        animator.SetInteger("Walk", 1);  // Walk �ִϸ��̼� ���
        agent.speed = walkSpeed;
        int randomPoint = Random.Range(0, walkPoints.Length);
        agent.SetDestination(walkPoints[randomPoint].position);
    }

    void CastRays()
    {
       
        //������Ʈ ����
        float height = GetComponent<Collider>().bounds.size.y;

        //�����ɽ�Ʈ�� �߻��� ���� ��ġ
        Vector3 rayStartPosition = transform.position + Vector3.up * height;

        //������������ ���������� �Ʒ��� �̵���
        for(int i=0;i<rayCount;i++)
        {
            Vector3 rayOrigin = rayStartPosition - Vector3.up * (i * (height / (rayCount - 1)));

            RaycastHit hit;
            if(Physics.Raycast(rayOrigin, transform.forward,out hit, rayLength))
            {
                if(hit.collider.CompareTag("Player"))
                {
                    if (chaseCoroutine != null)
                    {
                        StopCoroutine(chaseCoroutine);  // ���� ���� �ڷ�ƾ�� �ִٸ� ����
                    }
                    chaseCoroutine = StartCoroutine(ChasePlayer());  // ���ο� ���� ����
                    break; // �÷��̾ ã���� ������ ����ĳ��Ʈ�� ����
                }
            }

            //�����ɽ�Ʈ �׸���
            Debug.DrawRay(rayOrigin, transform.forward * rayLength, Color.red);
        
        }
    }
}
