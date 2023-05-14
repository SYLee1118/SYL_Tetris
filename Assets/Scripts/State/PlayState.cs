using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : GameStateBase
{
	private int score = 0;

	public PlayState(StateController _owner) : base(_owner)
	{

	}

	private void SetBlockSpeed()
	{
		float fallSpeed = Mathf.Min(3f + score * 0.2f, 10f);

		owner.BlockController.SetBlockSpeed(fallSpeed);
	}

	public override void Enter()
	{
		score = 0;
		owner.UIController.SetText($"{score}");
		SoundManager.it.PlayBGM("bgm");

		SetBlockSpeed();
	}

	public override void Exit()
	{

	}

	public override void Update()
	{
		if (owner.BlockController.CurrnetBlock == null)
		{
			owner.BlockController.CreateRandomBlock((block) =>
			{
				score++;
				owner.UIController.SetText($"{score}");
				owner.CameraController.SetBlockPosition(block.transform.position);
				block.SetOwner(owner);
			});
			SetBlockSpeed();
		}
	}
}
