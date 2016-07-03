using UnityEngine;
using System.Collections;

public class ArriveeMessage : MonoBehaviour {
	public GameObject MessageLandTarget;

	void Update () {

			transform.position = new Vector3 (Mathf.Lerp(transform.position.x,MessageLandTarget.transform.position.x,0.1F),Mathf.Lerp(transform.position.y,MessageLandTarget.transform.position.y,0.1F),Mathf.Lerp(transform.position.z,MessageLandTarget.transform.position.z,0.1F));

	}

}