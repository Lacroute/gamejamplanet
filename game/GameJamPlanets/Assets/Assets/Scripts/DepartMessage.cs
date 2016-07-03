using UnityEngine;
using System.Collections;

public class DepartMessage : MonoBehaviour {
	public GameObject MessageOutTarget;
	public float departLerpValue = 0.001F;

	void Update () {
		transform.position = new Vector3 (Mathf.Lerp(transform.position.x,MessageOutTarget.transform.position.x,departLerpValue),Mathf.Lerp(transform.position.y,MessageOutTarget.transform.position.y,departLerpValue),Mathf.Lerp(transform.position.z,MessageOutTarget.transform.position.z,departLerpValue));

	}
		
}
