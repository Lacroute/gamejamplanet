using UnityEngine;
using System.Collections;

public class InGameState : IGameState 

{
	private readonly StatePatternGame gameManager;

	public InGameState (StatePatternGame statePatternGame)
	{
		gameManager = statePatternGame	;
	}

	public void Start(){
		
	}

	public void UpdateState()
	{

	}

	public void ToSplashState()
	{
		gameManager.currentState = gameManager.splashState;
	}

	public void ToIntroState(){
		gameManager.currentState = gameManager.introState;
	}

	public void ToInGameState()
	{
		Debug.Log ("Can't transition to same state");
	}

}
