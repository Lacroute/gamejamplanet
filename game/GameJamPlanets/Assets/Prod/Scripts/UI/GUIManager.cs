using UnityEngine;
using System.Collections;

 public class GUIManager : MonoBehaviour {

	/// <summary>
	/// Fades IN and OUT.
	/// </summary>
	/// <param name="uiObjectNameToFadeIn">User interface object name to fade in and out.</param>
	public void FadeIn(string uiObjectNameToFadeIn){
		
		GameObject ToFadeObject = GameObject.Find (uiObjectNameToFadeIn);
		ToFadeObject.GetComponent<Animator> ().SetTrigger ("FadeInTrigger");

	}

	public void FadeOut(string uiObjectNameToFadeOut){

		GameObject ToFadeObject = GameObject.Find (uiObjectNameToFadeOut);
		ToFadeObject.GetComponent<Animator> ().SetTrigger ("FadeOutTrigger");

	}
		
}
