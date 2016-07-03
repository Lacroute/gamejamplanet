using UnityEngine;
using System.Collections;

public class Message{

	public enum messageState {
		InTransit,
		Assigned,
		Notified,
		Received,
		Read,
		Killed,
	}

	public string status;
	public string data;
	public int echo_count;
	public int id;
	public int author_id;
	public int target_id;

	private messageState current_state;

	private string content;


	public Message(string s, string d, int ec, int id, int aid, int tid)
	{
		this.status = s;
		this.data = d;
		this.echo_count = ec;
		this.id = id;
		this.author_id = aid;
		this.target_id = tid;
	}

	public Message(PendingMessageDBModel pdm)
	{
		this.status = pdm.status;
		this.data = pdm.data;
		this.echo_count = pdm.echo_count;
		this.id = pdm.id;
		this.author_id = pdm.author_id;
		this.target_id = pdm.target_id;
	}
		
	public void displayMessageInfo()
	{
		Debug.Log ("status: " + this.status + " data: " + this.data + " echocount: " + this.echo_count + " id: " + this.id + " author: " + this.author_id + " target: " + this.target_id );
	}

	public void setState(messageState new_state)
	{
		current_state = new_state;
	}

	public messageState getCurrentState()
	{
		return this.current_state;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
