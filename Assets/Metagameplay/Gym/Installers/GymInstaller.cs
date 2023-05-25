using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GymInstaller : MonoInstaller
{
    [SerializeField] private List<DisplayStatAmount> statAmountDisplayers;
    [SerializeField] private List<Boost> boosts;
    
    public override void InstallBindings()
    {
        BindStatAmountDisptayers();
        BindBoosts();
    }

    private void BindStatAmountDisptayers()
    {
        Container
            .Bind<List<DisplayStatAmount>>()
            .FromInstance(statAmountDisplayers)
            .AsSingle();
    }

    private void BindBoosts()
    {
        Container
            .Bind<List<Boost>>()
            .FromInstance(boosts)
            .AsSingle();
    }
}