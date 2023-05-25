using System;

public class Bank
{
    public event Action<int> OnMoneyCountChangedEvent;
    
    public int Money { get; private set; }

    private Storage _storage;

    public Bank()
    {
        _storage = new Storage();
        Money = (int)_storage.Load(Storage.money, StoreDataType.Int, 0);
    }
    
    public void GetMoney(int amount)
    {
        Money += amount;

        _storage.Save(Storage.money, Money);

        OnMoneyCountChangedEvent?.Invoke(Money);
    }

    public bool SpendMoney(int amount)
    {
        if (Money >= amount)
        {
            Money -= amount;
            _storage.Save(Storage.money, Money);
            OnMoneyCountChangedEvent?.Invoke(Money);
            return true;
        }

        return false;
    }
}
