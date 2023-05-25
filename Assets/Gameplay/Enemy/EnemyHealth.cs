using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    public event Action<int> OnHealthChangedEvent;

    [SerializeField] private int health;

    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = health;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        OnHealthChangedEvent?.Invoke(_currentHealth);
    }
}
