using UnityEngine;
using System.Collections;

public class DepartMessage : MonoBehaviour {
	public GameObject MessageOutTarget;

	void Update () {

		transform.position = new Vector3 (Mathf.Lerp(transform.position.x,MessageOutTarget.transform.position.x,0.001F),Mathf.Lerp(transform.position.y,MessageOutTarget.transform.position.y,0.001F),Mathf.Lerp(transform.position.z,MessageOutTarget.transform.position.z,0.001F));

	}
}
