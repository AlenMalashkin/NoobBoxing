using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerLifebar : MonoBehaviour
{
    [SerializeField] private Image filler;
    
    private Storage _storage;
    private PlayerSpawner _playerSpawner;
    
    [Inject]
    private void Construct(PlayerSpawner spawner, Storage storage)
    {
        _playerSpawner = spawner;
        _storage = storage;
    }

    private void Start()
    {
        _playerSpawner.Player.GetComponent<PlayerHealth>().OnHealthChangedEvent += HealthChanged;
    }

    private void OnDisable()
    {
        //_playerSpawner.Player.GetComponent<PlayerHealth>().OnHealthChangedEvent -= HealthChanged;
    }

    private void HealthChanged(int health)
    {
        filler.fillAmount = (float) health / (100 + (int)_storage.Load(Storage.guard, StoreDataType.Int, 0));
    }
}
