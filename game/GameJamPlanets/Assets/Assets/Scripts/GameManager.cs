using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerDBModel
{
	public bool message_sent;
	public int message_count;
	public int id;

	public PlayerDBModel()
	{
		this.message_sent = false;
		this.message_count = 0;
		this.id = 0;
	}
}

public class PendingMessageDBModel
{
	public string status;
	public string data;
	public int echo_count;
	public int id;
	public int author_id;
	public int target_id;

	public PendingMessageDBModel()
	{
		this.status = "";
		this.data = "";
		this.echo_count = 0;
		this.id = 0;
		this.author_id = 0;
		this.target_id = 0;
	}
}

//[{"status":"transit","data":"Message from 1","echo_count":0,"id":1,"author_id":1,"target_id":1}]

public class GameManager : MonoBehaviour {
	

	//UI
	public InputField inputfield;

	public enum request
	{
		FindExistingPlayer,
		FindPendingMessages,
	}

	public enum gameState
	{
		initializationGame,
		introRookie,
		gameStarted,
	}

	private int fakeId;

	private gameState current_game_state;

	private Player current_player;

	//récupération dans la base json => class
	string json_player_DB_model;
	PlayerDBModel player_DB_model;
	string json_pending_message;
	PendingMessageDBModel pending_DB_model;

	List<PendingMessageDBModel> list_pending_DB;


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

	public WWW getDataFromDB(request r)
	{	
		string url;
		WWW www;

		if (r == request.FindExistingPlayer) {
			url = createFindPlayerURL (fakeId);	
			www = new WWW (url);
			StartCoroutine (FindExistingPlayer (www));
		} else if (r == request.FindPendingMessages) {
			url = createFindPendingMessagesURL (fakeId);	
			www = new WWW (url);
			StartCoroutine (FindPendingMessages (www));
		} else
			www = null;

		return www;
	}

	IEnumerator FindExistingPlayer(WWW www)
	{
		yield return www;

		//il n'y a pas d'erreurs, le player est dans la base => on recupère ses informations
		if (www.error == null) 
		{
			Debug.Log("WWW find player Ok!: " + www.text);

			json_player_DB_model = www.text;

			player_DB_model = JsonUtility.FromJson<PlayerDBModel>(json_player_DB_model);

			//get info restante : pending message
			WWW www2 = getDataFromDB(request.FindPendingMessages);

			if (player_DB_model.message_sent) 
			{
				//find message sent
			}
			else 
				current_player = new Player(player_DB_model.id, null, null, player_DB_model.message_sent);


			setGameState(gameState.gameStarted);
		} 
		//s 'il y a une erreur, pas le player en question ds la abse => on en créé un
		else 
		{
			Debug.Log("WWW Error find player: "+ www.error);

			setGameState(gameState.introRookie);
			//player = generateNewPlayer();
			setGameState(gameState.gameStarted);
		}    
	}

	IEnumerator FindPendingMessages(WWW www)
	{
		yield return www;

		// check for errors
		//il n'y a pas d'erreurs, le player est dans la base => on recupère ses informations
		if (www.error == null)
		{
			Debug.Log("WWW pending messages Ok!: " + www.text);

			json_pending_message = www.text;
			Debug.Log (json_pending_message);

			//Traitement du json récupéré, on sépare chaque json du tableau 
			json_pending_message = json_pending_message.Remove(0,1);
			json_pending_message = json_pending_message.Remove(json_pending_message.Length-1,1);

			string[] pendingMessages = json_pending_message.Split(new string[] { "},{" }, System.StringSplitOptions.None);

			for(int i = 0; i < pendingMessages.Length; i++) 
			{
				if(i == 0)
					pendingMessages[i] = pendingMessages[i] + "}";
				else if(i == pendingMessages.Length-1)
					pendingMessages[i] = "{" + pendingMessages[i];
				else
					pendingMessages[i] = "{" + pendingMessages[i] + "}";

				Debug.Log (pendingMessages[i]);

				pending_DB_model = JsonUtility.FromJson<PendingMessageDBModel>(pendingMessages[i]);
				list_pending_DB.Add (pending_DB_model);
			}
		
			foreach (var message in list_pending_DB) 
			{
				
				Debug.Log (message.status + " " + message.data + " " + message.echo_count + " " + message.author_id + " " + message.target_id );

			}

			//Debug.Log (json_pending_message);
			//Debug.Log (pending_DB_model.status + " " + pending_DB_model.data + " " + pending_DB_model.echo_count + " " + pending_DB_model.author_id + " " + pending_DB_model.target_id );

			//get info restante : pending message


			//current_player = new Player(id, pending_messages, send_messages, false);

			//setGameState(gameState.gameStarted);
		} 
		else 
		{
			Debug.Log("WWW Error pendings messages: "+ www.error);

			setGameState(gameState.introRookie);
			//player = generateNewPlayer();
			setGameState(gameState.gameStarted);
		}    
	}

	public void playerConnection()
	{
		//le player est il deja dans la base?
		WWW www = getDataFromDB(request.FindExistingPlayer);
	}

	/*public Player generateNewPlayer()
	{
		//post sur la base pour créer un nouveau joueur avec un id;
		WWW data = getDataFromDB(request.FindExistingPlayer);

		if (data == null) {
			Debug.Log ("Impossible de créer un nouveau joueur");
			return null;
		} 
		else 
		{
			//string dt = JsonUtility.ToJson(data);
			Player player  = new Player(2, null, null, false);
			Debug.Log ("New player! Id: " + player.hexid);
			return player;
		}
	}*/

	IEnumerator WaitFunction(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		print("WaitFunction " + Time.time);
	}


	//generation de l'url pour savoir si le player est deja dans la base
	public string createFindPlayerURL(int playerId)
	{
		string request = "{\"where\": {\"id\":" + playerId + "}}";

		string url_existing_player = "http://0.0.0.0:3000/api/Players/findOne?filter=" + WWW.EscapeURL(request);

		return url_existing_player;
	}

	public string createFindPendingMessagesURL(int playerId)
	{
		///Pendingboxes/{id_player}/target
		string url_pendings = "http://0.0.0.0:3000/api//Pendingboxes/" + playerId + "/target";
		return url_pendings;
	}


	// Use this for initialization
	void Start () {

		fakeId = 1;

		//instanciation json et class modele pour la database
		player_DB_model = new PlayerDBModel();
		json_player_DB_model = JsonUtility.ToJson(player_DB_model);

		list_pending_DB = new List<PendingMessageDBModel> ();
		pending_DB_model = new PendingMessageDBModel ();
		json_pending_message = JsonUtility.ToJson(pending_DB_model);

		inputfield = GameObject.Find ("Canvas").transform.GetChild(0).transform.GetChild(0).GetComponent<InputField>();

		//on lance le jeu, state = initializationGame 
		setGameState(gameState.initializationGame);

		//get info du player ou créé un player s'il n existe pas 
		playerConnection();

		//current_player.writeMessage();
	}

	
	// Update is called once per frame
	void Update () {
		
		
	}
}
