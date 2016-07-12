using UnityEngine;
using System.Collections;


public class IntroState : IGameState 

{
	private readonly StatePatternGame gameManager;

	public IntroState (StatePatternGame statePatternGame)
	{
		gameManager = statePatternGame	;
	}

	public void UpdateState()
	{
		
	}

	public void DoBeforeEntering()
	{
		
		Debug.Log ("Before Entering introState");
	}

	public void DoBeforeLeaving()
	{
		Debug.Log ("Before Leaving introState");
	}

	public void ToSplashState()
	{
		gameManager.currentState = gameManager.splashState;
	}

	public void ToIntroState(){
		Debug.Log ("Can't transition to same state");
	}

	public void ToViewPlanetState()
	{
		Debug.Log ("Transition: " + gameManager.currentState.ToString() + " to ViewPlanetState");
		gameManager.currentState = gameManager.viewPlanetState;
	}

	public void ToRecordingPlanetState()
	{
		gameManager.currentState = gameManager.recordingPlanetState;
	}

	public void ToListeningRecordState()
	{
		gameManager.currentState = gameManager.listeningRecordState;
	}

	public void ToSendingRecordState()
	{
		gameManager.currentState = gameManager.sendingRecordState;
	}


}
