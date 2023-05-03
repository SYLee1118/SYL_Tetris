using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
	Start,
	Play,
	GameOver
}

public interface IGameState
{
	void Enter();
	void Update();
	void Exit();
}

public class StateController : MonoBehaviour
{ 
	private IGameState currentState;

	public void ChangeState(GameState state)
	{
		if (currentState != null)
		{
			currentState.Exit();
		}

		switch (state)
		{
			case GameState.Start:
				currentState = new StartState();
				break;
			case GameState.Play:
				currentState = new PlayState();
				break;
			case GameState.GameOver:
				currentState = new GameOverState();
				break;
		}

		currentState.Enter();
	}

	private void Update()
	{
		if (currentState != null)
		{
			currentState.Update();
		}
	}
}