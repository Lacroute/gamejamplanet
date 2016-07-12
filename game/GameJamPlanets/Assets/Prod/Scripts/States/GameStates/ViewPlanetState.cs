using UnityEngine;
using System.Collections;

public class ViewPlanetState : IGameState 

{
	private readonly StatePatternGame gameManager;

	public ViewPlanetState (StatePatternGame statePatternGame)
	{
		gameManager = statePatternGame	;
	}

	public void Start(){}

	public void UpdateState()
	{

	}

	public void DoBeforeEntering()
	{
		Debug.Log ("Before Leaving " + gameManager.currentState);
	}

	public void DoBeforeLeaving()
	{
		Debug.Log ("Before Entering " + gameManager.currentState);

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
		Debug.Log ("Can't transition to same state");
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
