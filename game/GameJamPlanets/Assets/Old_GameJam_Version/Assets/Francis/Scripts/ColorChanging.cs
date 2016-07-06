using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class ColorChanging : MonoBehaviour {
	public Color newHostColor;
	public GameObject myPlanet;

	public GameObject myparticleSystem;
	public Camera myCamera;

	void Start(){

	}

	void Update(){
		ChangeHostColor ();
	}

	void FillingWithColor(){
		myCamera.transform.GetComponent<ColorCorrectionCurves> ().saturation = Mathf.Lerp (myCamera.transform.GetComponent<ColorCorrectionCurves> ().saturation,1.5F,0.3F);
	}

	void ChangeHostColor(){
		// Lerp de la couleur de la planète et du nuage vers la couleur cible. A mettre dans l'état "Changement d'idée sur la planète".
		myPlanet.GetComponent<Renderer>().material.color = Color.Lerp (myPlanet.GetComponent<Renderer> ().material.color,newHostColor,0.3F);
		myCamera.transform.GetComponent<ColorCorrectionCurves> ().selectiveFromColor = newHostColor;
		myparticleSystem.GetComponent<ParticleSystem>().startColor = Color.Lerp (myparticleSystem.GetComponent<ParticleSystem>().startColor,newHostColor,0.1F);


	}
}

