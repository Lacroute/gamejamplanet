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
	//private List<Message> send_messages;
	public Message current_message; //ok
	public bool current_message_bool; //ok
	public GameManager gameManagerScript; //ok


	public Player(int id, List<Message> pm, Message cm, bool b)
	{
		this.hexid = id;
		this.pending_messages = pm;
		this.current_message = cm;
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
		Debug.Log ("Id: " + this.hexid + " / Pending messages: " + this.pending_messages + " / Current message : " + this.current_message);
		foreach (var message in pending_messages) 
		{
			message.displayMessageInfo ();
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
		Debug.Log(text);
	}


	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
