﻿using UnityEngine;
using System.Collections;

public class IntroState : IGameState 

{
	private readonly StatePatternGame gameManager;

	public void Start(){
		Debug.Log ("EnteringIntro");
	}

	public IntroState (StatePatternGame statePatternGame)
	{
		gameManager = statePatternGame	;
	}

	public void UpdateState()
	{
		Debug.Log ("IntroSate");
	}

	public void ToSplashState()
	{
		gameManager.currentState = gameManager.splashState;
	}

	public void ToIntroState(){
		Debug.Log ("Can't transition to same state");
	}

	public void ToInGameState()
	{
		gameManager.currentState = gameManager.inGameState;
	}


}
