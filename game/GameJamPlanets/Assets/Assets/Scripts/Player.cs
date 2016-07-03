using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Player{

	private enum playerState{
		rookie, //première connexion, aucun message envoyé recu, jamais eu de tuto
		confirmed, //Déja inscrit, tuto déjà visualisé
	}


	public int hexid; //ok
	public List<Message> pending_messages;//ok
	public Message current_message; //ok
	public bool current_message_bool; //ok
	public GameManager gameManagerScript; //ok


	public Player(int id, bool b)
	{
		this.hexid = id;
		this.pending_messages = new List<Message>();
		this.current_message = null;
		current_message_bool = b;
		gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
	}


	public void setPendingsMessages(List<Message> list)
	{
		this.pending_messages = list;
	}

	public void addPendingMessage(Message message)
	{
		//chekc a faire si message existe
		this.pending_messages.Add (message);
	}
	
	public void displayInfo()
	{
		if (this.pending_messages != null) 
		{
			Debug.Log ("Id: " + this.hexid + " currentMessageBool: " + this.current_message_bool + " nbPendingMessages: " + this.pending_messages.Count);
			foreach (var message in pending_messages) {
				message.displayMessageInfo ();
			}
		}
	}

	public void writeMessage()
	{
		InputField.SubmitEvent se = new InputField.SubmitEvent();
		gameManagerScript.inputfield.onEndEdit.AddListener(postMessage);


	}

	private void postMessage(string text)
	{
		//j'envoie le message à la db 
		gameManagerScript.PostDataToDB(text);
	}


	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
