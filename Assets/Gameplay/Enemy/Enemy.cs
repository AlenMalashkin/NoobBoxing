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
 	[SerializeField] private EnemyCelebrateState enemyCelebrate;

    private Storage _storage;
    private AfterFight _afterFight;
	private PlayerSpawner _playerSpawner;
	private Player _player;
        
	[Inject]
	private void Construct(PlayerSpawner spawner, Storage storage, AfterFight afterFight)
	{
		_storage = storage;
		_playerSpawner = spawner;
		_afterFight = afterFight;
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
		stateMachine.ChangeState(enemyCelebrate);
		_afterFight.SetMessage("Вы проиграли");
		_afterFight.SetCurrentWave("Раунд: " + (int)_storage.Load(Storage.round, StoreDataType.Int, 1));
		_afterFight.SetReward("Награда: " + 0);
		_afterFight.EnablePanel();
		_player.OnPlayerDiedEvent -= OnPlayerDied;
	}
}    

