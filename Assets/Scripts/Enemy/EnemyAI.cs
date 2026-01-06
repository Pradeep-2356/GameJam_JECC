using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;

    [Header("Ranges")]
    public float detectionRange = 10f;
    public float attackRange = 1.5f;

    [Header("Attack")]
    public float attackCooldown = 1.5f;
    private float lastAttackTime;

    [Header("Audio")]
    public AudioSource growlSound;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.stoppingDistance = attackRange;
    }

    private bool hasGrowled = false;

    private void Update()
    {
        if (!agent.isOnNavMesh) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > detectionRange)
        {
            Idle();
            hasGrowled = false;   // reset when player leaves
        }
        else if (distance > attackRange)
        {
            if (!hasGrowled)
            {
                growlSound.Play();
                hasGrowled = true;
            }

            Chase();
        }
        else
        {
            Attack();
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    void Idle()
    {
        agent.ResetPath();
    }

    void Chase()
    {
        agent.SetDestination(player.position);
    }

    void Attack()
    {
        agent.ResetPath();
        FaceTarget();

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            animator.SetTrigger("Attack");
            lastAttackTime = Time.time;
        }
    }

    void FaceTarget()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 8f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
