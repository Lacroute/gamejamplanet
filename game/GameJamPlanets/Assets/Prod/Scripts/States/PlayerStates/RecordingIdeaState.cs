using UnityEngine;
using System.Collections;

// Etat du joueur lorsqu'il est en train d'écrire son message.
// Il y entre en cliquant sur le bouton "enregistrer depuis le default State"
// Il en sort quand son message a fini d'être envoyé.

public class RecordingIdeaState : IPlayerState 

{
	private readonly StatePatternPlayer player;

	public RecordingIdeaState (StatePatternPlayer statePatternPlayer)
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
		
		player.currentState = player.defaultPlayerState;
	}

	public void ToSearchIdeaState()
	{
		player.currentState = player.searchIdeaState;
	}

	public void ToRecordingIdeaState()
	{
		Debug.Log ("Can't transition to same state");
	}


}
