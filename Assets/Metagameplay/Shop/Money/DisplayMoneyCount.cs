using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DisplayMoneyCount : MonoBehaviour
{
    [SerializeField] private Text displayMoneyCount;

    private Bank _bank;
    
    [Inject]
    private void Construct(Bank bank)
    {
        _bank = bank;
    }

    private void Awake()
    {
        displayMoneyCount.text = _bank.Money + "";
    }

    private void OnEnable()
    {
        _bank.OnMoneyCountChangedEvent += OnMoneyCountChanged;
    }

    private void OnDisable()
    {
        _bank.OnMoneyCountChangedEvent -= OnMoneyCountChanged;
    }

    private void OnMoneyCountChanged(int amount)
    {
        displayMoneyCount.text = amount + "";
    }
}
