using Zenject;

public class InfrastructureInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Storage storage = new Storage();

        Container
            .Bind<Storage>()
            .FromInstance(storage)
            .AsSingle();
        
        Bank bank = new Bank();

        Container
            .Bind<Bank>()
            .FromInstance(bank)
            .AsSingle();
    }
}