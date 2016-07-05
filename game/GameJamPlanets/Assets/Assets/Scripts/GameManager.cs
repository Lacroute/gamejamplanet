﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//public class PlayerDBModel
//{
//	public bool message_sent;
//	public int message_count;
//	public int id;
//
//	public PlayerDBModel()
//	{
//		this.message_sent = false;
//		this.message_count = 0;
//		this.id = 0;
//	}
//}

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
	public Button introButton;
	public Button sondeButton;
	public Button acceptButton;
	public Button declineButton;
	public bool boolButton;

	public GameObject myIdeaCapsule;
	public GameObject ideaCapsuleOther;


	public enum request
	{
		FindExistingPlayer,
		FindPendingMessages,
		PostMessage,
	}

	public enum gameState
	{
		splashScreen,
		writeMessage,
		freeMessage,
		waitingSonde,
		sonding,
		acceptIdea,
		declineIdea,
		ideaHost,


		initializationGame,
		introRookie,
		gameStarted,
	}

	public int fakeId;

	public gameState current_game_state;

	public Player current_player;

	//récupération dans la base json => class
	string json_player_DB_model;
	PlayerDBModel player_DB_model;
	string json_pending_message;
	PendingMessageDBModel pending_DB_model;


	public void setGameState(gameState gs)
	{
		current_game_state = gs;

		if (current_game_state == gameState.splashScreen) 
		{
			
		}
		else if (current_game_state == gameState.writeMessage) 
		{
			current_player.writeMessage ();
		}
		else if (current_game_state == gameState.freeMessage) 
		{
			myIdeaCapsule.SetActive (true);
			GameObject.Find ("Game_Title").GetComponent<Animator> ().SetTrigger ("freeMyCapsule");
			setGameState (gameState.waitingSonde);
		}
		else if (current_game_state == gameState.waitingSonde) 
		{
			
		}
		else if (current_game_state == gameState.sonding) 
		{
			ideaCapsuleOther.transform.GetChild(0).GetComponent<ArriveeMessage> ().enabled = true;
			ideaCapsuleOther.transform.GetChild(0).GetComponent<DepartMessage> ().enabled = false;
			ideaCapsuleOther.transform.position = new Vector3 (0, 0, 0);
			ideaCapsuleOther.SetActive (true);
			GameObject.Find ("Game_Title").GetComponent<Animator> ().SetTrigger ("sonding");
		}
		else if (current_game_state == gameState.declineIdea) 
		{
			Debug.Log (ideaCapsuleOther.transform.GetChild(0).GetComponent<ArriveeMessage> ());
			ideaCapsuleOther.transform.GetChild (0).GetComponent<ArriveeMessage> ().messageHasReachedTarget = false;
			ideaCapsuleOther.transform.GetChild(0).GetComponent<ArriveeMessage> ().enabled = false;
			ideaCapsuleOther.transform.GetChild(0).GetComponent<DepartMessage> ().enabled = true;
			setGameState (gameState.waitingSonde);
		}
		else if (current_game_state == gameState.acceptIdea) 
		{
			ideaCapsuleOther.transform.GetChild (0).GetComponent<ArriveeMessage> ().messageHasReachedTarget = false;
			ideaCapsuleOther.transform.GetChild(0).GetComponent<ArriveeMessage> ().enabled = false;
			ideaCapsuleOther.transform.GetChild(0).GetComponent<DepartMessage> ().enabled = true;
			setGameState (gameState.ideaHost);
		}

		else if (current_game_state == gameState.gameStarted) 
		{
			
		}
		Debug.Log ("NewState: " + current_game_state);
	}




