using UnityEngine;
using Zenject;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Player playerPrefab;

    public Player Player { get; private set; }

    private DiContainer _diContainer;
    
    [Inject]
    private void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    private void Awake()
    {
        Player player = _diContainer.InstantiatePrefabForComponent<Player>(playerPrefab, transform.position, Quaternion.identity, transform);
        Player = player;
    }
}
