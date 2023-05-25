using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Boost : MonoBehaviour
{
    public event Action<StatType> OnBoostUsedEvent;
    
    [SerializeField] private StatType type;
    [SerializeField] private Button button;
    [SerializeField] private Text boostCountText;

    private int _boostCount;
    private string _boostCountSavePath;
    
    private Storage _storage;
    
    public StatType Type => type;

    [Inject]
    private void Construct(Storage storage)
    {
        _storage = storage;
    }

    private void Awake()
    {
        SetupBoostByType();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(UseBoost);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(UseBoost);
    }

    private void UseBoost()
    {
        DisableButton();

        _boostCount -= 1;
        boostCountText.text = _boostCount + "";
        _storage.Save(_boostCountSavePath, _boostCount);
        
        OnBoostUsedEvent?.Invoke(type);
    }

    public void EnableButton()
    {
        button.interactable = true;
    }
    
    public void DisableButton()
    {
        button.interactable = false;
    }
    
    private void SetupBoostByType()
    {
        switch (type)
        {
            case StatType.Strenghth:
                _boostCountSavePath = Storage.strengthBoostCount;
                _boostCount = (int)_storage.Load(Storage.strengthBoostCount, StoreDataType.Int, 0);
                boostCountText.text = _boostCount + "";
                break;
            case StatType.AttackSpeed:
                _boostCountSavePath = Storage.attackSpeedBoostCount;
                _boostCount = (int)_storage.Load(Storage.attackSpeedBoostCount, StoreDataType.Int, 0);
                boostCountText.text = _boostCount + "";
                break;
            case StatType.Guard:
                _boostCountSavePath = Storage.guardBoostCount;
                _boostCount = (int)_storage.Load(Storage.guardBoostCount, StoreDataType.Int, 0);
                boostCountText.text = _boostCount + "";
                break;
        }
    }
}