//################  access to DB #################

	public WWW PostDataToDB(string message)
	{
		string url = createPostMessageURL(fakeId);


		WWWForm form = new WWWForm();
		form.AddField("data", message);
		WWW www = new WWW(url, form);

		StartCoroutine(WaitForPost(www));

		return www;
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


//##################### Coroutine ########################

	IEnumerator CreationStateCoroutine(){
		yield return new WaitForSeconds (1);
	}

	IEnumerator WaitForPost(WWW www)
	{
		yield return www;

			// check for errors
			if (www.error == null)
			{
				Debug.Log("WWW post message Ok!: " + www.text);
			} 
			else 
			{
				Debug.Log("WWW post message Error: "+ www.error);
			}    
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

			current_player = new Player(player_DB_model.id, player_DB_model.message_sent);

			if (player_DB_model.message_sent) 
			{
				//get message in transit
			}

		
			//get next info : pending message
			getDataFromDB(request.FindPendingMessages);

		} 
		//s 'il y a une erreur, pas le player en question ds la abse => on en créé un
		else 
		{
			Debug.Log("WWW Error find player: "+ www.error);

			//setGameState(gameState.introRookie);
			//player = generateNewPlayer();
			//setGameState(gameState.gameStarted);
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

			if (json_pending_message.Length > 4) 
			{
				//Traitement du json récupéré, on sépare chaque json du tableau 
				json_pending_message = json_pending_message.Remove (0, 1);
				json_pending_message = json_pending_message.Remove (json_pending_message.Length - 1, 1);

				string[] pendingMessages = json_pending_message.Split (new string[] { "},{" }, System.StringSplitOptions.None);

				for (int i = 0; i < pendingMessages.Length; i++) {

					if (pendingMessages.Length != 1) 
					{
						if (i == 0)
							pendingMessages [i] = pendingMessages [i] + "}";
						else if (i == pendingMessages.Length - 1)
							pendingMessages [i] = "{" + pendingMessages [i];
						else
							pendingMessages [i] = "{" + pendingMessages [i] + "}";
					}

					pending_DB_model = JsonUtility.FromJson<PendingMessageDBModel> (pendingMessages [i]);
					Message m = new Message (pending_DB_model);
					current_player.addPendingMessage (m);
				}	
			}
			Debug.Log ("All player data loaded");


			//current_player.writeMessage();
		} 
		else 
		{
			Debug.Log("WWW Error pendings messages: "+ www.error);
		}  
	}



//#################  Utils ##########################
		
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

	public void startGameButton(){

			GameObject.Find ("Game_Title").GetComponent<Animator> ().SetTrigger ("clickButton");
			setGameState (gameState.writeMessage);
		
	}

	public void sondeButtonAction()
	{
		Debug.Log ("click sonde");
		setGameState (gameState.sonding);
	}

	public void acceptButtonAction()
	{
		Debug.Log ("accept");
		GameObject.Find ("Game_Title").GetComponent<Animator> ().SetTrigger ("acceptIdeaWhenNoIdea");
		setGameState (gameState.acceptIdea);
	}

	public void declineButtonAction()
	{
		Debug.Log ("decline");
		GameObject.Find ("Game_Title").GetComponent<Animator> ().SetTrigger ("declineIdeaWhenNoIdea");
		setGameState (gameState.declineIdea);
	}





		

//############# Generation urls get/post database #########################

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

	public string createPostMessageURL(int playerId)
	{	
		string url = "http://0.0.0.0:3000/api/Players/" + playerId + "/author";
		return url;
	}







//####################### Methode native unity ###########################

	// Use this for initialization
	void Start () {

		ideaCapsuleOther = GameObject.Find ("MessageHolder");
		ideaCapsuleOther.SetActive(false);

		myIdeaCapsule = GameObject.Find ("MyRecordLaunch");
		myIdeaCapsule.SetActive(false);

		fakeId = 1;

		//instanciation json et class modele pour la database
		player_DB_model = new PlayerDBModel();
		json_player_DB_model = JsonUtility.ToJson(player_DB_model);

		list_pending_DB = new List<PendingMessageDBModel> ();
		pending_DB_model = new PendingMessageDBModel ();
		json_pending_message = JsonUtility.ToJson(pending_DB_model);

		inputfield = GameObject.Find ("Game_Title").transform.GetChild(0).transform.GetChild(0).GetComponent<InputField>();
		Debug.Log (inputfield);

		//on lance le jeu, state = initializationGame 
		setGameState(gameState.splashScreen);

		//get info du player ou créé un player s'il n existe pas 
		getDataFromDB(request.FindExistingPlayer);
	}

	void Update (){
		if(current_game_state == gameState.splashScreen){

			if(Input.GetMouseButtonDown(0)){
				GameObject.Find("Game_Title").GetComponent<Animator> ().SetBool ("Title_Disappear",true);
			}

			introButton.onClick.RemoveAllListeners();
			introButton.onClick.AddListener (startGameButton);
		}

		if(current_game_state == gameState.waitingSonde){

			sondeButton.onClick.RemoveAllListeners();
			sondeButton.onClick.AddListener (sondeButtonAction);
		}


		if(current_game_state == gameState.sonding){

			if (ideaCapsuleOther.transform.GetChild (0).GetComponent<ArriveeMessage> ().messageHasReachedTarget) 
			{
				acceptButton.onClick.RemoveAllListeners();
				acceptButton.onClick.AddListener (acceptButtonAction);
			
				declineButton.onClick.RemoveAllListeners();
				declineButton.onClick.AddListener (declineButtonAction);
			}

		}

	}

}
