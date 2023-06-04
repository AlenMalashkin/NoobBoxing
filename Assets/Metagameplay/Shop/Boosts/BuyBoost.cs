using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BuyBoost : MonoBehaviour
{
    [SerializeField] private StatType type;
    [SerializeField] private Button button;
    [SerializeField] private Text costText;
    [SerializeField] private Text boostCountText;
    [SerializeField] private int cost;

    private int _count;
    private Bank _bank;
    private Storage _storage;
    private string _boostCountSavePath;
    
    [Inject]
    private void Construct(Bank bank, Storage storage)
    {
        _bank = bank;
        _storage = storage;
    }

    private void Awake()
    {
        SetupBoostByType();

        cost *= (int) _storage.Load(Storage.round, StoreDataType.Int, 1);
        
        costText.text = cost + "";
        
        boostCountText.text = _count + "";
    }

    private void OnEnable()
    {
        button.onClick.AddListener(Buy);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(Buy);
    }

    private void Buy()
    {
        if (_bank.SpendMoney(cost))
        {
            _count += 1;
            
            boostCountText.text = _count + "";
            
            _storage.Save(_boostCountSavePath, _count);
        }
    }

    private void SetupBoostByType()
    {
        switch (type)
        {
            case StatType.Strenghth:
                _boostCountSavePath = Storage.strengthBoostCount;
                _count = (int)_storage.Load(Storage.strengthBoostCount, StoreDataType.Int, 0);
                break;
            case StatType.Guard:
                _boostCountSavePath = Storage.guardBoostCount;
                _count = (int)_storage.Load(Storage.guardBoostCount, StoreDataType.Int, 0);
                break;
        }
    }
}
