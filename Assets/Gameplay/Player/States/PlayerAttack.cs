using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerAttack : State
{
    [SerializeField] private Animator animator;
    
    private int _damage;
    
    private EnemyHealth _enemyHealth;
    private Storage _storage;
    
    [Inject]
    private void Construct(Storage storage)
    {
        _storage = storage;
    }
    
    public override void EnterState()
    {
        animator.SetBool("Boxing", true);
        _damage = 10 + (int) _storage.Load(Storage.strength, StoreDataType.Int, 0);
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        animator.SetBool("Boxing", false);
    }
    
    public void SetAttackTarget(EnemyHealth enemyHealth)
    {
        _enemyHealth = enemyHealth;
    }

    private void Attack()
    {
        _enemyHealth.TakeDamage(_damage);  
    }
}
