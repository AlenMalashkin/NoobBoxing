using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;

    public static Sound Instance => _instance;
    private static Sound _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
}