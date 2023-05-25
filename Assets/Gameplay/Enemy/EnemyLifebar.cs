using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EnemyLifebar : MonoBehaviour
{
    [SerializeField] private Image filler;
        
        private EnemySpawner _enemySpawner;
        
        [Inject]
        private void Construct(EnemySpawner spawner)
        {
            _enemySpawner = spawner;
        }
    
        private void Start()
        {
            _enemySpawner.Enemy.GetComponent<EnemyHealth>().OnHealthChangedEvent += HealthChanged;
        }
    
        private void OnDisable()
        {
            _enemySpawner.Enemy.GetComponent<EnemyHealth>().OnHealthChangedEvent -= HealthChanged;
        }
    
        private void HealthChanged(int health)
        {
            filler.fillAmount = (float) health / 5;
        }
}
