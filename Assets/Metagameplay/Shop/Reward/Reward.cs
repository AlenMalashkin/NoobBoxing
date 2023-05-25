using UnityEngine;
using YG;
using Zenject;

public class Reward : MonoBehaviour
{
    [SerializeField] private int adId;
    [SerializeField] private int reward;

    private Bank _bank;

    [Inject]
    private void Construct(Bank bank)
    {
        _bank = bank;
    }
    
    private void OnEnable()
    {
        YandexGame.CloseVideoEvent += GetReward;
    }

    private void OnDisable()
    {
        YandexGame.CloseVideoEvent -= GetReward;
    }

    private void GetReward(int id)
    {
        if (adId == id)
            _bank.GetMoney(reward);
    }
}
