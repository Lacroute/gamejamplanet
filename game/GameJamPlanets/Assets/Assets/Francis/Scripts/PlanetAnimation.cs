using UnityEngine;
using System.Collections;

public class PlanetAnimation : MonoBehaviour {
	public int planetRotationSpeed;
		
	IEnumerator CreationStateCoroutine(){
		GameObject.Find ("Anneau").GetComponent<Animator>().SetBool ("Anneau_Disappear",true);
		yield return new WaitForSeconds (1);

		yield return new WaitForSeconds (3);
		GameObject.Find ("Planet_Name").GetComponent<Animator> ().enabled = true;
		yield return new WaitForSeconds (1);
		// Faire apparaitre le champs de texte
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.forward * Time.deltaTime *  - planetRotationSpeed );

		if(Input.GetMouseButtonDown(0)){
			GoToCreationState ();	
		}
	}


	// On passe à l'état après le splash : la création de la planète
	/// <summary>
	/// Apparition du nom de la planète et du champs pour taper le texte
	/// </summary>
	void GoToCreationState (){
		GameObject.FindGameObjectWithTag("Title_Text").GetComponent<Animator> ().SetBool ("Title_Disappear",true);
		StartCoroutine ("CreationStateCoroutine");
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
