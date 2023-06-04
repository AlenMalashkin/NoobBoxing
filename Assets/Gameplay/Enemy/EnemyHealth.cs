using System;
using UnityEngine;
using Zenject;

public class EnemyHealth : MonoBehaviour, IHealth
{
    public event Action<int> OnHealthChangedEvent;

    private int _currentHealth;

    private Storage _storage;
    
    [Inject]
    private void Construct(Storage storage)
    {
        _storage = storage;
    }
    
    private void Awake()
    {
        _currentHealth = 100 + ((int)_storage.Load(Storage.round, StoreDataType.Int, 0) * 20);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        OnHealthChangedEvent?.Invoke(_currentHealth);
    }
}
