using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyMove : State
{
	[SerializeField] private NavMeshAgent agent;
	
	private PlayerSpawner _playerSpawner;

	[Inject]
	private void Construct(PlayerSpawner spawner)
	{
		_playerSpawner = spawner;
	}
	
	public override void EnterState()
	{
		agent.enabled = true;
	}

	public override void UpdateState()
	{
		agent.SetDestination(_playerSpawner.Player.transform.position);
	}

	public override void ExitState()
	{
		agent.enabled = false;
	}
}
