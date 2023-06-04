using UnityEngine;
using UnityEngine.UI;

public class AfterFight : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Text message;
    [SerializeField] private Text currentWave;
    [SerializeField] private Text currentReward;

    public void SetMessage(string text)
    {
        message.text = text;
    }

    public void SetCurrentWave(string wave)
    {
        currentWave.text = wave;
    }

    public void SetReward(string reward)
    {
        currentReward.text = reward;
    }

    public void EnablePanel()
    {
        panel.SetActive(true);
    }
}
