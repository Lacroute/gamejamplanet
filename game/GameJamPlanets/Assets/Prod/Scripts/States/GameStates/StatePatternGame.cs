using UnityEngine;
using System.Collections;

public class StatePatternGame : MonoBehaviour 
{

	[HideInInspector] public IGameState currentState;
	[HideInInspector] public SplashState splashState;
	[HideInInspector] public IntroState introState;
	[HideInInspector] public InGameState inGameState;


	// Awake s'effectue à la création de l'object sur la scène, AVANT Start().
	private void Awake()
	{
		splashState = new SplashState (this);
		introState = new IntroState (this);
		inGameState = new InGameState (this);
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