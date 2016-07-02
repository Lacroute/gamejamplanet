using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {
	

	//UI
	public InputField inputfield;






	public enum gameState
	{
		initializationGame,
		introRookie,
		gameStarted,
	}


	private gameState current_game_state;

	private Player current_player;

	public string test;


	public void setGameState(gameState gs)
	{
		current_game_state = gs;

		if (current_game_state == gameState.initializationGame) 
		{
			//anim?
		}
		else if (current_game_state == gameState.introRookie) 
		{
			//anim?
		}
		else if (current_game_state == gameState.gameStarted) 
		{
			//anim?
		}
		Debug.Log ("NewState: " + current_game_state);
	}

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
		WWW data = getDataFromDB("http//idDujoueurPourRecupCesInfos");
		data = null;

		//si le player est dans la base
		if (data == null) 
		{
			setGameState(gameState.introRookie);
			player = generateNewPlayer();
			setGameState(gameState.gameStarted);
		} 
		else 
		{			
			//test en attendant de vraiment get des choses 
			//string dt = JsonUtility.ToJson(data);
			int id = 1;
			List<Message> pending_messages = null;
			List<Message> send_messages = null;
			Message current_message = null;

			player = new Player(id, pending_messages, send_messages);

			setGameState(gameState.gameStarted);
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
			Player player  = new Player(2, null, null);
			Debug.Log ("New player! Id: " + player.hexid);
			return player;
		}
	}

	IEnumerator WaitFunction(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		print("WaitFunction " + Time.time);
	}


	// Use this for initialization
	void Start () {
		inputfield = GameObject.Find ("Canvas").transform.GetChild(0).transform.GetChild(0).GetComponent<InputField>();
		//on lan	ce le jeu, state = initializationGame 
		setGameState(gameState.initializationGame);

		//get id du player
		this.current_player = playerConnection();


		current_player.writeMessage();
	}

	
	// Update is called once per frame
	void Update () {
		
		
	}
}
