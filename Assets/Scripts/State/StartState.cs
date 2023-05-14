using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : GameStateBase
{
	public StartState(StateController _owner) : base(_owner)
	{
	}

	public override void Enter()
	{
		owner.UIController.SetText("PRESS\nSPACE");
	}

	public override void Exit()
	{
	}

	public override void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			owner.ChangeState(GameState.Play);
		}
	}
}
