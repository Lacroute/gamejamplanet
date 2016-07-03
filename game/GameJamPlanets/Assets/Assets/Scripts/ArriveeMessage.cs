using UnityEngine;
using System.Collections;

public class ArriveeMessage : MonoBehaviour {
	public GameObject MessageLandTarget;
	public GameObject MessageRotationTarget;
	public float arriveeLerpValue = 0.001F;
	public float messageRotationSpeed = 2F;
	public bool messageHasReachedTarget = false;

	void Update () {

		if(messageHasReachedTarget == false){

		transform.position = new Vector3 (Mathf.Lerp(transform.position.x,MessageLandTarget.transform.position.x,arriveeLerpValue),Mathf.Lerp(transform.position.y,MessageLandTarget.transform.position.y,arriveeLerpValue),Mathf.Lerp(transform.position.z,MessageLandTarget.transform.position.z,arriveeLerpValue));
		}
		else if(messageHasReachedTarget == true){
			transform.RotateAround (MessageRotationTarget.transform.position, Vector3.up,messageRotationSpeed);
		}

	}


	void OnTriggerEnter(Collider other){
		if(other.tag == "MessageLandTarget"){
			messageHasReachedTarget = true;
		}
	}

}