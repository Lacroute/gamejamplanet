using UnityEngine;
using System.Collections;

public class ArriveeMessage : MonoBehaviour {
	public GameObject MessageLandTarget;
	private bool RotationTargetReached = false;

	IEnumerator GoToTargetRotation () {
		yield return new WaitForSeconds (0.1F);
		Debug.Log ("Coroutine");

	}


	void Awake () {
		StartCoroutine ("GoToTargetRotation");
	}
	
	// Update is called once per frame
	void Update () {

		if(RotationTargetReached == false){
			transform.position = new Vector3 (Mathf.Lerp(transform.position.x,MessageLandTarget.transform.position.x,0.1F),Mathf.Lerp(transform.position.y,MessageLandTarget.transform.position.y,0.1F),Mathf.Lerp(transform.position.z,MessageLandTarget.transform.position.z,0.1F));
		}

		if(RotationTargetReached == true){
			transform.RotateAround (MessageLandTarget.transform.position,Vector3.up,3);}
	}

	void GoToTarget(){
		
	}


	}

