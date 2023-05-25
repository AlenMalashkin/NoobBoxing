using System;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
	public event Action OnPlayerDiedEvent;
	
	[SerializeField] private TriggerObserver triggerObserver;
	[SerializeField] private PlayerHealth playerHealth;
	[SerializeField] private StateMachine stateMachine;
	[SerializeField] private PlayerMove playerMove;
	[SerializeField] private PlayerAttack playerAttack;
	[SerializeField] private PlayerDie playerDie;

	private Enemy _enemy;

	[Inject]
	private void Construct(EnemySpawner spawner)
	{
		_enemy = spawner.Enemy;
	}

	private void Start()
	{
		triggerObserver.OnTriggerEnteredEvent += EnterAttackState;
		playerHealth.OnHealthChangedEvent += EnterDieState;
		_enemy.OnEnemyDiedEevnt += OnEnemyDied;
	}

	private void OnDisable()
	{
		triggerObserver.OnTriggerEnteredEvent -= EnterAttackState;
		playerHealth.OnHealthChangedEvent -= EnterDieState;
	}

	private void EnterAttackState(Collider other)
	{
		if (other.TryGetComponent(out Enemy enemy))
		{
			playerAttack.SetAttackTarget(enemy.GetComponent<EnemyHealth>());
			stateMachine.ChangeState(playerAttack);
		}
	}

	private void EnterDieState(int health)
	{
		if (health <= 0)
		{
			OnPlayerDiedEvent?.Invoke();
			stateMachine.ChangeState(playerDie);
		}
	}

	private void OnEnemyDied()
	{
		Debug.Log("Player Wins");
		_enemy.OnEnemyDiedEevnt -= OnEnemyDied;
	}
}    

