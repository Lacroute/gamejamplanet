using UnityEngine;
using System.Collections;

public class SendingRecordState : IGameState {

	// Use this for initialization
	private readonly StatePatternGame gameManager;

	public SendingRecordState (StatePatternGame statePatternGame)
	{
		gameManager = statePatternGame;
	}
		
	public void UpdateState()
	{

	}

	public void DoBeforeEntering()
	{
		Debug.Log ("Before Entering " + gameManager.currentState);
	}

	public void DoBeforeLeaving()
	{
		Debug.Log ("Before Leaving " + gameManager.currentState);
	}

	public void ToSplashState()
	{
		gameManager.currentState = gameManager.splashState;
	}

	public void ToIntroState(){
		gameManager.currentState = gameManager.introState;
	}

	public void ToViewPlanetState()
	{
		gameManager.currentState = gameManager.viewPlanetState;
	}

	public void ToRecordingPlanetState()
	{
		gameManager.currentState = gameManager.recordingPlanetState;
	}

	public void ToListeningRecordState()
	{
		gameManager.currentState = gameManager.sendingRecordState;
	}

	public void ToSendingRecordState()
	{
		Debug.Log ("Can't transition to same state");
	}


}
