using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EnemyLifebar : MonoBehaviour
{
    [SerializeField] private Image filler;
        
    private EnemySpawner _enemySpawner;
    private Storage _storage;    
    
    [Inject]
    private void Construct(EnemySpawner spawner, Storage storage)
    {
        _enemySpawner = spawner;
        _storage = storage;
    }
    
    private void Start()
    {
        _enemySpawner.Enemy.GetComponent<EnemyHealth>().OnHealthChangedEvent += HealthChanged;
    }
    
    private void OnDisable()
    {
        //_enemySpawner.Enemy.GetComponent<EnemyHealth>().OnHealthChangedEvent -= HealthChanged;
    }
    
    private void HealthChanged(int health)
    {
        filler.fillAmount = (float) health / (100 + (int)_storage.Load(Storage.round, StoreDataType.Int, 0) * 20);
    }
}
