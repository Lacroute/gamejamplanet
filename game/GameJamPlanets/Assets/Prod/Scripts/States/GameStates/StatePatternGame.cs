using UnityEngine;
using System.Collections;

public class StatePatternGame : MonoBehaviour 
{

	[HideInInspector] public IGameState currentState;
	[HideInInspector] public SplashState splashState;
	[HideInInspector] public IntroState introState;
	[HideInInspector] public ViewPlanetState viewPlanetState;
	[HideInInspector] public RecordingPlanetState recordingPlanetState;
	[HideInInspector] public ListeningRecordState listeningRecordState;
	[HideInInspector] public SendingRecordState sendingRecordState;


	// Awake s'effectue à la création de l'object sur la scène, AVANT Start().
	void Awake()
	{
		splashState = new SplashState (this);
		introState = new IntroState (this);
		viewPlanetState = new ViewPlanetState (this);
		recordingPlanetState = new RecordingPlanetState (this);
		listeningRecordState = new ListeningRecordState (this);
		sendingRecordState = new SendingRecordState (this);
	}

	void Start () 
	{
		currentState = splashState;
	}

	void Update () 
	{
		currentState.UpdateState ();
	}
		
}