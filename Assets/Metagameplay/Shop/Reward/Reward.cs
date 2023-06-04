using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

public class Reward : MonoBehaviour
{
    [SerializeField] private int adId;
    [SerializeField] private int rewardByDefault;
    [SerializeField] private Text rewardText;

    private int _reward;
    private Bank _bank;

    [Inject]
    private void Construct(Bank bank)
    {
        _bank = bank;
    }

    private void Awake()
    {
        _reward = rewardByDefault * PlayerPrefs.GetInt("Round", 1);
        rewardText.text = $"Посмотрите видео и получите {_reward} монет";
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
            _bank.GetMoney(_reward);
    }
}
