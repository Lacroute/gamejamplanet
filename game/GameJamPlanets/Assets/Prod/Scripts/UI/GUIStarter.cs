using UnityEngine;
using System.Collections;

public class GUIStarter : MonoBehaviour {

	IEnumerator TestButtonFade (){
		string uiObjectNameToFade = "B_SuivantIntroButton";
		GetComponent<GUIManager> ().FadeIn (uiObjectNameToFade);
		yield return new WaitForSeconds (3F);
		GetComponent<GUIManager> ().FadeOut (uiObjectNameToFade);

	}

	void Awake(){
		StartCoroutine ("TestButtonFade");
	}
}
