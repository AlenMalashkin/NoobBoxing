using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyMove : State
{
	[SerializeField] private Animator animator;
	[SerializeField] private NavMeshAgent agent;
	
	private PlayerSpawner _playerSpawner;

	[Inject]
	private void Construct(PlayerSpawner spawner)
	{
		_playerSpawner = spawner;
	}
	
	public override void EnterState()
	{
		animator.SetBool("Walking", true);
		agent.enabled = true;
	}

	public override void UpdateState()
	{
		agent.SetDestination(_playerSpawner.Player.transform.position);
	}

	public override void ExitState()
	{
		animator.SetBool("Walking", false);
		agent.enabled = false;
	}
}
