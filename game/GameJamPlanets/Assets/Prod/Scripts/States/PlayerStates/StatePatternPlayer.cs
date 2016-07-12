using UnityEngine;
using System.Collections;

public class StatePatternPlayer : MonoBehaviour 
{

	[HideInInspector] public IPlayerState currentState;
	[HideInInspector] public DefaultPlayerState defaultPlayerState;
	[HideInInspector] public SearchIdeaState searchIdeaState;
	[HideInInspector] public RecordingIdeaState recordingIdeaState;
	//[HideInInspector] public RecordingIdeaState sendIdeaState; ????

	// Awake s'effectue à la création de l'object sur la scène, AVANT Start().
	private void Awake()
	{
		defaultPlayerState = new DefaultPlayerState (this);
		searchIdeaState = new SearchIdeaState (this);
		recordingIdeaState = new RecordingIdeaState (this);
	}

	void Start () 
	{
		currentState = defaultPlayerState;
	}

	void Update () 
	{
		currentState.UpdateState ();
	}
		
}