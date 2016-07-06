using UnityEngine;
using System;

public class LevelManager : MonoBehaviour 
{	
	public SpawnManager SpawnScript;
	public GameObject SplashTitle_Prefab;
	public GameObject SplashPlanet_Prefab;
	//SetupScene est la fonction qui initialise le niveau
	// D'abord on fait un Setup sans paramètre comme si le jeu se lancait pour la première fois
	// a chaque fois.

	public void SetupScene () 
	{
		SetUpSplash ();
	}
		
	public void Instantiate_Managers() 
	{
		// Instantier l'UI Manager
		// Instantier le SpawnManager
		// Instantier le PlayerManager
		// Instantier le SoundManager
		
	}

	public void SetUpSplash(){
		// Instantiation des GO présents sur la SplashScreen
		// Le titre du jeu et la planète
		SpawnScript.InstantiateSplashObjects ();

	}
		
		
}