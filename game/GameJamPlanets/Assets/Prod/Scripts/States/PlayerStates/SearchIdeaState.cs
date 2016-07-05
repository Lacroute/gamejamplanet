using UnityEngine;
using System.Collections;

// Etat du joueur lorsqu'il cherche un message (sonder).
// Il y entre en cliquant sur "Sonder"
// Il en sort lorsqu'il prend une décision : propager l'idée ou Kill l'idée.
// Suite à quoi il retourne au default state.

public class SearchIdeaState : IPlayerState 

{
	private readonly StatePatternPlayer player;

	public SearchIdeaState (StatePatternPlayer statePatternPlayer)
	{
		player = statePatternPlayer	;
	}

	public void UpdateState()
	{

	}

	public void OnTriggerEnter (Collider other)
	{

	}

	public void ToDefaultState()
	{
		player.currentState = player.defaultState;
	}

	public void ToSearchIdeaState()
	{
		Debug.Log ("Can't transition to same state");
	}

	public void ToRecordingIdeaState(){
		player.currentState = player.recordingIdeaState;
	}


}
