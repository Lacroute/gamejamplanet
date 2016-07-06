using UnityEngine;
using System;


// Class qui s'occupe de toutes les instantiation 
// Instantie et gère les Spawnpoints.
// C'est donc ici qu'on déclare les objets.

public class SpawnManager : MonoBehaviour {
	public GameObject P_Title_Prefab;
	public GameObject P_Planet_Prefab;

	public GameObject P_Title_Spawn_GO;
	public GameObject P_Planet_Spawn_GO;
	public GameObject P_Title_Target_GO;

	// FONCTION INSTANTIATION SPLASH SCREEN
	public void InstantiateSplashObjects(){
		GameObject SplashTitle_GO = Instantiate(P_Title_Prefab,P_Title_Spawn_GO.transform.position,Quaternion.identity) as GameObject;
		GameObject SplashPlanet_GO = Instantiate(P_Planet_Prefab,P_Planet_Spawn_GO.transform.position,Quaternion.identity) as GameObject;

	}


}
