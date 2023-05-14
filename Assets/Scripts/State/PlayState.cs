using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : GameStateBase
{
	public PlayState(StateController _owner) : base(_owner)
	{

	}

	public override void Enter()
	{
		owner.BlockController.CreateRandomBlock();
	}

	public override void Exit()
	{

	}

	public override void Update()
	{

	}
}
