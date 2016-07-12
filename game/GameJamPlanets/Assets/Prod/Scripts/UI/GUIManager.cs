using UnityEngine;
using System.Collections;

 public class GUIManager : MonoBehaviour {

	/// <summary>
	/// Fades IN and OUT.
	/// </summary>
	/// <param name="uiObjectNameToFadeIn">User interface object name to fade in and out.</param>
	public void FadeIn(string uiObjectNameToFade){
		
		GameObject ToFadeObject = GameObject.Find (uiObjectNameToFade);

		if(ToFadeObject.tag == "Untagged"){
			Debug.Log ("Il faut ajouter un tag à " + ToFadeObject.transform.name + " pour que le fade fonctionne");
		}

		else if(ToFadeObject.tag == "guiText"){
		ToFadeObject.GetComponent<Animator> ().SetTrigger ("FadeInTrigger");
		} 

		else if(ToFadeObject.tag == "guiButton"){
		ToFadeObject.GetComponent<Animator> ().SetTrigger ("FadeInTrigger");
		ToFadeObject.transform.GetChild (0).GetComponent<Animator> ().SetTrigger ("FadeInTrigger");
		}

	}

	public void FadeOut(string uiObjectNameToFade){

		GameObject ToFadeObject = GameObject.Find (uiObjectNameToFade);

		if(ToFadeObject.tag == "Untagged"){
			Debug.Log ("Il faut ajouter un tag à " + ToFadeObject.transform.name + " pour que le fade fonctionne");
		}

		else if (ToFadeObject.tag == "guiText") {
			ToFadeObject.GetComponent<Animator> ().SetTrigger ("FadeOutTrigger");
		}

		else if(ToFadeObject.tag == "guiButton"){
			ToFadeObject.GetComponent<Animator> ().SetTrigger ("FadeOutTrigger");
			ToFadeObject.transform.GetChild (0).GetComponent<Animator> ().SetTrigger ("FadeOutTrigger");
		}

	}
		
		
}
