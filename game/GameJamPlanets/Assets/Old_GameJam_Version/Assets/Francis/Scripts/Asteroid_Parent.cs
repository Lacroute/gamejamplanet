using UnityEngine;
using System.Collections;

public class Asteroid_Parent : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Input.mousePosition);
		transform.position = new Vector3 (Mathf.Lerp(transform.position.x,Mathf.Clamp(Input.mousePosition.x / Screen.width *-10,0,Screen.width),0.1F),Mathf.Lerp(transform.position.y,Mathf.Clamp((Input.mousePosition.y *0.3F) / Screen.height,0,Screen.height),0.1F),transform.position.z);
		
	}

}


//transform.position = new Vector3 (Mathf.Clamp(Input.mousePosition.x / Screen.width /5,0,Screen.width),Mathf.Clamp(Input.mousePosition.y / Screen.height *1.4F,0,Screen.height/50),transform.position.z);
