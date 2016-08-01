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
		GameObject.Find ("P_GameManager").GetComponent<GUIManager> ().StartCoroutine ("LaunchIntro");

		Debug.Log (GameObject.Find ("P_GameManager").GetComponent<ModelController>().Player.MyColor);
	}

	public void DoBeforeLeaving()
	{
		Debug.Log ("Before Leaving introState");

		// Ici il faut définir avec le DBManager vers quel état on va 
		// Si y'a déjà un player ou non 
		// s'il a déjà hosté ou non un record
		// S'il a un record en voyage ou non


			
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
		gameManager.currentState.DoBeforeLeaving ();
		gameManager.currentState = gameManager.viewPlanetState;
		gameManager.currentState.DoBeforeEntering  ();
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
