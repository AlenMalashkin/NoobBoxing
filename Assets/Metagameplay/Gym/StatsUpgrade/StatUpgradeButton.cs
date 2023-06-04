using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StatUpgradeButton : MonoBehaviour
{
    [SerializeField] private StatType type;
    [SerializeField] private Button button;
    [SerializeField] private Slider progress;
    [SerializeField] private Text progressText;
    [SerializeField] private int targetTapsToUpgradeByDefault = 20;

    private int _targetTapsToUpgrade;
    private int _currentTapsToUpgrade;
    private int _statAmount;
    private int _statIncreaseAmount;
    private string _statAmountSavePath;
    private string _statProgressSavePath;

    private Storage _storage;
    private List<DisplayStatAmount> _statAmountDisplayers;
    private List<Boost> _boosts;
    
    [Inject]
    private void Construct(Storage storage, List<DisplayStatAmount> statAmountDisplayers, List<Boost> boosts)
    {
        _storage = storage;
        _statAmountDisplayers = statAmountDisplayers;
        _boosts = boosts;
    }

    private void Awake()
    {
        _statIncreaseAmount = (int)_storage.Load(Storage.round, StoreDataType.Int, 1);
        
        SetupUpgradeButtonByType();

        _statAmount = (int)_storage.Load(_statAmountSavePath, StoreDataType.Int, 0);
        _currentTapsToUpgrade = (int)_storage.Load(_statProgressSavePath, StoreDataType.Int, 0);
        _targetTapsToUpgrade = targetTapsToUpgradeByDefault;
        
        UpdateProgress();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonClick);

        foreach (var boost in _boosts)
        {
            boost.OnBoostUsedEvent += OnBoostUsed;
        }
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonClick);
        
        foreach (var boost in _boosts)
        {
            boost.OnBoostUsedEvent -= OnBoostUsed;
        }
    }

    private void OnButtonClick()
    {
        CheckProgress();
        
        _currentTapsToUpgrade += 1;

        _storage.Save(_statProgressSavePath, _currentTapsToUpgrade);

        UpdateProgress();
    }

    private void CheckProgress()
    {
        if (_currentTapsToUpgrade >= _targetTapsToUpgrade)
        {
            UpgradeStat();
        }
    }

    private void UpgradeStat()
    {
        _currentTapsToUpgrade = 0;
        _targetTapsToUpgrade = targetTapsToUpgradeByDefault;

        _statAmount += _statIncreaseAmount;
        _storage.Save(_statAmountSavePath, _statAmount);

        foreach (var statAmountDisplayer in _statAmountDisplayers)
        {
            statAmountDisplayer.StatAmountChanged(type);
        }

        foreach (var boost in _boosts)
        {
            if (boost.Type == type)
            {
                boost.EnableButton();
            }
        }
    }

    private void UpdateProgress()
    {
        progress.value = (float) _currentTapsToUpgrade / _targetTapsToUpgrade;

        progressText.text = _currentTapsToUpgrade + "/" + _targetTapsToUpgrade;
    }

    private void OnBoostUsed(StatType statType)
    {
        if (statType == type)
        {
            _targetTapsToUpgrade /= 2; 
            _currentTapsToUpgrade /= 2;
                
            UpdateProgress();    
        }
    }

    private void SetupUpgradeButtonByType()
    {
        switch (type)
        {
            case StatType.Strenghth:
                _statAmountSavePath = Storage.strength;
                _statProgressSavePath = Storage.strengthStatProgress;
                break;
            case StatType.Guard:
                _statAmountSavePath = Storage.guard;
                _statProgressSavePath = Storage.guardStatProgress;
                break;
        }
    }
}
