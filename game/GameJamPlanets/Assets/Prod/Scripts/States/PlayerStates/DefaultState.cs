using UnityEngine;
using System.Collections;

// Etat du joueur quand il regarde sa galaxie et ne fait rien.

public class DefaultState : IPlayerState 

{
	private readonly StatePatternPlayer player;

	public DefaultState (StatePatternPlayer statePatternPlayer)
	{
		player = statePatternPlayer	;
	}

	public void UpdateState()
	{

	}

	public void OnTriggerEnter (Collider other)
	{

	}

	public void ToDefaultState(){
		Debug.Log ("Can't transition to same state");
	}

	public void ToSearchIdeaState()
	{
		player.currentState = player.searchIdeaState;
	}

	public void ToRecordingIdeaState()
	{
		player.currentState = player.recordingIdeaState;
	}


}
