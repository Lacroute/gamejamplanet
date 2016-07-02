using UnityEngine;
using System.Collections;

public class PlanetAnimation : MonoBehaviour {
	public int planetRotationSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.forward * Time.deltaTime *  - planetRotationSpeed );
	}
		
	void OnMouseOver(){
		Debug.Log ("ddfd");
		GameObject.Find ("anneau").GetComponent<Animator>().SetTrigger("isOver");
	}

}
