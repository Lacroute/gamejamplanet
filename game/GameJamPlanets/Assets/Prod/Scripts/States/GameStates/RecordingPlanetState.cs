using UnityEngine;
using System.Collections;

public class RecordingPlanetState : IGameState {

	// Use this for initialization
	private readonly StatePatternGame gameManager;

	public RecordingPlanetState (StatePatternGame statePatternGame)
	{
		gameManager = statePatternGame	;
	}

	public void Start(){}

	public void UpdateState()
	{

	}

	public void DoBeforeEntering()
	{

	}

	public void DoBeforeLeaving()
	{

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
		Debug.Log ("Can't transition to same state");
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
