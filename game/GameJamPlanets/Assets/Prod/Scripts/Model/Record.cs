using UnityEngine;
using System.Collections;

public class Record{

	private int id;
	private string data;
	private int history;
	private int author_id;


	// Special constructor for DB connexion.
	public Record (RecordDBModel record_from_db)
	{
		this.id = record_from_db.id;
		this.data = record_from_db.data;
		this.history = record_from_db.history;
		this.author_id = record_from_db.author_id;
	}



	/// <summary>
	/// Properties.
	/// </summary>
	public int Id {
		get {
			return this.id;
		}
	}

	public string Data{
		get {
			return this.data;
		}
	}

	//	Standardize the display of infos
	public override string ToString()
	{
		return string.Format("id: {0}, data:{1}, history:{2}, author_id:{3}",id, data, history, author_id);
	}
}

