using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private State stateByDefault;

    private State _currentState;
    
    private void Awake()
    {
        _currentState = stateByDefault;
        _currentState.EnterState();
    }

    private void Update()
    {
        _currentState.UpdateState();
    }

    public void ChangeState(State state)
    {
        if (_currentState != null)
            _currentState.ExitState();

        _currentState = state;
        
        _currentState.EnterState();
    }
}
