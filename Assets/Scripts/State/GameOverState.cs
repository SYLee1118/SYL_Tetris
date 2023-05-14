using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : GameStateBase
{
	public GameOverState(StateController _owner) : base(_owner)
	{
	}

	public override void Enter()
	{
		SoundManager.it.PlaySFX("GameOver");
		SoundManager.it.StopBGM();

		Time.timeScale = 0f;
		owner.UIController.SetText("PRESS\nR\nTO\nRESTART");
	}

	public override void Exit()
	{
		Time.timeScale = 1f;
	}

	public override void Update()
	{
		if (Input.GetKeyDown(KeyCode.R)) 
		{
			owner.ChangeState(GameState.Start);
			owner.BlockController.ResetBlocks();
		}
	}
}
