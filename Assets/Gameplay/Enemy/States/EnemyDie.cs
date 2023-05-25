using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : State
{
	public override void EnterState()
	{
		Destroy(gameObject);
	}

	public override void UpdateState()
	{
	}

	public override void ExitState()
	{
	}
}
