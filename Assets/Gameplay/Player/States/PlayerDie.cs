using UnityEngine;

public class PlayerDie : State
{
    [SerializeField] private Animator animator;
    
    public override void EnterState()
    {
        animator.SetBool("Knoked", true);
    }

    public override void UpdateState()
    {
    }

    public override void ExitState()
    {
        animator.SetBool("Knoked", false);
    }
}
