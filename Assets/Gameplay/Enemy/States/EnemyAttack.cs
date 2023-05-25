using System.Collections;
using UnityEngine;

public class EnemyAttack : State
{
    private float _attackSpeed = 1f;
    private int _damage = 5;
    
    private PlayerHealth _playerHealth;

    public override void EnterState()
    {
        StartCoroutine(Attack());
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        StopCoroutine(Attack());
    }

    public void SetAttackTarget(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackSpeed); 
            _playerHealth.TakeDamage(_damage);
        }
    }
}
