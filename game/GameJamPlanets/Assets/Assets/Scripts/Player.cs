using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Player{

	private enum playerState{
		rookie, //première connexion, aucun message envoyé recu, jamais eu de tuto
		confirmed, //Déja inscrit, tuto déjà visualisé
	}


	/******* Online data. *******/
	// TODO : make everything private
	private int id;
	private string hexid;
	private bool message_sent; // if I already recorded something *** could be replaced by checking if a my_record is set ? ***
	private int message_count;
	private int sharing_id; // the id of a shared record.

	private Record my_record;
	private Record shared_record;
	/******* End online data. *******/



	public Message current_message; //ok *** DEPRECATED use Record myrecord instead ***
	public bool current_message_bool; //ok
	public GameManager gameManagerScript; //ok


	// *** DEPRECATED ? ***
	public Player(int id, bool b)
	{
		this.hexid = id.ToString();
		this.current_message = null;
		current_message_bool = b;
		gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	// *** END DEPRECATED ? ***


	// Special constructor for Database connection.
	public Player(PlayerDBModel player_from_db){
		this.id = player_from_db.id;
		this.hexid = player_from_db.hexid;
		this.message_sent = player_from_db.message_sent;
		this.message_count = player_from_db.message_count;
		this.sharing_id = player_from_db.sharing_id;
	}


	// TODO: little description.
	public void writeMessage()
	{
		/*var input = gameObject.GetComponent<InputField>();
		var se= new InputField.SubmitEvent();
		se.AddListener(SubmitName);
		input.onEndEdit = se;*/

		var se = new InputField.SubmitEvent();
		se.AddListener(postMessage);
		gameManagerScript.inputfield.onEndEdit = se;


		//GameObject idea = Instantiate ();
	}


	// TODO: little description.
	private void postMessage(string text)
	{	//j'envoie le message à la db 
		gameManagerScript.PostDataToDB(text);
		//on change d'état, le message est envoyé
		gameManagerScript.setGameState (GameManager.gameState.freeMessage);
	}



	// Init.
	void Start () {

	}



	// Property sharing_id.
	public int SharingId{
		get { return this.sharing_id;}
		set { this.sharing_id = value;}
	}

	// Property my_record.
	public Record MyRecord{
		get { return this.my_record;}
		set { this.my_record = value;}
	}

	// Property shared_record.
	public Record SharedRecord{
		get { return this.shared_record;}
		set { 
			this.shared_record = value;
			// TODO call DBManager shareRecord(id_new_record) to update the DB.
		}
	}

	//	Standardize the display of infos.
	public override string ToString()
	{
		string s = string.Format("id: {0}, hexid:{1}, message_sent:{2}, message_count:{3}, sharing_id:{4}",id, hexid, message_sent, message_count, sharing_id);
		if (my_record != null) {
			s += "\n my_record > " + my_record.ToString ();
		}
		return s;
	}
}
