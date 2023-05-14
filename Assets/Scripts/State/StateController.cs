using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
	Start,
	Play,
	GameOver
}

public abstract class GameStateBase
{
	protected StateController owner;

	public GameStateBase(StateController _owner)
	{
		this.owner = _owner;
	}

	public abstract void Enter();
	public abstract void Update();
	public abstract void Exit();
}

public class StateController : MonoBehaviour
{
	[SerializeField] private BlockController blockController;
	[SerializeField] private UIController uiController;
	[SerializeField] private CameraController cameraController;

	public BlockController BlockController { get { return blockController; } }
	public UIController UIController { get { return uiController; } }
	public CameraController CameraController { get { return cameraController; } }

	private GameStateBase currentState;

	public void ChangeState(GameState state)
	{
		if (currentState != null)
		{
			currentState.Exit();
		}

		switch (state)
		{
			case GameState.Start:
				currentState = new StartState(this);
				break;
			case GameState.Play:
				currentState = new PlayState(this);
				break;
			case GameState.GameOver:
				currentState = new GameOverState(this);
				break;
		}

		currentState.Enter();
	}

	private void Start()
	{
		ChangeState(GameState.Start);
	}

	private void Update()
	{
		if (currentState != null)
		{
			currentState.Update();
		}
	}
}