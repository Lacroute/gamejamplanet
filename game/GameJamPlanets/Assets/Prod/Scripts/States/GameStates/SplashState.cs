using UnityEngine;
using System.Collections;

public class SplashState : IGameState 

{
	private readonly StatePatternGame gameManager;

	public SplashState (StatePatternGame statePatternGame)
	{
		gameManager = statePatternGame	;
	}

	public void Start(){

	}

	public void UpdateState()
	{
		if(Input.GetMouseButtonDown(0)){
			ToIntroState ();
		}
	}

	public void ToSplashState()
	{
		Debug.Log ("Can't transition to same state");
	}
		
	public void ToIntroState(){

		GameObject.FindGameObjectWithTag ("P_Title").SetActive (false);
		gameManager.currentState = gameManager.introState;
		gameManager.introState.Start ();
	}

	public void ToInGameState()
	{
		gameManager.currentState = gameManager.inGameState;
	}
		
}
