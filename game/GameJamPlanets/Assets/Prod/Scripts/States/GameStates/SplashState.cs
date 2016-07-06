using UnityEngine;
using System.Collections;

public class SplashState : IGameState 

{
	private readonly StatePatternGame gameManager;

	public SplashState (StatePatternGame statePatternGame)
	{
		gameManager = statePatternGame	;
	}

	public void UpdateState()
	{

	}

	public void ToSplashState()
	{
		Debug.Log ("Can't transition to same state");
	}
		
	public void ToIntroState(){
		gameManager.currentState = gameManager.introState;
	}

	public void ToInGameState()
	{
		gameManager.currentState = gameManager.inGameState;
	}
		
}
