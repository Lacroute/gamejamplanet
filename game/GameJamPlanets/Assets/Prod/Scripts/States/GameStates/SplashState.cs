using UnityEngine;
using System.Collections;
using System.IO;


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

	public void DoBeforeEntering()
	{
		Debug.Log ("Before Entering SplashTest");
	}

	public void DoBeforeLeaving()
	{
		Debug.Log ("Before Leaving SplashTest");

	}

	public void ToSplashState()
	{
		Debug.Log ("Can't transition to same state");
	}
		
		
	public void ToIntroState(){



		/*Debug.Log ("Transition: " + gameManager.currentState.ToString() + " to IntroState");

		//***********************  UI *******************************
		GameObject.FindGameObjectWithTag ("P_Title").SetActive (false);

		//***********************  Model (player) *******************************
		StartCoroutine(DBManager.findMe());

		//***********************  View (planet) *******************************/

		gameManager.currentState.DoBeforeLeaving();

		gameManager.currentState = gameManager.introState;

		gameManager.introState.Start ();


		gameManager.currentState.DoBeforeEntering ();

	}

	public void ToViewPlanetState()
	{
		Debug.Log ("Can't transition from: " + gameManager.currentState.ToString() + " to: ViewPlaneState");
		//gameManager.currentState = gameManager.viewPlanetState;
	}

	public void ToRecordingPlanetState()
	{
		Debug.Log ("Can't transition from: " + gameManager.currentState.ToString() + " to: RecordingPlanetState");
		//gameManager.currentState = gameManager.recordingPlanetState;
	}

	public void ToListeningRecordState()
	{
		Debug.Log ("Can't transition from: " + gameManager.currentState.ToString() + " to: ListeningRecordState");
		//gameManager.currentState = gameManager.listeningRecordState;

	}

	public void ToSendingRecordState()
	{
		Debug.Log ("Can't transition from: " + gameManager.currentState.ToString() + " to: ToSendingRecordState");
		//gameManager.currentState = gameManager.sendingRecordState;
	}
		
}
