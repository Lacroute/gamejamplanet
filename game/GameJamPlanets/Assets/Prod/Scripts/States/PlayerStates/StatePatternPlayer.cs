using UnityEngine;
using System.Collections;

public class StatePatternPlayer : MonoBehaviour 
{

	[HideInInspector] public IPlayerState currentState;
	[HideInInspector] public DefaultState defaultState;
	[HideInInspector] public SearchIdeaState searchIdeaState;
	[HideInInspector] public RecordingIdeaState recordingIdeaState;

	// Awake s'effectue à la création de l'object sur la scène, AVANT Start().
	private void Awake()
	{
		defaultState = new DefaultState (this);
		searchIdeaState = new SearchIdeaState (this);
		recordingIdeaState = new RecordingIdeaState (this);
	}

	void Start () 
	{
		currentState = defaultState;
	}

	void Update () 
	{
		currentState.UpdateState ();
	}
		
}