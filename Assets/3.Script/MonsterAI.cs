using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player;
    public float chaseRadius = 10f;    // 플레이어를 쫓아갈 반경
    public float idleTime = 5f;        // Idle 상태 지속 시간
    public float walkSpeed = 2f;
    public float runSpeed = 3f;

    private float walkRadius = 5f;    // 혼자 돌아다닐 반경 
    private Transform[] walkPoints;     // 몬스터가 걸을 수 있는 지점들
    private NavMeshAgent agent;
    private Animator animator;
    private int currentState;          // 0: Idle, 1: Walk, 2: Run
    private float timer;

    public float rayLength = 5f; //레이케스트 길이
    public int rayCount = 8; //레이케스트를 발사할 개수

    private Coroutine chaseCoroutine;  // 코루틴을 제어하기 위한 변수

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

        // 자유로운 행동 패턴
        RandomBehavior();

        //레이케스트로 앞 인식
        CastRays();
    }

    IEnumerator ChasePlayer()
    {
        Debug.Log("난 CHASE 이야");
        animator.SetInteger("Run", 2);  // 애니메이션 상태 변경
        currentState = 2;  // Run 상태 
        agent.speed = runSpeed;

        float chaseDuration = 6f; // 추적 지속 시간
        float chaseTimer = 0f;

        while (chaseTimer < chaseDuration)
        {
            agent.SetDestination(player.position);
            chaseTimer += Time.deltaTime;
            yield return null;
        }

        // 6초 후에 다른 상태로 돌아가기 (예: Idle 상태)
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
                // Walk 상태 유지 후 Idle 상태로 전환
               //  timer = idleTime; // Idle 상태 지속 시간
                Idle();
            }
            else if (currentState == 1)
            {
                // Idle 상태 유지 후 Walk 상태로 전환
               //  timer = Random.Range(3f, 7f); // Walk 상태 지속 시간
                Walk();
            }
       // }

        if (currentState == 1 && agent.remainingDistance <= agent.stoppingDistance)
        {
            // Walk 상태에서 목표 지점에 도착하면 Idle 상태로 전환
         //   timer = idleTime;
            Idle();
        }
    }

    void Idle()
    {

        Debug.Log("난 IDLE 이야");
        animator.SetInteger("Idle", 0);  // Idle 애니메이션 재생 //파라메터 이름 
    }

    void Walk()
    {
        Debug.Log("난 WALK 이야");
        animator.SetInteger("Walk", 1);  // Walk 애니메이션 재생
        agent.speed = walkSpeed;
        int randomPoint = Random.Range(0, walkPoints.Length);
        agent.SetDestination(walkPoints[randomPoint].position);
    }

    void CastRays()
    {
       
        //오브젝트 높이
        float height = GetComponent<Collider>().bounds.size.y;

        //레이케스트를 발사할 시작 위치
        Vector3 rayStartPosition = transform.position + Vector3.up * height;

        //시작지점에서 점진적으로 아래로 이동함
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
                        StopCoroutine(chaseCoroutine);  // 기존 추적 코루틴이 있다면 멈춤
                    }
                    chaseCoroutine = StartCoroutine(ChasePlayer());  // 새로운 추적 시작
                    break; // 플레이어를 찾으면 나머지 레이캐스트는 무시
                }
            }

            //레이케스트 그리기
            Debug.DrawRay(rayOrigin, transform.forward * rayLength, Color.red);
        
        }
    }
}
