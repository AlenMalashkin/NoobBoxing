using UnityEngine;
using UnityEngine.SceneManagement;

public class Startup : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("Menu");
    }
}
