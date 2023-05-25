using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private EnemySpawner enemySpawner;

    public override void InstallBindings()
    {
        BindEnemySpawner();
        BindPlayerSpawner();
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
}