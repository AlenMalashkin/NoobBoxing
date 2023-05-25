using System;

public interface IHealth
{
    public event Action<int> OnHealthChangedEvent;
    
    void TakeDamage(int damage);
}
