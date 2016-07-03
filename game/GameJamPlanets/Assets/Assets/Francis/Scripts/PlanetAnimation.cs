using UnityEngine;
using System.Collections;

public class PlanetAnimation : MonoBehaviour {
	public int planetRotationSpeed;
	public GameObject Planet_Target;



	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.forward * Time.deltaTime *  - planetRotationSpeed );


	}





	void OnMouseEnter(){
		GameObject.Find ("Anneau").GetComponent<Renderer> ().enabled = true;
	}
	void OnMouseOver(){
		GameObject.Find ("Anneau").GetComponent<Animator> ().SetBool ("isOut",false);
		GameObject.Find ("Anneau").GetComponent<Animator> ().SetBool ("isOver",true);
	}

	void OnMouseExit(){
		GameObject.Find ("Anneau").GetComponent<Animator>().SetBool ("isOver",false);
		GameObject.Find ("Anneau").GetComponent<Animator>().SetBool ("isOut",true);
	}

}
