using UnityEngine;
using System.Collections;

public class Message : MonoBehaviour {

	public enum messageState {
		InTransit,
		Assigned,
		Notified,
		Received,
		Read,
		Killed,
	}

	private messageState current_state;

	private string content;

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
