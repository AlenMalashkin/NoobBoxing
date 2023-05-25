using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private Enemy enemyPrefab;

	public Enemy Enemy { get; private set; }

	private DiContainer _diContainer;
    
	[Inject]
	private void Construct(DiContainer diContainer)
	{
		_diContainer = diContainer;
	}

	private void Awake()
	{
		Enemy enemy = _diContainer.InstantiatePrefabForComponent<Enemy>(enemyPrefab, transform.position, Quaternion.identity, transform);
		Enemy = enemy;
	}
}
