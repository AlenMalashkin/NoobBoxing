using System.Collections;
using UnityEngine;
using Zenject;

public class EnemyAttack : State
{
    [SerializeField] private Animator animator;
    
    private int _damage = 1;
    
    private PlayerHealth _playerHealth;
    private Storage _storage;
    
    [Inject]
    private void Construct(Storage storage)
    {
        _storage = storage;
    }
    
    public override void EnterState()
    {
        animator.SetBool("Boxing", true);
        _damage = 7 + (int) _storage.Load(Storage.round, StoreDataType.Int, 0) * 5;
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        animator.SetBool("Boxing", false);
    }

    public void SetAttackTarget(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }

    private void Attack()
    {
        _playerHealth.TakeDamage(_damage);
    }
}
