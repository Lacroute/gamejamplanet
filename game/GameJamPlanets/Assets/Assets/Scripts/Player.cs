using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour {

	private enum playerState{
		rookie, //première connexion, aucun message envoyé recu, jamais eu de tuto
		confirmed, //Déja inscrit, tuto déjà visualisé
	}


	private int hexid;

	private List<Message> pending_messages;
	private List<Message> send_messages;
	Message current_message;
	GameManager gameManagerScript;

	public Player(int id, List<Message> pm, List<Message> sm)
	{
		this.hexid = id;
		this.pending_messages = pm;
		this.send_messages = sm;
		gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
	}


	// Use this for initialization
	void Start () {
		//gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
		Debug.Log(gameManagerScript.test);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(gameManagerScript.test);
	}
}
