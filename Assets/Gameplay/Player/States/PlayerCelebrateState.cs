using UnityEngine;

public class PlayerCelebrateState : State
{
    [SerializeField] private Animator animator;
    
    public override void EnterState()
    {
        animator.SetBool("Celebrating", true);
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        animator.SetBool("Celebrating", false);
    }
}
