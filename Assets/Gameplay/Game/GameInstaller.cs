using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private AfterFight afterFight;

    public override void InstallBindings()
    {
        BindEnemySpawner();
        BindPlayerSpawner();
        BindAfterFight();
    }

    private void BindPlayerSpawner()
    {
        Container
            .Bind<PlayerSpawner>()
            .FromInstance(playerSpawner)
            .AsSingle();
    }

    private void BindEnemySpawner()
    {
        Container
            .Bind<EnemySpawner>()
            .FromInstance(enemySpawner)
            .AsSingle();
    }

    private void BindAfterFight()
    {
        Container
            .Bind<AfterFight>()
            .FromInstance(afterFight)
            .AsSingle();
    }
}