using System;
using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public event Action<int> OnHealthChangedEvent;

    private Storage _storage;
    private int _currentHealth;

    [Inject]
    private void Construct(Storage storage)
    {
        _storage = storage;
    }

    private void Awake()
    {
        _currentHealth = 100 + (int)_storage.Load(Storage.guard, StoreDataType.Int, 0);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        OnHealthChangedEvent?.Invoke(_currentHealth);
    }
}
