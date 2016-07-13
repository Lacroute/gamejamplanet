using UnityEngine;
using System.Collections;
using UnityEngine.UI;

 public class GUIManager : MonoBehaviour {

	public GameObject[] IntroSlides;
	private int currentIntroSlide = 0;
	public bool IntroNextButtonClicked = false;
	public float FadeDuration;

	// Coroutine qui gère tout l'intro lorsqu'elle est lancée.
	// Se termine en signalant sa fin au gamestate manager. 
	IEnumerator LaunchIntro (){
	
		for (currentIntroSlide = 0; currentIntroSlide < IntroSlides.Length ; currentIntroSlide++){
			IntroNextButtonClicked = false;
			string IntroUIElementName = IntroSlides [currentIntroSlide].name;

			// Je passe passe par le script "GUIEFFECTS" qui gère les fonctions d'anime.
			GetComponent<GUIEffects> ().FadeIn ("B_NextIntroButton");
			GetComponent<GUIEffects> ().FadeIn (IntroUIElementName);


			// Tant que le bouton n'est pas clicker, la fonction FOR ne continue pas. La variable IntroNextButtonClicked
			// est changé au click (cf ONCLICK dans le bouton) en appelant la fonction IntroNextButtonPressed();
			while(!IntroNextButtonClicked )
			{
				yield return null;

				}
				
			GetComponent<GUIEffects> ().FadeOut ("B_NextIntroButton");
			GetComponent<GUIEffects> ().FadeOut (IntroUIElementName);
			yield return new WaitForSeconds (FadeDuration);

			// Changer le texte du bouton "suivant" par "Terminer" pour lancer le jeu
			if(currentIntroSlide == IntroSlides.Length -2){
				yield return new WaitForSeconds (1.2F);
				Text myButtonText = GameObject.Find ("B_NextIntroButton").transform.GetChild (0).GetComponent<Text> ();
				myButtonText.text = "Terminer";
			}

			// DECLENCHE LE CHANGEMENT DETAT DU JEU LORSQUE LE DERNIER SLIDE EST CLICKER
			if(currentIntroSlide == IntroSlides.Length -1){
				GameObject.FindGameObjectWithTag ("GameManager").GetComponent<StatePatternGame> ().currentState.ToViewPlanetState ();
			}

		}
			
	}
		
	public void IntroNextButtonPressed(){
		IntroNextButtonClicked = true;

	}
		
}
