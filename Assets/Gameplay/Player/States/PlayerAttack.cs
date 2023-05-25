using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerAttack : State
{
    private float _attackSpeed;
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
        _attackSpeed = 1 - (int) _storage.Load(Storage.attackSpeed, StoreDataType.Int, 0) * 0.01f;
        _damage = 1 + (int) _storage.Load(Storage.strength, StoreDataType.Int, 0);
        StartCoroutine(Attack());
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        StopCoroutine(Attack());
    }
    
    public void SetAttackTarget(EnemyHealth enemyHealth)
    {
        _enemyHealth = enemyHealth;
    }

    private IEnumerator Attack()
    {
        while (true)
        { 
            
            yield return new WaitForSeconds(_attackSpeed);
            _enemyHealth.TakeDamage(_damage);  
        }
    }
}
