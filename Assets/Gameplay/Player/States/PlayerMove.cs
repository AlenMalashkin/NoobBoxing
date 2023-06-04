using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class PlayerMove : State
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;

    private EnemySpawner _enemySpawner;

    [Inject]
    private void Construct(EnemySpawner spawner)
    {
        _enemySpawner = spawner;
    }
    
    public override void EnterState()
    {
        animator.SetBool("Walking", true);
        agent.enabled = true;
    }

    public override void UpdateState()
    {
        agent.SetDestination(_enemySpawner.Enemy.transform.position);
    }

    public override void ExitState()
    {
        animator.SetBool("Walking", false);
        agent.enabled = false;
    }
}
