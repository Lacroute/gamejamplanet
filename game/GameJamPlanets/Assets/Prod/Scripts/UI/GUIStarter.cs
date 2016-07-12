using UnityEngine;
using System.Collections;

public class GUIStarter : MonoBehaviour {

	void Awake(){
		string uiObjectNameToFadeIn = "T_Intro_1";
		GetComponent<GUIManager> ().FadeIn (uiObjectNameToFadeIn);
	}
}
