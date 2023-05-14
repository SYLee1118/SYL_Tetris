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
	}

	public override void Exit()
	{
	}

	public override void Update()
	{
		owner.ChangeState(GameState.Play);
	}
}
