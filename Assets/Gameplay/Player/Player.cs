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
	[SerializeField] private PlayerCelebrateState playerCelebrate;

	private Bank _bank;
	private Storage _storage;
	private AfterFight _afterFight;
	private Enemy _enemy;

	[Inject]
	private void Construct(EnemySpawner spawner, Storage storage, AfterFight afterFight, Bank bank)
	{
		_enemy = spawner.Enemy;
		_storage = storage;
		_afterFight = afterFight;
		_bank = bank;
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
		stateMachine.ChangeState(playerCelebrate);
		
		_afterFight.SetMessage("Вы выиграли");
		
		_storage.Save(Storage.round, (int)_storage.Load(Storage.round, StoreDataType.Int, 1) + 1);
		
		_afterFight.SetCurrentWave("Раунд: " + ((int)_storage.Load(Storage.round, StoreDataType.Int, 1) - 1));

		int reward = (int) _storage.Load(Storage.round, StoreDataType.Int, 1) * 500;
		_bank.GetMoney(reward);
		
		_afterFight.SetReward("Награда: " + reward);
		_afterFight.EnablePanel();
		
		_enemy.OnEnemyDiedEevnt -= OnEnemyDied;
	}
}    

