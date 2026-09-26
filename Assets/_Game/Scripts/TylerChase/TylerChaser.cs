using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public sealed class TylerChaser : MonoBehaviour
{
    [SerializeField, Min(0.1f)] private float catchDistance = 1.2f;
    [SerializeField, Min(0.02f)] private float repathInterval = 0.1f;
    [SerializeField] private GameOverController gameOverController;

    private NavMeshAgent agent;
    private Transform target;
    private float nextRepathTime;
    private bool isChasing;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = catchDistance;
    }

    private void Update()
    {
        if (!isChasing || target == null || gameOverController.IsGameOver)
            return;

        Vector3 offset = target.position - transform.position;
        offset.y = 0f;

        if (offset.sqrMagnitude <= catchDistance * catchDistance)
        {
            CatchPlayer();
            return;
        }

        if (Time.time < nextRepathTime)
            return;

        nextRepathTime = Time.time + repathInterval;

        if (agent.isOnNavMesh)
            agent.SetDestination(target.position);
    }

    public void StartChasing(Transform chaseTarget)
    {
        if (chaseTarget == null || !agent.isOnNavMesh)
            return;

        target = chaseTarget;
        isChasing = true;
        agent.isStopped = false;
        agent.SetDestination(target.position);
    }

    private void CatchPlayer()
    {
        isChasing = false;

        if (agent.isOnNavMesh)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }

        gameOverController.ShowGameOver(target);
    }
}