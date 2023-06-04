using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : State
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
