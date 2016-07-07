using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Player{
	

	/******* Online data. *******/
	private int id;
	private string rgba;
	private bool message_sent; // if I already recorded something *** could be replaced by checking if a my_record is set ? ***
	private int message_count;
	private int sharing_id;
	/******* End online data. *******/

	private Color my_color;
	private Record my_record;
	private Record shared_record;


	// Special constructor for Database connection.
	public Player(PlayerDBModel player_from_db){
		this.id = player_from_db.id;
		this.rgba = player_from_db.rgba;
		this.message_sent = player_from_db.message_sent;
		this.message_count = player_from_db.message_count;
		this.sharing_id = player_from_db.sharing_id;

		string[] tmp = player_from_db.rgba.Split (',');
		this.my_color = new Color (
			float.Parse (tmp [0]),
			float.Parse (tmp [1]),
			float.Parse (tmp [2]),
			float.Parse (tmp [3])
		);
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


	// Property my_color.
	public Color MyColor{
		get { return this.my_color;}
		set { this.my_color = value;}
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
		string s = string.Format("id: {0}, my_color:{1}, message_sent:{2}, message_count:{3}, sharing_id:{4}",id, my_color.ToString(), message_sent, message_count, sharing_id);
		if (my_record != null) {
			s += "\n my_record > " + my_record.ToString ();
		}
		if (shared_record != null) {
			s += "\n shared_record > " + shared_record.ToString ();
		}
		return s;
	}
}
