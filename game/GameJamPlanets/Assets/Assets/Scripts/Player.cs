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
	private List<Message> pending_messages;
	//private List<Message> send_messages;
	Message current_message; //ok
	bool current_message_bool; //ok
	GameManager gameManagerScript; //ok


	public Player(int id, List<Message> pm, Message cm, bool b)
	{
		this.hexid = id;
		this.pending_messages = pm;
		this.current_message = cm;
		current_message_bool = b;
		gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void displayInfo()
	{
		Debug.Log ("Id: " + this.hexid + " / Pending messages: " + this.pending_messages + " / Current message : " + this.current_message);
	}

	public void writeMessage()
	{
		InputField.SubmitEvent se = new InputField.SubmitEvent();
		gameManagerScript.inputfield.onEndEdit.AddListener(postMessage);
	}

	private void postMessage(string text)
	{
		//j'envoie le message à la db 
		Debug.Log(text);
	}


	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
