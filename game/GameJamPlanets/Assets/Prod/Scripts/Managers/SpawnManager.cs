using UnityEngine;
using System;


// Class qui s'occupe de toutes les instantiation 
// Instantie et gère les Spawnpoints.
// C'est donc ici qu'on déclare les objets.

public class SpawnManager : MonoBehaviour {
	public GameObject P_Title;
	public GameObject P_Planet;

	public GameObject P_TitleSpawn;
	public GameObject P_PlanetSpawn;
	public GameObject P_TitleTarget;

	// FONCTION INSTANTIATION SPLASH SCREEN
	public void InstantiateSplashObjects(){
		GameObject SplashTitle_GO = Instantiate(P_Title,P_TitleSpawn.transform.position,Quaternion.identity) as GameObject;
		//GameObject SplashPlanet_GO = Instantiate(P_Planet,P_PlanetSpawn.transform.position,Quaternion.identity) as GameObject;

	}


}
