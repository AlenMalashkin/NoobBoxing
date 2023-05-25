using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DisplayStatAmount : MonoBehaviour, IStatObserver
{
	[SerializeField] private StatType type;
	[SerializeField] private Text statAmount;

	private Storage _storage;
	
    private Dictionary<StatType, string> statSavePathesMap = new Dictionary<StatType, string>()
    {
	    {StatType.Strenghth, Storage.strength},
	    {StatType.AttackSpeed, Storage.attackSpeed},
	    {StatType.Guard, Storage.guard}
    };

    [Inject]
    private void Construct(Storage storage)
    {
	    _storage = storage;
    }

    private void Awake()
    {
	    statAmount.text = _storage.Load(statSavePathesMap[type], StoreDataType.Int, 0) + "";
    }

    public void StatAmountChanged(StatType statType)
    {
	    if (type == statType)
			statAmount.text = _storage.Load(statSavePathesMap[statType], StoreDataType.Int, 0) + "";
    }
}
