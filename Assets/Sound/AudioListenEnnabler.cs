using UnityEngine;

public class AudioListenEnnabler : MonoBehaviour
{
    [SerializeField] private AudioListener listener;
    
    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            DisableListener();
        }
        else
        {
            EnableListener();
        }
    }

    public void EnableListener()
    {
        listener.enabled = true;
    }

    public void DisableListener()
    {
        listener.enabled = false;
    }
}
