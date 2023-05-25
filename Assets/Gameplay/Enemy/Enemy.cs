using System;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
	public event Action OnEnemyDiedEevnt;
	
	[SerializeField] private TriggerObserver triggerObserver;
 	[SerializeField] private EnemyHealth enemyHealth;
 	[SerializeField] private StateMachine stateMachine;
 	[SerializeField] private EnemyMove enemyMove;
 	[SerializeField] private EnemyAttack enemyAttack;
 	[SerializeField] private EnemyDie enemyDie;
    
	private PlayerSpawner _playerSpawner;
	private Player _player;
        
	[Inject]
	private void Construct(PlayerSpawner spawner)
	{
		_playerSpawner = spawner;
	}

	private void Start()
	{
		_player = _playerSpawner.Player;
		triggerObserver.OnTriggerEnteredEvent += EnterAttackState;
		enemyHealth.OnHealthChangedEvent += EnterDieState;
		_player.OnPlayerDiedEvent += OnPlayerDied;
	}

	private void OnDisable()
	{
		triggerObserver.OnTriggerEnteredEvent -= EnterAttackState;
		enemyHealth.OnHealthChangedEvent -= EnterDieState;
	}

	private void EnterAttackState(Collider other)
	{
		if (other.TryGetComponent(out Player player))
		{
			enemyAttack.SetAttackTarget(player.GetComponent<PlayerHealth>());
			stateMachine.ChangeState(enemyAttack);
		}
	}

	private void EnterDieState(int health)
	{
		if (health <= 0)
		{
			OnEnemyDiedEevnt?.Invoke();
			stateMachine.ChangeState(enemyDie);
		}
	}

	private void OnPlayerDied()
	{
		Debug.Log("Enemy wins");
		_player.OnPlayerDiedEvent -= OnPlayerDied;
	}
}    

