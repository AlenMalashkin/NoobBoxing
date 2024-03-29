using UnityEngine;

public abstract class State : MonoBehaviour
{
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
