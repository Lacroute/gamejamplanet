using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {
	

	//UI







	public enum gameState
	{
		initializationGame,
		introRookie,
		gameStarted,
	}


	private gameState current_game_state;

	private Player current_player;

	public string test;


	public WWW getDataFromDB(string url)
	{		
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));

		return www;
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;

		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.data);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}

	public Player playerConnection()
	{
		Player player;
		//get data from db
		WWW data = getDataFromDB("http//petitebite");

		//si le player est dans la base
		if (data == null) 
		{
			current_game_state = gameState.introRookie;
			player = generateNewPlayer();
		} 
		else 
		{
			current_game_state = gameState.gameStarted;
			//test en attendant de vraiment get des choses 
			//string dt = JsonUtility.ToJson(data);
			int id = 1;
			List<Message> pending_messages = null;
			List<Message> send_messages = null;
			Message current_message = null;

			player = new Player(id, pending_messages, send_messages);
		}
			
		return player;
	}

	public Player generateNewPlayer()
	{
		//post sur la base pour créer un nouveau joueur avec un id;
		WWW data = getDataFromDB("http//newplayer");

		if (data == null) {
			Debug.Log ("Impossible de créer un nouveau joueur");
			return null;
		} 
		else 
		{
			//string dt = JsonUtility.ToJson(data);
			Player player  = new Player(1, null, null);
			return player;
		}


	}


	// Use this for initialization
	void Start () {
		//on lan	ce le jeu, state = initializationGame 
		current_game_state = gameState.initializationGame;

		//get id du player
		this.current_player = playerConnection();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
