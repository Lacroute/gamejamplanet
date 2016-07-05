using UnityEngine;
using System;

	public class LevelManager : MonoBehaviour
	{

		//SetupScene est la fonction qui initialise le niveau
		// D'abord on fait un Setup sans paramètre comme si le jeu se lancait pour la première fois
		// a chaque fois.
		public void SetupScene ()
		{

		// Instantiation des Managers
		Instantiate_Managers();

		}
		
		public void Instantiate_Managers()
		{
		// Instantier l'UI Manager
		// Instantier le SpawnManager
		// Instantier le PlayerManager
		// Instantier le SoundManager
		
		}
		
	}